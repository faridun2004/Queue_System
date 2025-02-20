using MediatR;
using Microsoft.AspNetCore.Mvc;
using Queue_WebApi.CQRS.Commands;
using Queue_WebApi.Models;

namespace Queue_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HallController
    {
        private readonly IMediator _mediator;
        public HallController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateHall([FromBody] CreateHallCommand command)
        {
            var hall = await _mediator.Send(command);
            return new OkObjectResult(hall);
        }
    }
}
