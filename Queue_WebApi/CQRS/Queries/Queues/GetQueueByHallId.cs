using MediatR;
using Queue_WebApi.DTOs;

namespace Queue_WebApi.CQRS.Queries.Queues
{
    public record GetQueueByHallId(int HallId) : IRequest<QueueDto>;

}
