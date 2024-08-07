﻿using Application.Common.Services.Mediator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Services.Mediator
{
    public static class MediatorDependencyInjection
    {
        public static void AddMediator(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();

            var requestHandlersInterfacesAndImplementation = MediatorRequestsManager.Instance.GetAllRequestHandlersInterfacesAndImplementation();
            var requestValidatorsInterfacesAndImplementation = MediatorRequestsManager.Instance.GetAllRequestValidatorsInterfacesAndImplementation();

            foreach (var (requestHandlerInterface, requestHandlerImplementation) in requestHandlersInterfacesAndImplementation)
            {
                services.AddTransient(requestHandlerInterface, requestHandlerImplementation);
            }

            foreach (var (requestValidatorInterface, requestValidatorImplementation) in requestValidatorsInterfacesAndImplementation)
            {
                services.AddSingleton(requestValidatorInterface, requestValidatorImplementation);
            }
        }
    }
}
