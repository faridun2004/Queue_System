using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.CQRS.Users.Queries;
using Queue_WebApi.DTOs;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Users.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly QueueDbContext _context;

        public GetAllUsersQueryHandler(QueueDbContext context)
        {
            _context = context;
        }
        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users
                .Include(u => u.Group)
                .Where(a => !a.IsDeleted)
                .ToListAsync(cancellationToken);

            return users.Where(a => a.Role == "user")
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Name = u.Name,
                    GroupId = u.GroupId,
                    GroupName = u.Group?.Name
                }).ToList();
        }
    }
}
