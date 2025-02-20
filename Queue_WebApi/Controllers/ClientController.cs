using MediatR;
using Microsoft.AspNetCore.Mvc;
using Queue_WebApi.CQRS.Commands;

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
        [HttpPost]
        public async Task<IActionResult> CreateHall([FromBody] CreateClientCommand command)
        {
            var hall = await _mediator.Send(command);
            return new OkObjectResult(hall);
        }
    }
}
