using Appointment_Hall.Data;
using Appointment_Hall.Use_Cases.Reservations.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Hall.Use_Cases.Reservations.Handlers
{
    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, Unit>
    {
        private readonly AppDbContext _context;

        public UpdateReservationCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations.FindAsync(new object[] { request.ReservationId }, cancellationToken)
                               ?? throw new InvalidOperationException("Бронирование не найдено.");

            var hasConflict = await _context.Reservations.AnyAsync(r => r.HallId == reservation.HallId &&
                                                                      r.Id != reservation.Id &&
                                                                      r.StartTime < request.EndTime &&
                                                                      r.EndTime > request.StartTime, cancellationToken);
            if (hasConflict)
                throw new InvalidOperationException("Выбранное время уже занято.");

            reservation.StartTime = request.StartTime;
            reservation.EndTime = request.EndTime;

            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
