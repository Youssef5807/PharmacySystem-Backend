using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.API.models;
using System.Security.Claims;

namespace PharmacySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesTimelineController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalesTimelineController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesTimeline()
        {
            try
            {
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userRole) || string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized(new { Message = "User identity not found in token." });
                }

                int userId = int.Parse(userIdClaim);
                var today = DateTime.Now.Date;
                var startOfMonth = new DateTime(today.Year, today.Month, 1);
                var startOfWeek = today.AddDays(-(int)today.DayOfWeek);

                var query = _context.Orders.Where(o => o.Order_Date >= today.AddDays(-28));

                if (userRole != "Admin")
                {
                    query = query.Where(o => o.Employee_ID == userId);
                }

                var orders = await query.ToListAsync();

                var timeline = new
                {
                    UserRole = userRole,
                    TodaySales = orders.Where(o => o.Order_Date.Date == today).Sum(o => o.Total_Amount),
                    ThisWeekSales = orders.Where(o => o.Order_Date.Date >= startOfWeek).Sum(o => o.Total_Amount),
                    ThisMonthSales = orders.Where(o => o.Order_Date.Date >= startOfMonth).Sum(o => o.Total_Amount),

                    // إضافة تفاصيل الأسابيع مع عدد الأوردرات
                    Last4WeeksDetail = Enumerable.Range(0, 4).Select(i => {
                        var start = today.AddDays(-(i + 1) * 7);
                        var end = today.AddDays(-i * 7);

                        // تصفية الأوردرات الخاصة بهذا الأسبوع فقط
                        var weeklyOrders = orders.Where(o => o.Order_Date >= start && o.Order_Date < end).ToList();

                        return new
                        {
                            WeekLabel = $"Week {4 - i}",
                            DateRange = $"{start:dd/MM} - {end:dd/MM}",
                            TotalAmount = weeklyOrders.Sum(o => o.Total_Amount),
                            OrdersCount = weeklyOrders.Count // <--- ده السطر اللي بيحسب عدد الأوردرات
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