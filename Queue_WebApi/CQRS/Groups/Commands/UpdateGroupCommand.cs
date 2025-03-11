using MediatR;

namespace Queue_WebApi.CQRS.Groups.Commands
{
    public record UpdateGroupCommand(Guid GroupId, string Name) : IRequest;

}
