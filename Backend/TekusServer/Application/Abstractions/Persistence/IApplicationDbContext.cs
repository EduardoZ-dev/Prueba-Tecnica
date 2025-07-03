using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Abstractions.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Provider> Providers { get; }
        DbSet<Service> Services { get; }
        DbSet<CustomField> CustomFields { get; }
    }
}
