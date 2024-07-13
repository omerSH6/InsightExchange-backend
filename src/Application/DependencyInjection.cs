using Application.Common.Services.PasswordHash.Interfaces;
using Application.Common.Services.PasswordHash;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Services.Mediator;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediator();
            services.AddSingleton<IPasswordHashService, PasswordHashService>();

            return services;
        }
    }
}
