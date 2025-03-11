using MediatR;
using Queue_WebApi.CQRS.Halls.Commands;
using Queue_WebApi.DTOs;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Halls.Handlers
{
    public class UpdateHallCommandHandler : IRequestHandler<UpdateHallCommand, HallDto>
    {
        private readonly QueueDbContext _context;

        public UpdateHallCommandHandler(QueueDbContext context)
        {
            _context = context;
        }
        public async Task<HallDto> Handle(UpdateHallCommand request, CancellationToken cancellationToken)
        {
            var hall = await _context.Halls.FindAsync(request.Id, cancellationToken);

            if (hall == null)
                throw new KeyNotFoundException("Hall not found");

            hall.Name = request.Name;
            hall.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return  new HallDto
            {
                Id = hall.Id,
                Name = hall.Name
            };  
        }
    }
}
