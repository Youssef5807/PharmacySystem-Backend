using Microsoft.AspNetCore.Mvc;
using PharmacySystem.API.models;
using System.Linq;
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

        [HttpGet]
        public IActionResult GetClients()
        {
            var clients = _context.Clients.ToList();
            return Ok(clients);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetClientById(Guid id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public IActionResult CreateClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return Ok(client);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateClient(Guid id, Client client)
        {
            var existingClient = _context.Clients.Find(id);
            if (existingClient == null)
                return NotFound();

            existingClient.Client_Name = client.Client_Name;
            existingClient.Client_Phone = client.Client_Phone;
            existingClient.Client_Address = client.Client_Address;

            _context.SaveChanges();
            return Ok(existingClient);
        }

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
