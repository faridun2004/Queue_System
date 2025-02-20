using MediatR;

namespace Queue_WebApi.CQRS.Commands
{
    public record CreateHallCommand(
        string Name,
        string Address,
        string Phone,
        int workerId
        ):IRequest<int>;
  
}
