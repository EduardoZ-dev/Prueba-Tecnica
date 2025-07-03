using Application.Abstractions;
using Application.Abstractions.Persistence;
using Domain.ValueObjects;
using Domain.Common;

namespace Application.Commands.Providers.UpdateProvider
{
    internal sealed class UpdateProviderCommandHandler : ICommandHandler<UpdateProviderCommand>
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProviderCommandHandler(
            IProviderRepository providerRepository,
            IUnitOfWork unitOfWork)
        {
            _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task HandleAsync(UpdateProviderCommand command, CancellationToken cancellationToken = default)
        {
            var provider = await _providerRepository.GetById(command.Id, cancellationToken);

            /*if (provider is null)
            {
                throw new NotFoundException($"Provider with ID {command.Id} was not found.");
            }*/

            var email = Email.Create(command.Email);

            provider.Update(command.Name, email);

            _providerRepository.Update(provider);

            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}