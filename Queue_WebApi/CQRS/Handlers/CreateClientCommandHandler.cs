using MediatR;
using Queue_WebApi.CQRS.Commands;
using Queue_WebApi.DataBase;
using Queue_WebApi.Models;

namespace Queue_WebApi.CQRS.Handlers
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
    {
        private readonly QueueDbContext _dbContext;
        public CreateClientCommandHandler(QueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(CreateClientCommand command, CancellationToken cancellationToken)
        {
            var client = new Client
            {
                Name = command.Name,
                PhoneNumber = command.PhoneNumber,
            };
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return client.Id;
        }
    }
}
