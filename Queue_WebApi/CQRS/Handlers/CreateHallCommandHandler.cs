using MediatR;
using Queue_WebApi.CQRS.Commands;
using Queue_WebApi.DataBase;
using Queue_WebApi.Models;

namespace Queue_WebApi.CQRS.Handlers
{
    public class CreateHallCommandHandler:IRequestHandler<CreateHallCommand, int>
    {
        private readonly QueueDbContext _dbContext;
        public CreateHallCommandHandler(QueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(CreateHallCommand command, CancellationToken cancellationToken)
        {
            var hall = new Hall
            {
                Name = command.Name,
                Address = command.Address,
                PhoneNumber = command.Phone,
                workerId = command.workerId,

            };
            _dbContext.Halls.Add(hall);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return hall.Id;
        }
    }
}
