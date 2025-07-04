using MediatR;
using Application.Abstractions.Persistence;
using Domain.Common;
using Domain.ValueObjects;

namespace Application.Commands.Providers.UpdateProvider
{
    internal sealed class UpdateProviderHandler : IRequestHandler<UpdateProviderCommand>
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProviderHandler(IProviderRepository providerRepository, IUnitOfWork unitOfWork)
        {
            _providerRepository = providerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateProviderCommand command, CancellationToken cancellationToken)
        {
            var provider = await _providerRepository.GetById(command.Id, cancellationToken);

            if (provider == null)
                throw new KeyNotFoundException($"Provider with ID {command.Id} was not found.");

            var email = Email.Create(command.Email);
            provider.Update(command.Name, email);

            // ✅ Limpiar los campos personalizados existentes
            provider.ClearCustomFields();

            // ✅ Agregar los nuevos campos personalizados que vienen del comando
            foreach (var field in command.CustomFields)
            {
                provider.AddCustomField(field.Key, field.Value);
            }

            // ✅ Limpiar y actualizar los servicios (opcional, si también se actualizan)
            provider.ClearServices();
            foreach (var service in command.Services)
            {
                provider.AddService(service.Name, service.HourlyRateUsd, service.Countries);
            }

            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}