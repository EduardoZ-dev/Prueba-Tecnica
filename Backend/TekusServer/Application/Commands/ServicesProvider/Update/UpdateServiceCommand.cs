using MediatR;

namespace Application.Commands.ServicesProvider.Update
{
    public sealed record UpdateServiceCommand(
        Guid Id,
        string Name,
        decimal HourlyRateUsd
    ) : IRequest;
}
