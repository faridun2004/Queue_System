using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.DTOs;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Appointments.Queries
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
                .Select(a => new AppointmentDto
                {
                    UserName = a.User != null ? a.User.Name : "Неизвестный пользователь",
                    GroupName = a.User != null && a.User.Group != null ? a.User.Group.Name : "Без группы",
                    HallName = a.Hall != null ? a.Hall.Name : "Неизвестный зал",
                    StartTime = a.StartTime,
                    EndTime = a.EndTime
                })
                .ToListAsync(cancellationToken);
        }
    }

}
