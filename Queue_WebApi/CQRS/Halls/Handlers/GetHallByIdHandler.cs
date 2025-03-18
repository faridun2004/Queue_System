using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.CQRS.Halls.Queries;
using Queue_WebApi.DTOs;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Halls.Handlers
{
    public class GetHallByIdQueryHandler : IRequestHandler<GetHallById, HallDto>
    {
        private readonly QueueDbContext _context;
        public GetHallByIdQueryHandler(QueueDbContext context)
        {
            _context = context;
        }
        public async Task<HallDto> Handle(GetHallById request, CancellationToken cancellationToken)
        {
            var hall = await _context.Halls
                .FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);

            if (hall == null)
                throw new KeyNotFoundException("Hall not found");
            return new HallDto
            {
                Id = hall.Id,
                Name = hall.Name
            };
        }
    }
}
