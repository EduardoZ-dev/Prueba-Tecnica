using Application.Abstractions.Persistence;
using Domain.Common;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.ServicesProvider.Create
{
    internal sealed class AddServiceToProviderHandler : IRequestHandler<AddServiceToProviderCommand, Guid>
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddServiceToProviderHandler(
            IProviderRepository providerRepository,
            IUnitOfWork unitOfWork)
        {
            _providerRepository = providerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AddServiceToProviderCommand command, CancellationToken cancellationToken)
        {
            var provider = await _providerRepository.GetById(command.ProviderId, cancellationToken)
                           ?? throw new DomainException($"Provider with ID {command.ProviderId} was not found.");

            var service = provider.AddService(command.Name, command.HourlyRateUsd, command.Countries);

            await _unitOfWork.SaveChanges(cancellationToken);
            return service.Id;
        }
    }
}
