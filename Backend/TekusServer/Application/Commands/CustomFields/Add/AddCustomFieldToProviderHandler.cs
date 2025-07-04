using Application.Abstractions.Persistence;
using Domain.Common;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.CustomFields.Add
{
    internal sealed class AddCustomFieldToProviderHandler : IRequestHandler<AddCustomFieldToProviderCommand>
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

        public async Task Handle(AddCustomFieldToProviderCommand command, CancellationToken cancellationToken)
        {
            var provider = await _providerRepository.GetById(command.ProviderId, cancellationToken);

            if (provider == null)
                throw new DomainException($"Provider with ID {command.ProviderId} was not found.");

            provider.AddCustomField(command.Key, command.Value);

            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
