using MediatR;
using Queue_WebApi.DTOs;

namespace Queue_WebApi.CQRS.Halls.Queries
{
    public class GetHalls(): IRequest<List<HallDto>>;

}
