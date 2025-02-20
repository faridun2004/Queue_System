using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.DataBase;
using Queue_WebApi.DTOs;

namespace Queue_WebApi.CQRS.Queries.Queues
{
    public class GetQueueByHallIdHandler : IRequestHandler<GetQueueByHallId, QueueDto>
    {
        private readonly QueueDbContext _dbContext;

        public GetQueueByHallIdHandler(QueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QueueDto> Handle(GetQueueByHallId query, CancellationToken cancellationToken)
        {
            var queue = await _dbContext.Queues
                .Include(q => q.Hall)
                .Include(q => q.Apointments)
                .FirstOrDefaultAsync(q => q.HallId == query.HallId, cancellationToken);

            if (queue == null)
            {
                throw new KeyNotFoundException($"Queue with hallId {query.HallId} not found.");
            }

            return new QueueDto
            {
                Id = queue.Id,
                HallId = queue.HallId,
                HallName = queue.Hall?.Name,
                Apointments = queue.Apointments.Select(a => new ApointmentDto
                {
                    Id = a.Id,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    Description = a.Description
                }).ToList()
            };
        }
    }

}
