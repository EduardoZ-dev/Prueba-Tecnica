
using Application.Abstractions;

namespace Application.Commands.Providers.UpdateProvider
{
    public sealed record UpdateProviderCommand(Guid Id, string Name, string Email) : ICommand;
}
