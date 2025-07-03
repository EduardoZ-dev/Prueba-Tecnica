using Application.DTOs;
using Domain.Entities;


namespace Application.Abstractions.Persistence
{
    public interface IServiceRepository
    {
        Task<Service?> GetById(Guid id, CancellationToken cancellationToken = default);
        Task<List<Service>> GetAll(CancellationToken cancellationToken = default);
        Task Add(Service service, CancellationToken cancellationToken = default);
        Task Update(Service service, CancellationToken cancellationToken = default);
        void Delete(Service service);

        Task<Dictionary<string, int>> CountByCountry(CancellationToken cancellationToken = default);
        Task<List<TopServiceDto>> GetTopServices(CancellationToken cancellationToken = default);
    }
}
