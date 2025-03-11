using MediatR;
using Queue_WebApi.CQRS.Groups.Commands;
using Queue_WebApi.DTOs;
using Queue_WebApi.Models;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Groups.Handlers
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Guid>
    {
        private readonly QueueDbContext _context;
        public CreateGroupCommandHandler(QueueDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateGroupCommand command, CancellationToken cancellationToken)
        {
            var group = new Group
            {
                Id = Guid.NewGuid(),
                Name = command.Name
            };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync(cancellationToken);
            return group.Id;
        }
    }
}
