using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.CQRS.Commands;
using Queue_WebApi.DataBase;
using Queue_WebApi.DTOs;
using Queue_WebApi.Models;

namespace Queue_WebApi.CQRS.Handlers
{
    public class CreateQueueCommandHndler:IRequestHandler<CreateQueueCommand, int>
    {
        private readonly QueueDbContext _dbContext;
        public CreateQueueCommandHndler(QueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(CreateQueueCommand command, CancellationToken cancellationToken)
        {
            var maxNum = await _dbContext.Queues.Where(
                q => q.HallId == command.hallId)
                .MaxAsync(q => (int?)q.Number, cancellationToken) ?? 0;
                
            var hall = await _dbContext.Halls.FindAsync(command.hallId);
            var newqueue = new Queue
            {
                HallId = command.hallId,
                Number = maxNum + 1
            };

            _dbContext.Queues.Add(newqueue);    
            await _dbContext.SaveChangesAsync(cancellationToken);

           return newqueue.Id;
        }
    }
}
