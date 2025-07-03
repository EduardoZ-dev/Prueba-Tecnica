using Application.Abstractions;
using Application.Abstractions.Persistence;
using Domain.Common;

namespace Application.Commands.CustomFields.Add
{
    public sealed class AddCustomFieldToProviderHandler : ICommandHandler<AddCustomFieldToProviderCommand>
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddCustomFieldToProviderHandler(
            IProviderRepository providerRepository,
            IUnitOfWork unitOfWork)
        {
            _providerRepository = providerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(AddCustomFieldToProviderCommand command, CancellationToken cancellationToken = default)
        {
            var provider = await _providerRepository.GetById(command.ProviderId, cancellationToken);

            /*if (provider is null)
                throw new NotFoundException($"Provider with ID '{command.ProviderId}' not found.");*/

            provider.AddCustomField(command.Key, command.Value);

            _providerRepository.Update(provider);
            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
