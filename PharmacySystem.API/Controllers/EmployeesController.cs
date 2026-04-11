using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PharmacySystem.API.models;

namespace PharmacySystem.API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetMyData()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var employee = _context.Employees
                .FirstOrDefault(e => e.Employee_ID.ToString() == userId);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpGet("all")]
        public IActionResult GetAllEmployees()
        {
            return Ok(_context.Employees.ToList());
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto dto)
        {
            var employee = new Employee
            {
                Employee_Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Employee_Role = dto.Role,
                Salary = dto.Salary,
                Attendance_Details = dto.Attendance_Details
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return Ok(employee);
        }
    }
}
