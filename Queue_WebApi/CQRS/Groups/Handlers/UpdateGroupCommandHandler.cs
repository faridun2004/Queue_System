using MediatR;
using Queue_WebApi.CQRS.Groups.Commands;
using Queue_WebApi.DTOs;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Groups.Handlers
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, GroupDto>
    {
        private readonly QueueDbContext _context;
        public UpdateGroupCommandHandler(QueueDbContext context)
        {
            _context = context;
        }
        public async Task<GroupDto> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.FindAsync(request.Id, cancellationToken);
            if (group == null)
                throw new KeyNotFoundException("Group not found");
            group.Name = request.Name;
            group.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name
            };
        }
    }
}
