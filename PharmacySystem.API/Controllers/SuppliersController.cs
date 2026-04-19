using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.API.models;
using Microsoft.EntityFrameworkCore;

namespace PharmacySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] 
    public class SuppliersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SuppliersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await _context.Suppliers.ToListAsync();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound(new { message = "Supplier not found." });
            }

            return Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplier(SupplierDto dto)
        {
            var supplier = new Supplier
            {
                Supplier_Name = dto.Name,
                Supplier_Phone = dto.Phone,
                Supplier_Address = dto.Address
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Supplier added successfully.", data = supplier });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, SupplierDto dto)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound(new { message = "Supplier not found." });
            }

            supplier.Supplier_Name = dto.Name;
            supplier.Supplier_Phone = dto.Phone;
            supplier.Supplier_Address = dto.Address;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Supplier updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound(new { message = "Supplier not found." });
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Supplier deleted successfully." });
        }
    }

    
}