using Application.Abstractions.Persistence;
using Domain.Common;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.CustomFields.Update
{
    internal sealed class UpdateCustomFieldHandler : IRequestHandler<UpdateCustomFieldCommand>
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomFieldHandler(
            IProviderRepository providerRepository,
            IUnitOfWork unitOfWork)
        {
            _providerRepository = providerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCustomFieldCommand command, CancellationToken cancellationToken)
        {
            var provider = await _providerRepository.GetById(command.ProviderId, cancellationToken);

            if (provider == null)
                throw new DomainException($"Provider with ID {command.ProviderId} was not found.");

            var customField = provider.CustomFields.FirstOrDefault(cf => cf.Id == command.CustomFieldId);
            if (customField == null)
                throw new DomainException($"Custom field with ID {command.CustomFieldId} was not found.");

            customField.Update(command.NewValue);

            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
