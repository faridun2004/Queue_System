using MediatR;
using Queue_WebApi.CQRS.Appointments.Commands;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Appointments.Handlers
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand>
    {
        private readonly QueueDbContext _context;

        public DeleteAppointmentCommandHandler(QueueDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _context.Apointsments.FindAsync(request.AppointmentId , cancellationToken);
            if (appointment == null)
                throw new KeyNotFoundException("Appointment not found");

            _context.Apointsments.Remove(appointment);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
