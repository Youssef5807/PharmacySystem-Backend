using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            return Ok(new
            {
                employee.Employee_ID,
                employee.Employee_Name,
                employee.Email,
                employee.Employee_Role,
                employee.Salary
            });
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllEmployees()
        {
            var employees = _context.Employees.Select(e => new
            {
                e.Employee_ID,
                e.Employee_Name,
                e.Email,
                e.Employee_Role,
                e.Salary
            }).ToList();

            return Ok(employees);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddEmployee(AddEmployeeDto dto)
        {
            var exists = _context.Employees.Any(e => e.Email == dto.Email);
            if (exists)
                return BadRequest("Email already exists");

            var allowedRoles = new[] { "Admin", "Pharmacist" };
            if (!allowedRoles.Contains(dto.Role))
                return BadRequest("Invalid role");

            var employee = new Employee
            {
                Employee_Name = dto.Name,
                Email = dto.Email,
                PasswordHash = PasswordHasher.Hash(dto.Password),
                Employee_Role = dto.Role,
                Salary = dto.Salary,
                Attendance_Details = dto.Attendance_Details
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Employee added successfully",
                employee.Employee_ID,
                employee.Employee_Name,
                employee.Email,
                employee.Employee_Role,
                employee.Salary
            });
        }
    }
}
