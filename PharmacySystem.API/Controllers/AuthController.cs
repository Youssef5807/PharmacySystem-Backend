using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PharmacySystem.API.DTO;
using PharmacySystem.API.models;

namespace PharmacySystem.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _jwtKey = "JustF0rGr@du@t!0nPr0ject_1234567890";

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto request)
        {
            // ✅ 1. Validate request
            if (request == null)
                return BadRequest("Request body is required");

            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Email and password are required");

            // ✅ 2. Find user
            var employee = _context.Employees
                .FirstOrDefault(e => e.Email == request.Email);

            if (employee == null)
                return Unauthorized("Invalid email or password");

            // ✅ 3. Validate password hash
            if (string.IsNullOrEmpty(employee.PasswordHash))
                return StatusCode(500, "Password hash is missing in database");

            if (!employee.PasswordHash.StartsWith("$2"))
                return StatusCode(500, "Stored password is not a valid BCrypt hash");

            // ✅ 4. Verify password
            bool isPasswordCorrect =
                BCrypt.Net.BCrypt.Verify(request.Password, employee.PasswordHash);

            if (!isPasswordCorrect)
                return Unauthorized("Invalid email or password");

            // ✅ 5. Generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, employee.Employee_ID.ToString()),
                    new Claim(ClaimTypes.Name, employee.Employee_Name),
                    new Claim(ClaimTypes.Email, employee.Email),
                    new Claim(ClaimTypes.Role, employee.Employee_Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            // ✅ 6. Return response
            return Ok(new
            {
                Id = employee.Employee_ID,
                Name = employee.Employee_Name,
                Email = employee.Email,
                Role = employee.Employee_Role,
                Token = jwtToken
            });
        }
    }
}
