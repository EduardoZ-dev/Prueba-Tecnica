using Application.Abstractions.Persistence;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProviderRepository Providers { get; }
        public IServiceRepository Services { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IProviderRepository providerRepository,
            IServiceRepository serviceRepository)
        {
            _context = context;
            Providers = providerRepository;
            Services = serviceRepository;
        }

        public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
        {
            foreach (var entry in _context.ChangeTracker
                         .Entries<BaseEntity>()
                         .Where(e => e.State == EntityState.Modified))
            {
                entry.Entity.SetUpdatedNow();
            }

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose() => _context.Dispose();
    }
}
