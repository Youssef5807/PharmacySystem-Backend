using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.API.models;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    // =========================
    // ✅ CREATE ORDER
    // =========================
    [HttpPost]
    [Authorize(Roles = "Pharmacist,Admin")]
    [HttpPost]
    public IActionResult CreateOrder(CreateOrderDto dto)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            var order = new Order
            {
                // لضمان أن الـ SQL هو من يحدد الرقم، تأكد أن القيمة تبدأ بـ 0
                Order_ID = 0,
                Client_ID = dto.Client_ID,
                Employee_ID = 1,
                Payment_Method = "Cash",
                Order_Date = DateTime.Now,
                OrderItems = new List<Order_Item>()
            };

            foreach (var item in dto.Items)
            {
                var medicine = _context.Medicines
                    .FirstOrDefault(m => m.Medicine_ID == item.Medicine_ID);

                if (medicine == null)
                    return BadRequest($"Medicine with ID {item.Medicine_ID} not found");

                if (medicine.Quantity_In_Stock < item.Quantity)
                {
                    return BadRequest(new
                    {
                        Message = $"Not enough stock for {medicine.Medicine_Name}",
                        AvailableQuantity = medicine.Quantity_In_Stock,
                        RequestedQuantity = item.Quantity
                    });
                }

                var orderItem = new Order_Item
                {
                    Order_Item_ID = 0, // لضمان الترقيم التلقائي للأصناف أيضاً
                    Medicine_ID = item.Medicine_ID,
                    Quantity = item.Quantity,
                    Sub_Total = medicine.Selling_Price * item.Quantity,
                    // حذفنا سطر Order = order لتجنب مشاكل الـ Tracking المعقدة أحياناً
                };

                order.OrderItems.Add(orderItem);

                // تحديث المخزون
                medicine.Quantity_In_Stock -= item.Quantity;
            }

            order.Total_Amount = order.OrderItems.Sum(i => i.Sub_Total);

            _context.Orders.Add(order);
            _context.SaveChanges();

            transaction.Commit();

            // 🔥 التعديل الجوهري هنا: نرجع رسالة نجاح بدل ما نرجع الـ order كامل
            // ده بيمنع الـ Object Cycle تماماً لأننا مش بنرجع العلاقات المعقدة
            return Ok(new
            {
                Message = "Order Created Successfully",
                OrderId = order.Order_ID,
                Total = order.Total_Amount
            });
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return StatusCode(500, new
            {
                Message = "Something went wrong",
                Error = ex.InnerException?.Message ?? ex.Message
            });
        }
    }
    // =========================
    // ✅ GET ALL ORDERS
    // =========================
    [HttpGet]
    public IActionResult GetAllOrders()
    {
        var orders = _context.Orders
            .Include(o => o.OrderItems)
            .ToList();

        return Ok(orders);
    }
}
