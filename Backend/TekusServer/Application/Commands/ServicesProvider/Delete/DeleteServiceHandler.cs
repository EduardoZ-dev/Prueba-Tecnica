using Application.Abstractions;
using Application.Abstractions.Persistence;
using Domain.Common;

namespace Application.Commands.ServicesProvider.Delete
{
    internal sealed class DeleteServiceHandler : ICommandHandler<DeleteServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceHandler(IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(DeleteServiceCommand command, CancellationToken cancellationToken = default)
        {
            var service = await _serviceRepository.GetById(command.Id, cancellationToken);
            //if (service is null) throw new NotFoundException(...);

            _serviceRepository.Delete(service);
            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
