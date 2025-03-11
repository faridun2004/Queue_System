using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.DTOs;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Appointments.Queries
{
    public class GetAppointmentByIdHandler : IRequestHandler<GetAppointmentById, AppointmentDto>
    {
        private readonly QueueDbContext _context;

        public GetAppointmentByIdHandler(QueueDbContext context)
        {
            _context = context;
        }

        public async Task<AppointmentDto> Handle(GetAppointmentById query, CancellationToken cancellationToken)
        {
            var appointment = await _context.Apointsments
                .Include(a => a.User)  
                .Include(a => a.Hall)  
                .FirstOrDefaultAsync(a => a.Id == query.AppointmentId, cancellationToken); 

            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found");
            }

            if (appointment.User == null || appointment.Hall == null)
            {
                throw new InvalidOperationException("Appointment is missing required related entities (User or Hall).");
            }

            return new AppointmentDto
            {
                UserName = appointment.User.Name,
                GroupName = appointment.User.Group?.Name ?? "No Group",  
                HallName = appointment.Hall.Name,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime
            };
        }
    }
}

