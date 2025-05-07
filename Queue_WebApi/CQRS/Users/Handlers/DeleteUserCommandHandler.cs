using MediatR;
using Queue_WebApi.Auth;
using Queue_WebApi.CQRS.Users.Commands;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Users.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly QueueDbContext _context;

        public DeleteUserCommandHandler(QueueDbContext context)
        {
            _context = context;      
        }
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //var userId = _userProvider.GetUserId();
            //if (!userId.HasValue) throw new Exception("User is not authenticated");

            var user= await _context.Users.FindAsync(request.id, cancellationToken);
            if (user == null) throw new KeyNotFoundException("User not found");
            user.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
