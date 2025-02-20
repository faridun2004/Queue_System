using MediatR;
using Microsoft.AspNetCore.Mvc;
using Queue_WebApi.CQRS.Commands;
using Queue_WebApi.CQRS.Queries.Queues;

namespace Queue_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly IMediator _mediator;
        public QueueController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateQueue([FromBody] CreateQueueCommand command)
        {
            var hall = await _mediator.Send(command);
            return new OkObjectResult(hall);
        }
        [HttpGet("{hallId}")]
        public async Task<IActionResult> GetQueueByHallId(int hallId)
        {
            var queue = await _mediator.Send(new GetQueueByHallId(hallId));
            return Ok(queue);
        }
    }

}
