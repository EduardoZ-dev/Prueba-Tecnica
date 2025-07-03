using Application.Abstractions;
using Application.Abstractions.Persistence;
using Application.DTOs;

namespace Application.Queries.ServicesProvider.GetById
{
    internal sealed class GetServiceByIdHandler : IQueryHandler<GetServiceByIdQuery, ServiceDto>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServiceByIdHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<ServiceDto> HandleAsync(GetServiceByIdQuery query, CancellationToken cancellationToken = default)
        {
            var service = await _serviceRepository.GetById(query.Id, cancellationToken);
                          //?? throw new NotFoundException("Service", query.Id);

            return new ServiceDto(service.Name, service.HourlyRateUsd, service.Countries.ToList());
        }
    }
}
