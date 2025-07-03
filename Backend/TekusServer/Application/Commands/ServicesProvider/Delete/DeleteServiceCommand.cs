using Application.Abstractions;

namespace Application.Commands.ServicesProvider.Delete
{
    public sealed record DeleteServiceCommand(Guid Id) : ICommand;
}
