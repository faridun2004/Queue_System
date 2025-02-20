using MediatR;

namespace Queue_WebApi.CQRS.Commands
{
    public record CreateQueueCommand(
        int hallId      
        ):IRequest<int>;
    
}
