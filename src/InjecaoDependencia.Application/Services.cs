using InjecaoDependencia.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InjecaoDependencia.Application
{
    public static class Services
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IScoped, Scoped>();
            services.AddTransient<ITransient, Transient>();
            services.AddSingleton<ISingleton, Singleton>();

            return services;
        }
    }
}
