using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queue_WebApi.CQRS.Appointments.Commands;
using Queue_WebApi.CQRS.Appointments.Commands.CreateAppointments;
using Queue_WebApi.CQRS.Appointments.Commands.DeleteAppointments;
using Queue_WebApi.CQRS.Appointments.Commands.UpdateAppointments;
using Queue_WebApi.CQRS.Appointments.Queries;
using Queue_WebApi.CQRS.Halls.Commands;
using Queue_WebApi.DTOs;

namespace Queue_WebApi.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> GetById(Guid id)
        {
            var appointment = await _mediator.Send(new GetAppointmentById(id));
            return Ok(appointment);
        }
        [HttpPut("user/{userId}/update/{appointmentId}")]
        public async Task<IActionResult> UpdateByUserId(Guid userId, Guid appointmentId, [FromBody] UpdateAppointmentDto dto)
        {
            var command = new UpdateAppointmentCommand(userId, appointmentId, dto.HallId, dto.StartTime, dto.EndTime);
            var result = await _mediator.Send(command);

            return result ? Ok("Appointment updated successfully") : NotFound("Appointment not found or not owned by user");
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await _mediator.Send(new GetAppointmentsQuery());
            return Ok(appointments);
        }

        [HttpDelete("user/{userId}/delete/{appoinmentId}")]
        public async Task<IActionResult> DeleteAppointmentById(Guid appoinmentId, Guid userId)
        {
                await _mediator.Send(new DeleteAppointmentCommand(appoinmentId, userId));
                return NoContent();    
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var result = await _mediator.Send(new GetAppointmentByUserId(userId));
            return Ok(result);
        }
    }

}

