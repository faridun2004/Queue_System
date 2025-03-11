using MediatR;

namespace Queue_WebApi.CQRS.Appointments.Commands
{
    public record UpdateAppointmentCommand(
        Guid AppointmentId,
        DateTime StartTime, 
        DateTime EndTime
                        ) : IRequest;

}
