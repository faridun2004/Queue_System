using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.CQRS.Commands;
using Queue_WebApi.DataBase;
using Queue_WebApi.Models;

namespace Queue_WebApi.CQRS.Handlers
{
    public class AddApointmentCommandHandler : IRequestHandler<AddApointmentCommand, int>
    {
        private readonly QueueDbContext _dbContext;
        public AddApointmentCommandHandler(QueueDbContext queueDbContext)
        {
            _dbContext = queueDbContext;
        }
        public async Task<int> Handle(AddApointmentCommand command, CancellationToken cancellationToken)
        {
    
            bool isOccupied = await _dbContext.Apointsments
                .AnyAsync(a => a.QueueId == command.QueueId &&
                              
                               ((command.StartTime >= a.StartTime && command.StartTime < a.EndTime) || 
                                (command.EndTime > a.StartTime && command.EndTime <= a.EndTime) ||    
                                (command.StartTime <= a.StartTime && command.EndTime >= a.EndTime)),  
                          cancellationToken);

            if (isOccupied)
            {
                throw new InvalidOperationException("The time slot is already occupied.");
            }
            var client=await _dbContext.Clients.FindAsync(command.ClientId);
            if (client == null) throw new Exception("Client not dound");
            var queue=await _dbContext.Queueas.FindAsync(command.QueueId);
            if (queue == null) throw new Exception("queuer not found");
            var apointment = new Apointment
            {
                StartTime = command.StartTime,
                EndTime = command.EndTime,
                Description = command.Description,
                ClientId = command.ClientId,
                QueueId = command.QueueId
            };

            _dbContext.Apointsments.Add(apointment);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return apointment.Id;
        }

    }
}
