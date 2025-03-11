using MediatR;

namespace Queue_WebApi.CQRS.Appointments.Commands
{
    public record DeleteAppointmentCommand(Guid AppointmentId) : IRequest;

}
