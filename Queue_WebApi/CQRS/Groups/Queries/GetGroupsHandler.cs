using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.DTOs;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Groups.Queries
{
    public class GetGroupsHandler : IRequestHandler<GetGroups, IEnumerable<GroupDto>>
    {
        private readonly QueueDbContext _context;   
        public GetGroupsHandler(QueueDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<GroupDto>> Handle(GetGroups request, CancellationToken cancellationToken)
        {
            var groups = await _context.Groups.ToListAsync(cancellationToken);
            return groups.Select(g => new GroupDto
            {
                Id = g.Id,
                Name = g.Name
            });
        }
    }
}
