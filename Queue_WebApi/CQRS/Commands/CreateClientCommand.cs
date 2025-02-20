using MediatR;

namespace Queue_WebApi.CQRS.Commands
{
    public record CreateClientCommand(
        string Name,
        string PhoneNumber
        ) : IRequest<int>;

}
