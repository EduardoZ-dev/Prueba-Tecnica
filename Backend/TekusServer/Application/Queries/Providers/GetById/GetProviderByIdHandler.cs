using MediatR;
using Application.Abstractions.Persistence;
using Application.DTOs;

namespace Application.Queries.Providers.GetById
{
    internal sealed class GetProviderByIdHandler : IRequestHandler<GetProviderByIdQuery, ProviderDto>
    {
        private readonly IProviderRepository _providerRepository;

        public GetProviderByIdHandler(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<ProviderDto> Handle(GetProviderByIdQuery query, CancellationToken cancellationToken)
        {
            var provider = await _providerRepository.GetById(query.Id, cancellationToken);

            if (provider == null)
                throw new KeyNotFoundException($"Provider with ID {query.Id} was not found.");

            return new ProviderDto(
                Id: provider.Id,
                Nit: provider.Nit,
                Name: provider.Name,
                Email: provider.Email.Value,
                CustomFields: provider.CustomFields
                    .Select(cf => new CustomFieldDto(cf.Key, cf.Value))
                    .ToList(),
                Services: provider.Services
                    .Select(s => new ServiceDto(s.Name, s.HourlyRateUsd, s.Countries.ToList()))
                    .ToList()
            );
        }
    }
}
