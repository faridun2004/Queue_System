using Appointment_Hall.Data;
using Appointment_Hall.Use_Cases.Clients.Queries;
using Appointment_Hall.Use_Cases.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Hall.Use_Cases.Clients.Handlers
{
    public class GetClientReservationsQueryHandler : IRequestHandler<GetClientReservationsQuery, List<ReservationDto>>
    {
        private readonly AppDbContext _context;

        public GetClientReservationsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationDto>> Handle(GetClientReservationsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Reservations
                .Where(r => r.ClientId == request.ClientId)
                .Select(r => new ReservationDto
                {
                    HallName = r.Hall.Name,
                    StartTime = r.StartTime,
                    EndTime = r.EndTime
                })
                .ToListAsync(cancellationToken);
        }
    }

}
