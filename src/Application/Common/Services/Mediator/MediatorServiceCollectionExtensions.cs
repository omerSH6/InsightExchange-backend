using Application.Common.Services.Mediator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Services.Mediator
{
    public static class MediatorServiceCollectionExtensions
    {
        public static void AddMediator(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();

            var requestHandlersInterfacesAndImplementation = MediatorRequestsManager.Instance.GetAllRequestHandlersInterfacesAndImplementation();

            foreach (var (requestHandlerInterface, requestHandlerImplementation) in requestHandlersInterfacesAndImplementation)
            {
                services.AddTransient(requestHandlerImplementation);
            }
        }
    }
}
