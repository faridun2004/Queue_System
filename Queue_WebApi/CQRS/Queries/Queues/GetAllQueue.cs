using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.DataBase;
using Queue_WebApi.DTOs;

namespace Queue_WebApi.CQRS.Queries.Queues
{
    public record GetAllQueuesQuery() : IRequest<IEnumerable<QueueDto>>;

    public class GetAllQueuesQueryHandler : IRequestHandler<GetAllQueuesQuery, IEnumerable<QueueDto>>
    {
        private readonly QueueDbContext _context;

        public GetAllQueuesQueryHandler(QueueDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QueueDto>> Handle(GetAllQueuesQuery request, CancellationToken cancellationToken)
        {
            var queues = await _context.Queues
                .Include(q => q.Hall)
                .Include(q => q.Apointments)
                    .ThenInclude(a => a.Client)
                .Select(q => new QueueDto
                {
                    Id = q.Id,
                    HallId = q.HallId,
                    HallName = q.Hall.Name,
                    Number=q.Number,
                    Apointments = q.Apointments.Select(a => new ApointmentDto
                    {
                        Id = a.Id,
                        StartTime = a.StartTime,
                        EndTime = a.EndTime,
                        Description = a.Description,
                        ClientId = a.ClientId,
                        ClientName = a.Client.Name
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            return queues;
        }
    }




}
