using MediatR;

namespace Queue_WebApi.CQRS.Users.Commands
{
    public record UpdateUserCommand(
        Guid UserId, 
        string Username, 
        string Password, 
        string Name, 
        Guid GroupId
                        ) : IRequest;

}
