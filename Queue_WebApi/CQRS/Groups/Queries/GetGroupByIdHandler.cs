using MediatR;
using Queue_WebApi.DTOs;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Groups.Queries
{
    public class GetGroupByIdHandler : IRequestHandler<GetGroupById, GroupDto>
    {
        private readonly QueueDbContext _context;
        public GetGroupByIdHandler(QueueDbContext context)
        {
            _context = context;
        }
        public async Task<GroupDto> Handle(GetGroupById request, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.FindAsync(request.GroupId, cancellationToken);
            if (group == null)
                throw new KeyNotFoundException("Group not found");
            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name
            };
        }
    }
}
