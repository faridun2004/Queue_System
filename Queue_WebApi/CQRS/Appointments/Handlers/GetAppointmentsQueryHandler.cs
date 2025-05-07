using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.CQRS.Appointments.Queries;
using Queue_WebApi.DTOs;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Appointments.Handlers
{
    public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, List<AppointmentDto>>
    {
        private readonly QueueDbContext _context;

        public GetAppointmentsQueryHandler(QueueDbContext context)
        {
            _context = context;
        }
        public async Task<List<AppointmentDto>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Apointsments
                .Include(a => a.Hall)
                .Include(a => a.User)
                .ThenInclude(u => u.Group)
                .Where(a => a.EndTime > DateTime.Now)
                .Select(a => new AppointmentDto
                {
                    UserName = a.User.Name,
                    GroupName = a.User.Group.Name,
                    HallName = a.Hall.Name,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime
                })
                .ToListAsync(cancellationToken);
        }

    }
}
