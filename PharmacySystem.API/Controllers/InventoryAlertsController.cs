using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.API.models;

namespace PharmacySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryAlertsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InventoryAlertsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetInventoryStatus()
        {
            try
            {
                var today = DateTime.Now;
                var oneMonthFromNow = today.AddMonths(1);

                var allMedicines = await _context.Medicines.ToListAsync();

                var status = new
                {
                    // 1. الأدوية الناقصة (أقل من 10 قطع كمثال للـ 10%)
                    LowStock = allMedicines.Where(m => m.Quantity_In_Stock > 0 && m.Quantity_In_Stock <= 10),

                    // 2. الأدوية اللي خلصت تماماً
                    OutOfStock = allMedicines.Where(m => m.Quantity_In_Stock == 0),

                    // 3. الأدوية اللي هتنتهي صلاحيتها خلال شهر
                    NearExpiry = allMedicines.Where(m => m.Expiry_Date <= oneMonthFromNow && m.Expiry_Date > today)
                };

                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error fetching inventory status", Details = ex.Message });
            }
        }

        // 4. إند بوينت منفصلة للبحث باسم الدواء
        [HttpGet("search")]
        public async Task<IActionResult> SearchMedicine([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Please provide a medicine name");

            var results = await _context.Medicines
                .Where(m => m.Medicine_Name.Contains(name))
                .Select(m => new {
                    m.Medicine_ID,
                    m.Medicine_Name,
                    m.Selling_Price,
                    m.Quantity_In_Stock,
                    m.Expiry_Date
                })
                .ToListAsync();

            return Ok(results);
        }
    }
}