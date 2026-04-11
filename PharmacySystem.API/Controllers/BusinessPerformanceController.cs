using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.API.models;

namespace PharmacySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessPerformanceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BusinessPerformanceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPerformance()
        {
            try
            {
                // 1. أكثر 5 أدوية مبيعاً (الاسم، الكمية، إجمالي الإيراد)
                var topSelling = await _context.OrderItems
                    .Include(oi => oi.Medicine)
                    .GroupBy(oi => new { oi.Medicine_ID, oi.Medicine.Medicine_Name })
                    .Select(g => new
                    {
                        MedicineName = g.Key.Medicine_Name,
                        TotalQuantitySold = g.Sum(oi => oi.Quantity),
                        TotalRevenue = g.Sum(oi => oi.Sub_Total)
                    })
                    .OrderByDescending(x => x.TotalQuantitySold)
                    .Take(5)
                    .ToListAsync();

                // 2. إجمالي عدد الأوردرات في السيستم
                var totalOrdersCount = await _context.Orders.CountAsync();

                // 3. صافي المكسب: مجموع (سعر البيع - سعر الشراء) * الكمية المباعة
                var netProfit = await _context.OrderItems
                    .Include(oi => oi.Medicine)
                    .SumAsync(oi => (oi.Medicine.Selling_Price - oi.Medicine.Cost_Price) * oi.Quantity);

                return Ok(new
                {
                    TopSellingMedicines = topSelling,
                    TotalOrders = totalOrdersCount,
                    TotalNetProfit = netProfit
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error calculating performance", Details = ex.Message });
            }
        }
    }
}