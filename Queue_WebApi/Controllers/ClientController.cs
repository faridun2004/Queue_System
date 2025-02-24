using MediatR;
using Microsoft.AspNetCore.Mvc;
using Queue_WebApi.CQRS.Commands;
using Queue_WebApi.CQRS.Queries.Queues;

namespace Queue_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController
    {
        private readonly IMediator _mediator;
        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("by-client/{clientId}")]
        public async Task<IActionResult> GetByClientId(int clientId)
        {
            var result = await _mediator.Send(new GetAppointmentsByClientIdQuery(clientId));
            return new OkObjectResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateHall([FromBody] CreateClientCommand command)
        {
            var hall = await _mediator.Send(command);
            return new OkObjectResult(hall);
        }
    }
}
