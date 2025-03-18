using MediatR;

namespace Queue_WebApi.CQRS.Groups.Commands
{
    public record DeleteGroupCommand(Guid GroupId) : IRequest;
    
}
