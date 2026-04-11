using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacySystem.API.DTOs;
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

        [HttpGet]
        [Authorize(Roles = "Admin,Pharmacist")]
        public IActionResult GetMedicines()
        {
            var medicines = _context.Medicines
                .Select(m => new MedicineDto
                {
                    Medicine_ID = m.Medicine_ID,
                    Medicine_Name = m.Medicine_Name,
                    Selling_Price = m.Selling_Price,
                    Cost_Price = m.Cost_Price,
                    Batch_No = m.Batch_No,
                    Quantity_In_Stock = m.Quantity_In_Stock
                })
                .ToList();

            return Ok(medicines);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Pharmacist")]
        public IActionResult GetMedicineById(int id)
        {
            var medicine = _context.Medicines
                .Where(m => m.Medicine_ID == id)
                .Select(m => new MedicineDto
                {
                    Medicine_ID = m.Medicine_ID,
                    Medicine_Name = m.Medicine_Name,
                    Selling_Price = m.Selling_Price,
                    Cost_Price = m.Cost_Price,
                    Batch_No = m.Batch_No,
                    Quantity_In_Stock = m.Quantity_In_Stock
                })
                .FirstOrDefault();

            if (medicine == null)
                return NotFound();

            return Ok(medicine);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Pharmacist")]
        public IActionResult AddMedicine(CreateMedicineDto dto)
        {
            var medicine = new Medicine
            {
                Medicine_Name = dto.Medicine_Name,
                Selling_Price = dto.Selling_Price,
                Cost_Price = dto.Cost_Price,
                Batch_No = dto.Batch_No,
                Quantity_In_Stock = dto.Quantity_In_Stock
            };

            _context.Medicines.Add(medicine);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMedicineById), new { id = medicine.Medicine_ID }, new MedicineDto
            {
                Medicine_ID = medicine.Medicine_ID,
                Medicine_Name = medicine.Medicine_Name,
                Selling_Price = medicine.Selling_Price,
                Cost_Price = medicine.Cost_Price,
                Batch_No = medicine.Batch_No,
                Quantity_In_Stock = medicine.Quantity_In_Stock
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Pharmacist")]
        public IActionResult UpdateMedicine(int id, UpdateMedicineDto dto)
        {
            var existingMedicine = _context.Medicines.Find(id);

            if (existingMedicine == null)
                return NotFound();

            existingMedicine.Medicine_Name = dto.Medicine_Name;
            existingMedicine.Selling_Price = dto.Selling_Price;
            existingMedicine.Cost_Price = dto.Cost_Price;
            existingMedicine.Batch_No = dto.Batch_No;
            existingMedicine.Quantity_In_Stock = dto.Quantity_In_Stock;

            _context.SaveChanges();

            return Ok(new MedicineDto
            {
                Medicine_ID = existingMedicine.Medicine_ID,
                Medicine_Name = existingMedicine.Medicine_Name,
                Selling_Price = existingMedicine.Selling_Price,
                Cost_Price = existingMedicine.Cost_Price,
                Batch_No = existingMedicine.Batch_No,
                Quantity_In_Stock = existingMedicine.Quantity_In_Stock
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteMedicine(int id)
        {
            var medicine = _context.Medicines.Find(id);

            if (medicine == null)
                return NotFound();

            _context.Medicines.Remove(medicine);
            _context.SaveChanges();

            return Ok($"Medicine with ID {id} deleted.");
        }
    }
}
