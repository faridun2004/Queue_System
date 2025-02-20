using MediatR;
using Queue_WebApi.CQRS.Commands;
using Queue_WebApi.DataBase;
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
            var hall = await _dbContext.Halls.FindAsync(command.hallId);
            if (hall == null) new Exception("not found");
            var queue = new Queue
            {
                HallId = command.hallId,
            };

            _dbContext.Queues.Add(queue);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return queue.Id;
        }
    }
}
