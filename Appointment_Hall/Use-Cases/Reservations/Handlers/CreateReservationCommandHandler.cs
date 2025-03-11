using Appointment_Hall.Data;
using Appointment_Hall.Models;
using Appointment_Hall.Use_Cases.Reservations.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Hall.Use_Cases.Reservations.Handlers
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Guid>
    {
        private readonly AppDbContext _context;

        public CreateReservationCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var hasConflict = await _context.Reservations.AnyAsync(r => r.HallId == request.HallId &&
                                                                      r.StartTime < request.EndTime &&
                                                                      r.EndTime > request.StartTime, cancellationToken);
            if (hasConflict)
                throw new InvalidOperationException("Выбранное время уже занято.");

            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                HallId = request.HallId,
                ClientId = request.ClientId,
                StartTime = request.StartTime,
                EndTime = request.EndTime
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync(cancellationToken);

            return reservation.Id;
        }
    }
}
