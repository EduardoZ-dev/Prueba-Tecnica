using Application.Abstractions.Persistence;
using Application.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Service service, CancellationToken cancellationToken = default)
        {
            await _context.Services.AddAsync(service, cancellationToken);
        }

        public async Task<Service?> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Services
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<List<Service>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _context.Services.ToListAsync(cancellationToken);
        }

        public Task Update(Service service, CancellationToken cancellationToken = default)
        {
            _context.Services.Update(service);
            return Task.CompletedTask;
        }

        public void Delete(Service service)
        {
            _context.Services.Remove(service);
        }

        public async Task<Dictionary<string, int>> CountByCountry(CancellationToken cancellationToken = default)
        {
            return await _context.Services
                .SelectMany(s => s.Countries)
                .GroupBy(c => c)
                .ToDictionaryAsync(g => g.Key, g => g.Count(), cancellationToken);
        }

        public async Task<List<TopServiceDto>> GetTopServices(CancellationToken cancellationToken = default)
        {
            return await _context.Providers
                .SelectMany(p => p.Services)
                .GroupBy(s => s.Name)
                .Select(g => new TopServiceDto(g.Key, g.Count()))
                .OrderByDescending(dto => dto.Count)
                .ToListAsync(cancellationToken);
        }
    }
}
