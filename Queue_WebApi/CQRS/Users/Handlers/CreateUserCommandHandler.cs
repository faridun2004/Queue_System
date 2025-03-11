using MediatR;
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
            var user = new User
            {
                Username = request.Username,
                Password = request.Password,
                Name = request.Name,
                Role = request.Role,
                GroupId = request.GroupId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }

}
