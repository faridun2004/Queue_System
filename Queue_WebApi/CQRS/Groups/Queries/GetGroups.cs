using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.DTOs;
using Queue_WebApi.Persistence.DataBase;

namespace Queue_WebApi.CQRS.Groups.Queries
{
    public record GetGroups() : IRequest<IEnumerable<GroupDto>>;
   
}
