using MediatR;
using Application.Abstractions.Persistence;
using Domain.Common;
using Domain.Exceptions;

namespace Application.Commands.Providers.DeleteProvider
{
    internal sealed class DeleteProviderHandler : IRequestHandler<DeleteProviderCommand>
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProviderHandler(IProviderRepository providerRepository, IUnitOfWork unitOfWork)
        {
            _providerRepository = providerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteProviderCommand command, CancellationToken cancellationToken)
        {
            var provider = await _providerRepository.GetById(command.Id, cancellationToken);

            if (provider == null)
                throw new DomainException($"Provider with ID {command.Id} was not found.");

            _providerRepository.Delete(provider);

            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
