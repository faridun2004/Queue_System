using MediatR;

namespace Queue_WebApi.CQRS.Commands
{
    public record AddApointmentCommand(
        DateTime StartTime,
        DateTime EndTime,
        string Description,
        int ClientId,
        int QueueId
        ) : IRequest<int>;
    
}
