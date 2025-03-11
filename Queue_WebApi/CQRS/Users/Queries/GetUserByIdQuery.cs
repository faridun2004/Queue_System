using MediatR;
using Queue_WebApi.DTOs;
using Queue_WebApi.Models;

namespace Queue_WebApi.CQRS.Users.Queries
{
    public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>;

}
