using Application.Abstractions.Persistence;
using Domain.Common;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.UnitOfWork;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            // Repositorios
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ICustomFieldRepository, CustomFieldRepository>();

            // Servicios externos
            services.AddScoped<ICountryService, CountryService>();

            return services;
        }
    }
}
