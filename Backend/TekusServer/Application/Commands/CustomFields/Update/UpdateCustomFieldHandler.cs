using Application.Abstractions;
using Application.Abstractions.Persistence;
using Domain.Common;

namespace Application.Commands.CustomFields.Update
{
    public sealed class UpdateCustomFieldHandler : ICommandHandler<UpdateCustomFieldCommand>
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

        public async Task HandleAsync(UpdateCustomFieldCommand command, CancellationToken cancellationToken = default)
        {
            var provider = await _providerRepository.GetById(command.ProviderId, cancellationToken)
                           ?? throw new KeyNotFoundException("Provider not found.");

            var customField = provider.CustomFields.FirstOrDefault(cf => cf.Id == command.CustomFieldId)
                              ?? throw new KeyNotFoundException("Custom field not found.");

            customField.Update(command.NewValue);

            await _providerRepository.Update(provider, cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
