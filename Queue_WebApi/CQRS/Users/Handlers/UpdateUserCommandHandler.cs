using MediatR;
using Queue_WebApi.CQRS.Users.Commands;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Users.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly QueueDbContext _context;

        public UpdateUserCommandHandler(QueueDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId , cancellationToken);

            if (user == null)
                throw new KeyNotFoundException("User not found");
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Username = request.Username;
            user.Password = hashedPassword;
            user.Name = request.Name;
            user.GroupId = request.GroupId;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
