using MediatR;

namespace Queue_WebApi.CQRS.Users.Commands
{
    public record DeleteUserCommand(Guid UserId) : IRequest;
}
