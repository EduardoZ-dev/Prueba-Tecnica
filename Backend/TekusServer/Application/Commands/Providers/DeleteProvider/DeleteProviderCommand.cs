using MediatR;

namespace Application.Commands.Providers.DeleteProvider
{
    public sealed record DeleteProviderCommand(Guid Id) : IRequest;
}
