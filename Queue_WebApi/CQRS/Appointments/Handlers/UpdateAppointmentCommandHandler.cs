using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.CQRS.Appointments.Commands.UpdateAppointments;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Appointments.Handlers
{
    public class UpdateAppointmentByUserIdCommandHandler : IRequestHandler<UpdateAppointmentCommand, bool>
    {
        private readonly QueueDbContext _context;

        public UpdateAppointmentByUserIdCommandHandler(QueueDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _context.Apointsments.FindAsync(request.AppointmentId);
            if (appointment == null || appointment.UserId != request.UserId)
                return false;

            await using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable, cancellationToken);
            try
            {
                bool isOccupied = await _context.Apointsments
                    .AnyAsync(a => a.HallId == request.HallId &&
                                   a.Id != request.AppointmentId && 
                                   (
                                       (request.StartTime >= a.StartTime && request.StartTime < a.EndTime) ||  
                                       (request.EndTime > a.StartTime && request.EndTime <= a.EndTime) ||     
                                       (request.StartTime <= a.StartTime && request.EndTime >= a.EndTime)      
                                   ),
                              cancellationToken);

                if (isOccupied)
                    throw new InvalidOperationException("The time slot is already occupied.");
                else
                {
                    appointment.HallId = request.HallId;
                    appointment.StartTime = request.StartTime;
                    appointment.EndTime = request.EndTime;

                    await _context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return true;
                }
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }




}


