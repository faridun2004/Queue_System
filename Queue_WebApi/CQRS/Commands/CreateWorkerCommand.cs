using MediatR;

namespace Queue_WebApi.CQRS.Commands
{
    public record CreateWorkerCommand(
         string Name,
        string PhoneNumber
        ) :IRequest<int>;
   
}
