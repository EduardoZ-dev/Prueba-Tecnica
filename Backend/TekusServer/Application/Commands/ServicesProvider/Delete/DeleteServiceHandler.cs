using Application.Abstractions.Persistence;
using Domain.Common;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.ServicesProvider.Delete
{

    internal sealed class DeleteServiceHandler : IRequestHandler<DeleteServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceHandler(IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetById(command.Id, cancellationToken);

            if (service == null)
                throw new DomainException($"Service with ID {command.Id} was not found.");

            _serviceRepository.Delete(service);
            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
