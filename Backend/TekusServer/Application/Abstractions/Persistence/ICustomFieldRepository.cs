using Domain.Entities;

namespace Application.Abstractions.Persistence
{
    public interface ICustomFieldRepository
    {
        Task<CustomField?> GetById(Guid id, CancellationToken cancellationToken = default);
        Task<List<CustomField>> GetAll(CancellationToken cancellationToken = default);
        Task Add(CustomField customField, CancellationToken cancellationToken = default);
        Task Update(CustomField customField, CancellationToken cancellationToken = default);
        Task Delete(CustomField customField, CancellationToken cancellationToken = default);
    }
}
