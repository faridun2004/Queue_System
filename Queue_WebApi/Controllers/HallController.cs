using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queue_WebApi.CQRS.Halls.Commands;
using Queue_WebApi.CQRS.Halls.Queries;

namespace Queue_WebApi.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class HallController: ControllerBase
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
            return Ok(hall);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateHall([FromBody] UpdateHallCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHall(Guid id)
        {
            await _mediator.Send(new DeleteHallCommand(id));
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("{hallId}")]
        public async Task<IActionResult> GetById(Guid hallId)
        {
            var hall = await _mediator.Send(new GetHallById(hallId));
            return Ok(hall); 
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var halls = await _mediator.Send(new GetHalls());
            return Ok(halls);
        }
    }
}
