using Application.Abstractions;

namespace Application.Commands.ServicesProvider.Create
{
    public sealed record CreateServiceCommand(
        string Name,
        decimal HourlyRateUsd,
        List<string> Countries
    ) : ICommand;
}
