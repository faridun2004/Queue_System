using MediatR;
using Queue_WebApi.Auth;
using Queue_WebApi.CQRS.Users.Commands;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Users.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserProvider _userProvider;
        private readonly QueueDbContext _context;

        public DeleteUserCommandHandler(QueueDbContext context, IUserProvider userProvider) // Added IUserProvider to constructor
        {
            _context = context;
            _userProvider = userProvider; 
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _userProvider.GetUserId();
            if (!userId.HasValue) throw new Exception("User is not authenticated");

            var user = await _context.Users.FindAsync(userId.Value);
            if (user == null) throw new KeyNotFoundException("User not found");
            user.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
