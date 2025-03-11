using MediatR;

namespace Appointment_Hall.Use_Cases.Reservations.Commands
{
    public record DeleteReservationCommand(
        Guid ReservationId
                                          ) : IRequest<Unit>;
}
