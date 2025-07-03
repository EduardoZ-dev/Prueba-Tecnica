using Application.Abstractions;
using Application.Abstractions.Persistence;
using Domain.Common;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Commands.Providers.Create
{
    internal sealed class CreateProviderHandler : ICommandHandler<CreateProviderCommand>
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProviderHandler(IProviderRepository providerRepository, IUnitOfWork unitOfWork)
        {
            _providerRepository = providerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(CreateProviderCommand command, CancellationToken cancellationToken = default)
        {
            var email = Email.Create(command.Email);
            var provider = new Provider(command.Nit, command.Name, email);

            // Agregar campos personalizados
            foreach (var field in command.CustomFields)
            {
                provider.AddCustomField(field.Key, field.Value);
            }

            // Agregar servicios
            foreach (var serviceDto in command.Services)
            {
                var service = Service.Create(serviceDto.Name, serviceDto.HourlyRateUsd, serviceDto.Countries);
                provider.AddService(service);
            }

            await _providerRepository.Add(provider, cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
