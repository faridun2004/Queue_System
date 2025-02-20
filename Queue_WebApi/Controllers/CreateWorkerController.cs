using MediatR;
using Microsoft.AspNetCore.Mvc;
using Queue_WebApi.CQRS.Commands;

namespace Queue_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateWorkerController
    {
        private readonly IMediator _mediator;
        public CreateWorkerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateWorker([FromBody] CreateWorkerCommand command)
        {
            var apointment = await _mediator.Send(command);
            return new OkObjectResult(apointment);
        }
    }
}
