using Application.Abstractions;
using Application.Abstractions.Persistence;
using Domain.Common;

namespace Application.Commands.ServicesProvider.Update
{
    internal sealed class UpdateServiceHandler : ICommandHandler<UpdateServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateServiceHandler(IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(UpdateServiceCommand command, CancellationToken cancellationToken = default)
        {
            var service = await _serviceRepository.GetById(command.Id, cancellationToken);
                          //?? throw new NotFoundException("Service", command.Id);

            service.Update(command.Name, command.HourlyRateUsd);

            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
