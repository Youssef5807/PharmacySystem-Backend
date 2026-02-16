using Microsoft.AspNetCore.Mvc;
using PharmacySystem.API.models;
using System.Linq;

namespace PharmacySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicinesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicinesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Medicines
        [HttpGet]
        public IActionResult GetMedicines()
        {
            var medicines = _context.Medicines.ToList();
            return Ok(medicines);
        }

        // GET: api/Medicines/5
        [HttpGet("{id}")]
        public IActionResult GetMedicineById(int id)
        {
            var medicine = _context.Medicines.Find(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return Ok(medicine);
        }

        // POST: api/Medicines
        [HttpPost]
        public IActionResult AddMedicine(Medicine medicine)
        {
            if (medicine == null)
            {
                return BadRequest("Medicine data is required.");
            }

            _context.Medicines.Add(medicine);
            _context.SaveChanges();

            // 201 Created
            return CreatedAtAction(nameof(GetMedicineById), new { id = medicine.Medicine_ID }, medicine);
        }

        // PUT: api/Medicines/5
        [HttpPut("{id}")]
        public IActionResult UpdateMedicine(int id, Medicine medicine)
        {
            if (medicine == null)
            {
                return BadRequest("Medicine data is required.");
            }

            var existingMedicine = _context.Medicines.Find(id);
            if (existingMedicine == null)
            {
                return NotFound();
            }

            existingMedicine.Medicine_Name = medicine.Medicine_Name;
            existingMedicine.Selling_Price = medicine.Selling_Price;
            existingMedicine.Cost_Price = medicine.Cost_Price;
            existingMedicine.Batch_No = medicine.Batch_No;
            existingMedicine.Quantity_In_Stock = medicine.Quantity_In_Stock;

            _context.SaveChanges();

            return Ok(existingMedicine);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMedicine(int id)
        {
            var medicine = _context.Medicines.Find(id);

            if (medicine == null)
            {
                return NotFound();
            }

            _context.Medicines.Remove(medicine);
            _context.SaveChanges();

            return Ok($"Medicine with ID {id} deleted.");
        }
    }
}
