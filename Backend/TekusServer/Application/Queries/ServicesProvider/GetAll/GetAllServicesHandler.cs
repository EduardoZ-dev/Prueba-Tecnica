using Application.Abstractions.Persistence;
using Application.Abstractions;
using Application.DTOs;

namespace Application.Queries.ServicesProvider.GetAll
{
    internal sealed class GetAllServicesHandler : IQueryHandler<GetAllServicesQuery, List<ServiceDto>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetAllServicesHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<List<ServiceDto>> HandleAsync(GetAllServicesQuery query, CancellationToken cancellationToken = default)
        {
            var services = await _serviceRepository.GetAll(cancellationToken);

            return services.Select(s =>
                new ServiceDto(s.Name, s.HourlyRateUsd, s.Countries.ToList())).ToList();
        }
    }
}
