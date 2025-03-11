using MediatR;

namespace Queue_WebApi.CQRS.Appointments.Commands.CreateAppointments
{
    public record AddAppointmentCommand(
        Guid HallId,
        DateTime StartTime,
        DateTime EndTime

                        ) : IRequest<Guid>;

}
