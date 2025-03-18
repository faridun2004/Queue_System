using MediatR;

namespace Queue_WebApi.CQRS.Users.Commands
{
    public record CreateUserCommand(
        string Username,
        string Password,
        string Name,
        Guid GroupId
        ) : IRequest<Guid>;

}
