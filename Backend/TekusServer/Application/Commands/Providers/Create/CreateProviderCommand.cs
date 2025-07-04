﻿using MediatR;

namespace Application.Commands.Providers.Create
{
    public sealed record CreateProviderCommand(
        string Nit,
        string Name,
        string Email,
        List<CustomFieldDto> CustomFields,
        List<ServiceDto> Services
    ) : IRequest<Guid>;

    public sealed record CustomFieldDto(string Key, string Value);
    public sealed record ServiceDto(string Name, decimal HourlyRateUsd, List<string> Countries);

}
