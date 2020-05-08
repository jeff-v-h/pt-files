using PTFiles.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PTFiles.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PTFilesDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("PTFilesDb"), b => b.EnableRetryOnFailure()));
            //configuration.GetConnectionString("PTFilesDb"),
            //b => b.MigrationsAssembly(typeof(PTFilesDbContext).Assembly.FullName)));

            services.AddScoped<IPTFilesDbContext>(provider => provider.GetService<PTFilesDbContext>());

            return services;
        }
    }
}
