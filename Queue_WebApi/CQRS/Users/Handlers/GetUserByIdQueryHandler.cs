using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.CQRS.Users.Queries;
using Queue_WebApi.DTOs;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Users.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly QueueDbContext _context;

        public GetUserByIdQueryHandler(QueueDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Group)
                .Where(v => v.IsDeleted==false)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null )
                throw new KeyNotFoundException("User not found");

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Name = user.Name,
                GroupId = user.GroupId,
                GroupName = user.Group?.Name
            };
        }
    }

}
