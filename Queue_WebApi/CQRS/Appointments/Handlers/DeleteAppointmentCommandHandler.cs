using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.CQRS.Appointments.Commands.DeleteAppointments;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Appointments.Handlers
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand,bool>
    {
        private readonly QueueDbContext _context;

        public DeleteAppointmentCommandHandler(QueueDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _context.Apointsments
                .FirstOrDefaultAsync(a => a.Id == request.AppointmentId && a.UserId == request.UserId, cancellationToken);
            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found");
            }
            appointment.IsDeleted = true;   
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

    }

}
