using MediatR;
using Queue_WebApi.CQRS.Groups.Commands;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Groups.Handlers
{
    public class DeleteGroupsCommandHandler : IRequest
    {
       private readonly QueueDbContext _context;
        public DeleteGroupsCommandHandler(QueueDbContext context)
        {
            _context = context;
        }
        public async Task Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.FindAsync(request.GroupId, cancellationToken);
            if (group == null)
                throw new KeyNotFoundException("Group not found");
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
