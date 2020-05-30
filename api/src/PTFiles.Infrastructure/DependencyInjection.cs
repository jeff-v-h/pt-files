using PTFiles.Application.Common.Interfaces.Time;
using PTFiles.Infrastructure.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PTFiles.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IDateTime, MachineDateTime>();
            return services;
        }
    }
}
