using Application.Abstractions.Persistence;
using Application.Abstractions;
using Domain.Common;
using Domain.Entities;

namespace Application.Commands.ServicesProvider.Create
{
    internal sealed class CreateServiceHandler : ICommandHandler<CreateServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateServiceHandler(
            IServiceRepository serviceRepository,
            IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(
            CreateServiceCommand command,
            CancellationToken cancellationToken = default)
        {
            var service = Service.Create(
                command.Name,
                command.HourlyRateUsd,
                command.Countries);

            await _serviceRepository.Add(service, cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
