using MediatR;
using Queue_WebApi.DTOs;

namespace Queue_WebApi.CQRS.Commands
{
    public record CreateQueueCommand(
        int hallId      
        ):IRequest<int>;
    
}
