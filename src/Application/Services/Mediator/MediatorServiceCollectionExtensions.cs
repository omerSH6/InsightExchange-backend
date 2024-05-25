using Application.Services.Mediator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Mediator
{
    public static class MediatorServiceCollectionExtensions
    {
        public static void AddMediator(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();

            var requestHandlers = MediatorRequestHandlersManager.Instance.GetAllRequestHandlers();

            foreach (var requestHandler in requestHandlers)
            {
                services.AddScoped(requestHandler.Key, requestHandler.Value);
            }
        }
    }
}
