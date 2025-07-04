using MediatR;
using Application.Abstractions.Persistence;
using Application.DTOs;
using Domain.Exceptions;

namespace Application.Queries.ServicesProvider.GetById
{
    internal sealed class GetServiceByIdHandler : IRequestHandler<GetServiceByIdQuery, ServiceDto>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServiceByIdHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<ServiceDto> Handle(GetServiceByIdQuery query, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetById(query.Id, cancellationToken);

            if (service == null)
                throw new DomainException($"Service with ID {query.Id} was not found.");

            return new ServiceDto(service.Name, service.HourlyRateUsd, service.Countries.ToList());
        }
    }
}
