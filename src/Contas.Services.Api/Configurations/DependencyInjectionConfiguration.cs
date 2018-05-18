using Contas.Infrastructure.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Contas.Services.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            Bootstrapper.RegisterServices(services);
        }
    }
}