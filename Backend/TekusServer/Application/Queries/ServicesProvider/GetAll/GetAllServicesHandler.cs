using MediatR;
using Application.Abstractions.Persistence;
using Application.DTOs;

namespace Application.Queries.ServicesProvider.GetAll
{
    internal sealed class GetAllServicesHandler : IRequestHandler<GetAllServicesQuery, List<ServiceDto>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetAllServicesHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<List<ServiceDto>> Handle(GetAllServicesQuery query, CancellationToken cancellationToken)
        {
            var services = await _serviceRepository.GetAll(cancellationToken);

            return services.Select(s =>
                new ServiceDto(s.Name, s.HourlyRateUsd, s.Countries.ToList())).ToList();
        }
    }
}
