using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.CQRS.Users.Commands;
using Queue_WebApi.Models;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly QueueDbContext _context;

        public CreateUserCommandHandler(QueueDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users.Where(a => !a.IsDeleted)
                .SingleOrDefaultAsync(x => x.Username == request.Username, cancellationToken);

            if (existingUser != null)
            {
                throw new ArgumentException("Username is already taken.");
            }

            var group = await _context.Groups
                .SingleOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken);

            if (group == null)
            {
                throw new ArgumentException("Group not found.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                Username = request.Username,
                Password = hashedPassword,
                Name = request.Name,
                KeyWord = request.KeyWord,
                GroupId = request.GroupId,
                Role = "user",
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;
        }

    }
}
