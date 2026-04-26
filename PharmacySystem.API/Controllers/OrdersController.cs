using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.API.models;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ CREATE ORDER (Modified to get Employee ID from Token)
    [HttpPost]
    [Authorize(Roles = "Pharmacist,Admin")]
    public IActionResult CreateOrder(CreateOrderDto dto)
    {
        // سحب الـ ID بتاع الموظف اللي عامل Login حالياً
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized("User ID not found in token");
        int currentEmployeeId = int.Parse(userIdClaim);

        using var transaction = _context.Database.BeginTransaction();

        try
        {
            var order = new Order
            {
                Order_ID = 0,
                Client_ID = dto.Client_ID,
                Employee_ID = currentEmployeeId, // تم التعديل ليصبح ديناميكي
                Payment_Method = "Cash",
                Order_Date = DateTime.Now,
                OrderItems = new List<Order_Item>()
            };

            foreach (var item in dto.Items)
            {
                var medicine = _context.Medicines.FirstOrDefault(m => m.Medicine_ID == item.Medicine_ID);
                if (medicine == null) return BadRequest($"Medicine with ID {item.Medicine_ID} not found");

                if (medicine.Quantity_In_Stock < item.Quantity)
                {
                    return BadRequest(new { Message = $"Not enough stock for {medicine.Medicine_Name}" });
                }

                var orderItem = new Order_Item
                {
                    Order_Item_ID = 0,
                    Medicine_ID = item.Medicine_ID,
                    Quantity = item.Quantity,
                    Sub_Total = medicine.Selling_Price * item.Quantity
                };

                order.OrderItems.Add(orderItem);
                medicine.Quantity_In_Stock -= item.Quantity;
            }

            order.Total_Amount = order.OrderItems.Sum(i => i.Sub_Total);
            _context.Orders.Add(order);
            _context.SaveChanges();
            transaction.Commit();

            return Ok(new { Message = "Order Created Successfully", OrderId = order.Order_ID, EmployeeId = order.Employee_ID });
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return StatusCode(500, new { Error = ex.InnerException?.Message ?? ex.Message });
        }
    }

    // ✅ GET ALL ORDERS (Modified to return EmployeeId)
    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _context.Orders
            .Include(o => o.Client)
            .Include(o => o.Employee)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Medicine)
            .Select(o => new OrderResponseDto
            {
                OrderId = o.Order_ID,
                EmployeeId = o.Employee_ID, 
                OrderDate = o.Order_Date,
                TotalAmount = o.Total_Amount,
                PaymentMethod = o.Payment_Method,
                ClientName = o.Client != null ? o.Client.Client_Name : "Walk-in Customer",
                EmployeeName = o.Employee != null ? o.Employee.Employee_Name : "Unknown",
                Items = o.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    MedicineName = oi.Medicine != null ? oi.Medicine.Medicine_Name : "Unknown Product",
                    Quantity = oi.Quantity,
                    SubTotal = oi.Sub_Total
                }).ToList()
            })
            .ToListAsync();

        return Ok(orders);
    }
}