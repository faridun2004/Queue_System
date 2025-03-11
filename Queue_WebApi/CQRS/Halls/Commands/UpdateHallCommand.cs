using MediatR;
using Queue_WebApi.DTOs;

namespace Queue_WebApi.CQRS.Halls.Commands
{
    public record UpdateHallCommand(Guid Id, string Name) : IRequest<HallDto>;

}
