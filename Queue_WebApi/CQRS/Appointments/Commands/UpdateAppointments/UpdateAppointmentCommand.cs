using MediatR;

namespace Queue_WebApi.CQRS.Appointments.Commands.UpdateAppointments
{
    public record UpdateAppointmentCommand(
        Guid UserId,
        Guid AppointmentId,
        Guid HallId,
        DateTime StartTime,
        DateTime EndTime
    ) : IRequest<bool>;
}
