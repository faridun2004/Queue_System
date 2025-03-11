using MediatR;

namespace Queue_WebApi.CQRS.Users.Commands
{
    public record CreateUserCommand(
        string Username, 
        string Password, 
        string Name,
        string Role,
        Guid GroupId
        ) : IRequest<Guid>;

}
