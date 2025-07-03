using Application.Abstractions;
using Application.Abstractions.Persistence;
using Application.DTOs;


namespace Application.Queries.Dashboard
{
    internal sealed class GetDashboardHandler : IQueryHandler<GetDashboardQuery, DashboardDto>
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IServiceRepository _serviceRepository;

        public GetDashboardHandler(
            IProviderRepository providerRepository,
            IServiceRepository serviceRepository)
        {
            _providerRepository = providerRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<DashboardDto> HandleAsync(GetDashboardQuery query, CancellationToken cancellationToken = default)
        {
            var servicesPerCountry = await _serviceRepository.CountByCountry(cancellationToken);
            var providersPerCountry = await _providerRepository.CountByCountry(cancellationToken);
            var topServices = await _serviceRepository.GetTopServices(cancellationToken);
            var servicesPerProvider = await _providerRepository.CountServicesPerProvider(cancellationToken);

            var countryWithMostServices = servicesPerCountry
                .OrderByDescending(kvp => kvp.Value)
                .Select(kvp => kvp.Key)
                .FirstOrDefault();

            return new DashboardDto(
                ServicesPerCountry: servicesPerCountry,
                ProvidersPerCountry: providersPerCountry,
                TopServices: topServices,
                ServicesPerProvider: servicesPerProvider,
                CountryWithMostServices: countryWithMostServices
            );
        }
    }
}
