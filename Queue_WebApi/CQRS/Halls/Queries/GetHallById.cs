using MediatR;
using Queue_WebApi.DTOs;

namespace Queue_WebApi.CQRS.Halls.Queries
{
    public record GetHallById(Guid Id) : IRequest<HallDto>;

}
