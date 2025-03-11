using MediatR;

namespace Appointment_Hall.Use_Cases.Reservations.Commands
{
    public record CreateReservationCommand(
        Guid HallId, 
        Guid ClientId, 
        DateTime StartTime, 
        DateTime EndTime
                                           ) : IRequest<Guid>;
}
