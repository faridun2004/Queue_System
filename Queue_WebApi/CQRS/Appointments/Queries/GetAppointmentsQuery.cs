using MediatR;
using Queue_WebApi.DTOs;

namespace Queue_WebApi.CQRS.Appointments.Queries
{
    public record GetAppointmentsQuery() : IRequest<List<AppointmentDto>>;


}
