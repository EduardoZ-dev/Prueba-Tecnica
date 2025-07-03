using Application.Abstractions;
using Application.Abstractions.Persistence;
using Domain.Common;

namespace Application.Commands.Providers.DeleteProvider
{
    internal sealed class DeleteProviderHandler : ICommandHandler<DeleteProviderCommand>
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProviderHandler(
            IProviderRepository providerRepository,
            IUnitOfWork unitOfWork)
        {
            _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task HandleAsync(DeleteProviderCommand command, CancellationToken cancellationToken = default)
        {
            var provider = await _providerRepository.GetById(command.Id, cancellationToken);

            /*if (provider is null)
            {
                throw new NotFoundException($"Provider with ID {command.Id} was not found.");
            }*/

            _providerRepository.Delete(provider);

            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
