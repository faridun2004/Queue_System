using Appointment_Hall.Use_Cases.DTOs;
using MediatR;

namespace Appointment_Hall.Use_Cases.Clients.Queries
{
    public record GetClientReservationsQuery(
        Guid ClientId
        ) : IRequest<List<ReservationDto>>;
}
