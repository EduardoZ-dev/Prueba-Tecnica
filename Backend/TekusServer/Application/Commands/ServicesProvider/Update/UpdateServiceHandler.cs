using Application.Abstractions;
using Application.Abstractions.Persistence;
using Domain.Common;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.ServicesProvider.Update
{
    internal sealed class UpdateServiceHandler : IRequestHandler<UpdateServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateServiceHandler(IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetById(command.Id, cancellationToken);

            if (service == null)
                throw new DomainException($"Service with ID {command.Id} was not found.");

            service.Update(command.Name, command.HourlyRateUsd);
            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
