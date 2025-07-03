using Application.Abstractions.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class CustomFieldRepository : ICustomFieldRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomFieldRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(CustomField customField, CancellationToken cancellationToken = default)
        {
            await _context.CustomFields.AddAsync(customField, cancellationToken);
        }

        public async Task<List<CustomField>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _context.CustomFields.ToListAsync(cancellationToken);
        }

        public async Task<CustomField?> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.CustomFields.FirstOrDefaultAsync(cf => cf.Id == id, cancellationToken);
        }

        public Task Update(CustomField customField, CancellationToken cancellationToken = default)
        {
            _context.CustomFields.Update(customField);
            return Task.CompletedTask;
        }

        public Task Delete(CustomField customField, CancellationToken cancellationToken = default)
        {
            _context.CustomFields.Remove(customField);
            return Task.CompletedTask;
        }
    }
}
