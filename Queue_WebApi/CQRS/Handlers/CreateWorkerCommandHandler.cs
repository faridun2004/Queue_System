using MediatR;
using Microsoft.AspNetCore.Mvc;
using Queue_WebApi.CQRS.Commands;
using Queue_WebApi.DataBase;
using Queue_WebApi.Models;

namespace Queue_WebApi.CQRS.Handlers
{
    public class CreateWorkerCommandHandler : IRequestHandler<CreateWorkerCommand, int>
    {
        private readonly QueueDbContext _dbContext;
        public CreateWorkerCommandHandler(QueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(CreateWorkerCommand command,CancellationToken cancellationToken)
        {
            var worker = new Worker
            {
                Name = command.Name,
                PhoneNumber = command.PhoneNumber
            };
            _dbContext.Workers.Add(worker);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return worker.Id;
        }
    }
}
