using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.CQRS.Appointments.Commands;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Appointments.Handlers
{
    public class RemoveExpiredAppointmentsCommandHandler : IRequestHandler<RemoveExpiredAppointmentsCommand>
    {
        private readonly QueueDbContext _context;
        public RemoveExpiredAppointmentsCommandHandler(QueueDbContext context)
        {
            _context = context;
        }
        public async Task Handle(RemoveExpiredAppointmentsCommand request, CancellationToken cancellationToken)
        {
            var now = request.Now;
            var expiredAppointments = await _context.Apointsments
                .Where(a => a.EndTime < now && !a.IsExpired) 
                .ToListAsync(cancellationToken);
            if (expiredAppointments.Any())
            {
                foreach (var appointment in expiredAppointments)
                {
                    appointment.IsExpired = true;
                    _context.Apointsments.Remove(appointment);
                }
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }

}
