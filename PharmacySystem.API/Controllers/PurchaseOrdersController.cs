using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.API.models;

namespace PharmacySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Locked for Admin use
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PurchaseOrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseOrders
        [HttpGet]
        public async Task<IActionResult> GetAllPurchaseOrders()
        {
            // We include Supplier and Employee data to show who handled the order
            var orders = await _context.PurchaseOrders
                .Include(po => po.Supplier)
                .Include(po => po.Employee)
                .ToListAsync();
            return Ok(orders);
        }

        // GET: api/PurchaseOrders/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseOrderById(int id)
        {
            var order = await _context.PurchaseOrders
                .Include(po => po.Supplier)
                .Include(po => po.Employee)
                .Include(po => po.PurchaseItems) // Include items bought in this order
                .FirstOrDefaultAsync(po => po.PO_ID == id);

            if (order == null)
            {
                return NotFound(new { message = "Purchase Order not found." });
            }

            return Ok(order);
        }

        // POST: api/PurchaseOrders
        [HttpPost]
        public async Task<IActionResult> CreatePurchaseOrder(CreatePurchaseOrderDto dto)
        {
            var purchaseOrder = new Purchase_Order
            {
                Supplier_ID = dto.SupplierId,
                Employee_ID = dto.EmployeeId,
                PO_Date = DateTime.UtcNow,
                Total_Amount = dto.TotalAmount
            };

            _context.PurchaseOrders.Add(purchaseOrder);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Purchase Order created successfully.", orderId = purchaseOrder.PO_ID });
        }

        // DELETE: api/PurchaseOrders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.PurchaseOrders.FindAsync(id);
            if (order == null) return NotFound();

            _context.PurchaseOrders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order deleted successfully." });
        }
    }



}