using MediatR;
using Microsoft.AspNetCore.Mvc;
using Queue_WebApi.CQRS.Appointments.Commands;
using Queue_WebApi.CQRS.Appointments.Queries;
using Queue_WebApi.CQRS.Groups.Commands;
using Queue_WebApi.CQRS.Groups.Queries;
using Queue_WebApi.CQRS.Halls.Commands;
using Queue_WebApi.CQRS.Halls.Queries;

namespace Queue_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] AddAppointmentCommand command)
        {
            var appointment = await _mediator.Send(command);
            return new OkObjectResult(appointment);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid appointmentId)
        {
            var appointment = await _mediator.Send(new GetAppointmentById(appointmentId));
            return Ok(appointment);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await _mediator.Send(new GetAppointmentsQuery());
            return Ok(appointments);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointmentById(Guid Id)
        {
                await _mediator.Send(new DeleteAppointmentCommand(Id));
                return Ok();    
        }
    }

}

