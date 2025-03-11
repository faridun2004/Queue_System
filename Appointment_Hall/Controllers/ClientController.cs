using Appointment_Hall.Data;
using Appointment_Hall.Models;
using Appointment_Hall.Use_Cases.Clients.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointment_Hall.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;

        public ClientController(AppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            client.Id = Guid.NewGuid();
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return Ok(client);
        }

        [HttpGet("{clientId}/reservations")]
        public async Task<IActionResult> GetClientReservations(Guid clientId)
        {
            var reservations = await _mediator.Send(new GetClientReservationsQuery(clientId));
            return Ok(reservations);
        }
    }

}
