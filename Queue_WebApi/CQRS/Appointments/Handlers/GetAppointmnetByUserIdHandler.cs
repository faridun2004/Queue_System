using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.CQRS.Appointments.Queries;
using Queue_WebApi.DTOs;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Appointments.Handlers
{
    public class GetAppointmentByUserIdHandler : IRequestHandler<GetAppointmentByUserId, List<AppointmentDto>>
    {
        private readonly QueueDbContext _context;

        public GetAppointmentByUserIdHandler(QueueDbContext context)
        {
            _context = context;
        }

        public async Task<List<AppointmentDto>> Handle(GetAppointmentByUserId request, CancellationToken cancellationToken)
        {
            return await _context.Apointsments
                .Where(a => a.UserId == request.UserId)
                .Where(predicate => !predicate.IsDeleted)
                .Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    HallName = a.Hall.Name,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime
                })
                .ToListAsync(cancellationToken);
        }
    }
}
