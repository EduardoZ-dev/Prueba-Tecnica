using Microsoft.Extensions.DependencyInjection;


namespace Application
{
    /*public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(c => c.Where(type => type.Name.EndsWith("Handler")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }*/
}
