using Application.Abstractions;
using Application.Abstractions.Persistence;
using Application.DTOs;


namespace Application.Queries.Summary.GetSummary
{
    internal sealed class GetSummaryHandler : IQueryHandler<GetSummaryQuery, SummaryDto>
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IServiceRepository _serviceRepository;

        public GetSummaryHandler(
            IProviderRepository providerRepository,
            IServiceRepository serviceRepository)
        {
            _providerRepository = providerRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<SummaryDto> HandleAsync(
            GetSummaryQuery query,
            CancellationToken cancellationToken = default)
        {
            var providersPerCountry = await _providerRepository.CountByCountry(cancellationToken);
            var servicesPerCountry = await _serviceRepository.CountByCountry(cancellationToken);

            return new SummaryDto(providersPerCountry, servicesPerCountry);
        }
    }
}
