using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.API.models;

namespace PharmacySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PurchaseOrdersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPurchaseOrders()
        {
            var orders = await _context.PurchaseOrders
                .Include(po => po.Supplier)
                .Include(po => po.Employee)
                .Select(po => new PurchaseOrderResponseDto
                {
                    PurchaseOrderId = po.PO_ID,
                    OrderDate = po.PO_Date,
                    TotalAmount = po.Total_Amount,
                    SupplierName = po.Supplier.Supplier_Name,
                    EmployeeName = po.Employee.Employee_Name
                })
                .ToListAsync();

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchaseOrder(CreatePurchaseOrderDto dto)
        {
            try
            {
                // 1. Validate existence of Supplier and Employee
                var supplierExists = await _context.Suppliers.AnyAsync(s => s.Supplier_ID == dto.SupplierId);
                var employeeExists = await _context.Employees.AnyAsync(e => e.Employee_ID == dto.EmployeeId);

                if (!supplierExists || !employeeExists)
                {
                    return BadRequest(new { message = "Invalid Supplier_ID or Employee_ID. Record not found." });
                }

                // 2. Map DTO to Model
                var purchaseOrder = new Purchase_Order
                {
                    Supplier_ID = dto.SupplierId,
                    Employee_ID = dto.EmployeeId,
                    PO_Date = DateTime.Now,
                    Total_Amount = dto.TotalAmount
                };

                _context.PurchaseOrders.Add(purchaseOrder);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    status = "Success",
                    message = "Purchase Order created successfully.",
                    orderId = purchaseOrder.PO_ID
                });
            }
            catch (Exception ex)
            {
                var detail = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, new
                {
                    error = "Internal Server Error",
                    details = detail
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.PurchaseOrders.FindAsync(id);
            if (order == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            _context.PurchaseOrders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order deleted successfully." });
        }
    }

    public class CreatePurchaseOrderDto
    {
        public int SupplierId { get; set; }
        public int EmployeeId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}