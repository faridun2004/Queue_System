using MediatR;

namespace Appointment_Hall.Use_Cases.Reservations.Commands
{
    public record UpdateReservationCommand(
        Guid ReservationId, 
        DateTime StartTime, 
        DateTime EndTime
                                          ) : IRequest<Unit>;
}
