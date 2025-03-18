using MediatR;
using Queue_WebApi.DTOs;

namespace Queue_WebApi.CQRS.Groups.Commands
{
    public record UpdateGroupCommand(Guid Id, string Name) : IRequest<GroupDto>;

}
