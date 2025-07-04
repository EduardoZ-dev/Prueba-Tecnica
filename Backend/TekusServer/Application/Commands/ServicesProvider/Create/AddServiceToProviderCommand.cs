using MediatR;

namespace Application.Commands.ServicesProvider.Create
{
    public sealed record AddServiceToProviderCommand(
        Guid ProviderId,
        string Name,
        decimal HourlyRateUsd,
        List<string> Countries
    ) : IRequest<Guid>;
}
