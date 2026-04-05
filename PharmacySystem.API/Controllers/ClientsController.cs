using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.API.DTOs;
using PharmacySystem.API.models;

namespace PharmacySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Get All Clients
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetClients()
        {
            var clients = _context.Clients
                .Select(c => new ClientDto
                {
                    Id = c.Client_ID,
                    Client_Name = c.Client_Name,
                    Client_Phone = c.Client_Phone,
                    Client_Address = c.Client_Address
                })
                .ToList();

            return Ok(clients);
        }

        // 🔹 Get Client by Id
        [HttpGet("{id:guid}")]
        public IActionResult GetClientById(Guid id)
        {
            var client = _context.Clients
                .Where(c => c.Client_ID == id)
                .Select(c => new ClientDto
                {
                    Id = c.Client_ID,
                    Client_Name = c.Client_Name,
                    Client_Phone = c.Client_Phone,
                    Client_Address = c.Client_Address
                })
                .FirstOrDefault();

            if (client == null)
                return NotFound();

            return Ok(client);
        }

        // 🔹 Create Client
        [HttpPost]
        public IActionResult CreateClient(CreateClientDto dto)
        {
            var client = new Client
            {
                Client_ID = Guid.NewGuid(),
                Client_Name = dto.Client_Name,
                Client_Phone = dto.Client_Phone,
                Client_Address = dto.Client_Address
            };

            _context.Clients.Add(client);
            _context.SaveChanges();

            return Ok(new ClientDto
            {
                Id = client.Client_ID,
                Client_Name = client.Client_Name,
                Client_Phone = client.Client_Phone,
                Client_Address = client.Client_Address
            });
        }

        // 🔹 Update Client
        [HttpPut("{id:guid}")]
        public IActionResult UpdateClient(Guid id, UpdateClientDto dto)
        {
            var client = _context.Clients.Find(id);

            if (client == null)
                return NotFound();

            client.Client_Name = dto.Client_Name;
            client.Client_Phone = dto.Client_Phone;
            client.Client_Address = dto.Client_Address;

            _context.SaveChanges();

            return Ok(new ClientDto
            {
                Id = client.Client_ID,
                Client_Name = client.Client_Name,
                Client_Phone = client.Client_Phone,
                Client_Address = client.Client_Address
            });
        }

        // 🔹 Delete Client
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteClient(Guid id)
        {
            var client = _context.Clients.Find(id);

            if (client == null)
                return NotFound();

            _context.Clients.Remove(client);
            _context.SaveChanges();

            return Ok("Client deleted.");
        }
    }
}
