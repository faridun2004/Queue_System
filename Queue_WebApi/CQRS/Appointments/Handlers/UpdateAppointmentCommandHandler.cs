using MediatR;
using Queue_WebApi.CQRS.Appointments.Commands;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Appointments.Handlers
{
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand>
    {
        private readonly QueueDbContext _context;

        public UpdateAppointmentCommandHandler(QueueDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _context.Apointsments.FindAsync( request.AppointmentId , cancellationToken);
            if (appointment == null)
                throw new KeyNotFoundException("Appointment not found");
            appointment.Id = request.AppointmentId;
            appointment.StartTime = request.StartTime;
            appointment.EndTime = request.EndTime;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
