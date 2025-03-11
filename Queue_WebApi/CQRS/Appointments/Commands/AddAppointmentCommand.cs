using MediatR;

namespace Queue_WebApi.CQRS.Appointments.Commands
{
    public record AddAppointmentCommand(
        Guid HallId, 
        Guid UserId, 
        DateTime StartTime, 
        DateTime EndTime
                        ) : IRequest<Guid>;

}
