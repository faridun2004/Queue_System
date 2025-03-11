using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.CQRS.Halls.Queries;
using Queue_WebApi.DTOs;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Halls.Handlers
{
    public class GetHallsHandler : IRequestHandler<GetHalls, List<HallDto>>
    {
        private readonly QueueDbContext _context;
        public GetHallsHandler(QueueDbContext context)
        {
            _context = context;
        }
        public async Task<List<HallDto>> Handle(GetHalls request, CancellationToken cancellationToken)
        {
            var halls = await _context.Halls.ToListAsync(cancellationToken);
            return halls.Select(hall => new HallDto
            {
                Id = hall.Id,
                Name = hall.Name
            }).ToList();
        }
    }
}
