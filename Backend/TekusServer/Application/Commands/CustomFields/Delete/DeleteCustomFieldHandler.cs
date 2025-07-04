using Application.Abstractions.Persistence;
using Application.Abstractions;
using Domain.Common;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.CustomFields.Delete
{
    internal sealed class DeleteCustomFieldHandler : IRequestHandler<DeleteCustomFieldCommand>
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomFieldHandler(
            IProviderRepository providerRepository,
            IUnitOfWork unitOfWork)
        {
            _providerRepository = providerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteCustomFieldCommand command, CancellationToken cancellationToken)
        {
            var provider = await _providerRepository.GetById(command.ProviderId, cancellationToken);

            if (provider == null)
                throw new DomainException($"Provider with ID {command.ProviderId} was not found.");

            var removed = provider.RemoveCustomField(command.CustomFieldId);
            if (!removed)
                throw new DomainException($"Custom field with ID {command.CustomFieldId} was not found.");

            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
