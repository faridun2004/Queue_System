using MediatR;

namespace Queue_WebApi.CQRS.Halls.Commands
{
    public record CreateHallCommand(
        string Name
        ) : IRequest<Guid>;
   
}
