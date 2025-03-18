using MediatR;

namespace Queue_WebApi.CQRS.Halls.Commands
{
    public record DeleteHallCommand(Guid Id) : IRequest;
    
}
