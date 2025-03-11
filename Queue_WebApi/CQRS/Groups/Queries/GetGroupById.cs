using MediatR;
using Queue_WebApi.DTOs;

namespace Queue_WebApi.CQRS.Groups.Queries
{
    public record GetGroupById(Guid GroupId) : IRequest<GroupDto>;

}
