using MediatR;

namespace Queue_WebApi.CQRS.Appointments.Commands
{
    public class RemoveExpiredAppointmentsCommand : IRequest
    {
        public DateTime Now { get; set; } = DateTime.UtcNow;
    }


}
