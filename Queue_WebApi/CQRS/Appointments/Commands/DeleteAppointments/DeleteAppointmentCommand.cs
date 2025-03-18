using MediatR;

namespace Queue_WebApi.CQRS.Appointments.Commands.DeleteAppointments
{
    public record DeleteAppointmentCommand(

        Guid AppointmentId,
        Guid UserId
        ) : IRequest<bool>;

}
