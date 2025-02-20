using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Queue_WebApi.CQRS.Commands;

namespace Queue_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApointmentController
    {
        private readonly IMediator _mediator;
        public ApointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateApointment([FromBody] AddApointmentCommand command)
        {
            var apointment= await _mediator.Send(command);
            return new OkObjectResult(apointment);
        }
    }
}
