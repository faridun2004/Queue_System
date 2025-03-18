using MediatR;
using Queue_WebApi.CQRS.Halls.Commands;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Halls.Handlers
{
    public class DeleteHallCommandHandler : IRequestHandler<DeleteHallCommand>
    {
        private readonly QueueDbContext _context;

        public DeleteHallCommandHandler(QueueDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteHallCommand request, CancellationToken cancellationToken)
        {
            var hall = await _context.Halls.FindAsync(request.Id , cancellationToken);

            if (hall == null)
                throw new KeyNotFoundException("Hall not found");

            _context.Halls.Remove(hall);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
