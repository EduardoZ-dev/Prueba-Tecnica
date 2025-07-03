using Domain.Entities;

namespace Application.Abstractions.Persistence
{
    public interface IProviderRepository
    {
        Task<Provider?> GetById(Guid id, CancellationToken cancellationToken = default);
        Task<List<Provider>> GetAll(CancellationToken cancellationToken = default);
        Task Add(Provider provider, CancellationToken cancellationToken = default);
        Task Update(Provider provider, CancellationToken cancellationToken = default);
        void Delete(Provider provider);
        Task<bool> Exists(Guid id, CancellationToken cancellationToken = default);
        Task<Dictionary<string, int>> CountByCountry(CancellationToken cancellationToken = default);
        Task<Dictionary<string, int>> CountServicesPerProvider(CancellationToken cancellationToken = default);
        Task<List<Provider>> SearchAsync(string? search, string? sortBy, int page, int pageSize, CancellationToken cancellationToken = default);

    }
}
