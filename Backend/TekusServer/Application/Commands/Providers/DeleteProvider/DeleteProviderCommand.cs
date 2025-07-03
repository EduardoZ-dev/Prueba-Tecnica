using Application.Abstractions;

namespace Application.Commands.Providers.DeleteProvider
{
    public sealed record DeleteProviderCommand(Guid Id) : ICommand;
}
