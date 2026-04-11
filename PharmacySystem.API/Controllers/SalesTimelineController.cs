using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.API.models; // تأكد إن ده مسار الموديلات الصح عندك

namespace PharmacySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesTimelineController : ControllerBase // لازم يرث من ControllerBase
    {
        private readonly AppDbContext _context; // تعريف الـ Context

        // Constructor: السطر ده هو اللي بيربط الكنترولر بالداتا بيز
        public SalesTimelineController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet] // المسار هيكون api/SalesTimeline
        public async Task<IActionResult> GetSalesTimeline()
        {
            try
            {
                var today = DateTime.Now.Date;
                var startOfMonth = new DateTime(today.Year, today.Month, 1);
                var startOfWeek = today.AddDays(-(int)today.DayOfWeek);

                // استخدمنا _context بدل DbContext
                var orders = await _context.Orders
                    .Where(o => o.Order_Date >= today.AddDays(-28))
                    .ToListAsync();

                var timeline = new
                {
                    TodaySales = orders.Where(o => o.Order_Date.Date == today).Sum(o => o.Total_Amount),
                    ThisWeekSales = orders.Where(o => o.Order_Date.Date >= startOfWeek).Sum(o => o.Total_Amount),
                    ThisMonthSales = orders.Where(o => o.Order_Date.Date >= startOfMonth).Sum(o => o.Total_Amount),

                    Last4WeeksDetail = Enumerable.Range(0, 4).Select(i => {
                        var start = today.AddDays(-(i + 1) * 7);
                        var end = today.AddDays(-i * 7);
                        return new
                        {
                            WeekLabel = $"Week {4 - i}",
                            DateRange = $"{start:dd/MM} - {end:dd/MM}",
                            Total = orders.Where(o => o.Order_Date >= start && o.Order_Date < end).Sum(o => o.Total_Amount)
                        };
                    }).Reverse().ToList()
                };

                return Ok(timeline);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }
    }
}