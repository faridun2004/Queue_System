using Appointment_Hall.Data;
using Appointment_Hall.Use_Cases.Reservations.Commands;
using MediatR;

namespace Appointment_Hall.Use_Cases.Reservations.Handlers
{
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, Unit>
    {
        private readonly AppDbContext _context;

        public DeleteReservationCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations.FindAsync(new object[] { request.ReservationId }, cancellationToken)
                               ?? throw new InvalidOperationException("Бронирование не найдено.");

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
