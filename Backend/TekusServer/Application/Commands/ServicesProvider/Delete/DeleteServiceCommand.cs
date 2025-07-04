using MediatR;

namespace Application.Commands.ServicesProvider.Delete
{
    public sealed record DeleteServiceCommand(Guid Id) : IRequest;
}
