using MediatR;
using Queue_WebApi.CQRS.Halls.Commands;
using Queue_WebApi.Models;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Halls.Handlers
{
    public class CreateHallCommandHandler: IRequestHandler<CreateHallCommand, Guid>
    {
        private readonly QueueDbContext _context;
        public CreateHallCommandHandler(QueueDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateHallCommand request, CancellationToken cancellationToken)
        {
            var hall = new Hall
            {
                Name = request.Name
            };
            _context.Halls.Add(hall);
            await _context.SaveChangesAsync();
            return hall.Id;
        }
    }
}
