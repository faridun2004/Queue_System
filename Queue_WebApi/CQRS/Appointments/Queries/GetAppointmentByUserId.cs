using MediatR;
using Queue_WebApi.DTOs;

namespace Queue_WebApi.CQRS.Appointments.Queries
{
    public record GetAppointmentByUserId(Guid UserId) : IRequest<List<AppointmentDto>>
    {
    }

}
