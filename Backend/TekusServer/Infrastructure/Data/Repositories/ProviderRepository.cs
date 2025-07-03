using Application.Abstractions.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly ApplicationDbContext _context;

        public ProviderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Provider provider, CancellationToken cancellationToken = default) =>
            await _context.Providers.AddAsync(provider, cancellationToken);

        public void Delete(Provider provider)
        {
            _context.Providers.Remove(provider);
        }

        public async Task<List<Provider>> GetAll(CancellationToken cancellationToken = default) =>
            await _context.Providers
                .Include(p => p.CustomFields)
                .Include(p => p.Services)
                .ToListAsync(cancellationToken);

        public async Task<Provider?> GetById(Guid id, CancellationToken cancellationToken = default) =>
            await _context.Providers
                .Include(p => p.CustomFields)
                .Include(p => p.Services)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public Task<bool> Exists(Guid id, CancellationToken cancellationToken = default) =>
            _context.Providers.AnyAsync(p => p.Id == id, cancellationToken);

        public async Task Update(Provider provider, CancellationToken cancellationToken = default)
        {
            var trackedProvider = await _context.Providers
                .Include(p => p.CustomFields)
                .Include(p => p.Services)
                .FirstOrDefaultAsync(p => p.Id == provider.Id, cancellationToken);

            if (trackedProvider is null)
            {
                _context.Providers.Attach(provider);
                _context.Entry(provider).State = EntityState.Modified;
            }
            else
            {
                _context.Entry(trackedProvider).CurrentValues.SetValues(provider);
            }
        }

        public async Task<Dictionary<string, int>> CountByCountry(CancellationToken cancellationToken = default)
        {
            return await _context.Providers
                .SelectMany(p => p.Services)
                .SelectMany(s => s.Countries)
                .GroupBy(c => c)
                .ToDictionaryAsync(g => g.Key, g => g.Count(), cancellationToken);
        }

        public async Task<Dictionary<string, int>> CountServicesPerProvider(CancellationToken cancellationToken = default)
        {
            return await _context.Providers
                .Select(p => new { p.Name, ServiceCount = p.Services.Count })
                .ToDictionaryAsync(p => p.Name, p => p.ServiceCount, cancellationToken);
        }

        public async Task<List<Provider>> SearchAsync(
            string? search,
            string? sortBy,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Providers
                .Include(p => p.CustomFields)
                .Include(p => p.Services)
                .AsQueryable();

            // Filtro por búsqueda
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    p.Name.Contains(search) ||
                    p.Nit.Contains(search) ||
                    p.Email.Value.Contains(search));
            }

            // Ordenamiento
            query = sortBy?.ToLower() switch
            {
                "name_desc" => query.OrderByDescending(p => p.Name),
                "name" or _ => query.OrderBy(p => p.Name)
            };

            // Paginación
            query = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync(cancellationToken);
        }


    }
}
