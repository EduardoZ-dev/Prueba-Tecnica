using Application.Abstractions.Persistence;
using Application.Abstractions;
using Domain.Common;

namespace Application.Commands.CustomFields.Delete
{
    internal sealed class DeleteCustomFieldHandler : ICommandHandler<DeleteCustomFieldCommand>
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

        public async Task HandleAsync(DeleteCustomFieldCommand command, CancellationToken cancellationToken = default)
        {
            var provider = await _providerRepository.GetById(command.ProviderId, cancellationToken)
                           ?? throw new KeyNotFoundException("Provider not found.");

            var removed = provider.RemoveCustomField(command.CustomFieldId);
            if (!removed)
                throw new KeyNotFoundException("Custom field not found.");

            await _providerRepository.Update(provider, cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
