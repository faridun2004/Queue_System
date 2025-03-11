using MediatR;

namespace Queue_WebApi.CQRS.Groups.Commands
{
    public record CreateGroupCommand(string Name) : IRequest<Guid>;

}
