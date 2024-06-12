using Application.Common.Services.Mediator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Services.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMediatorRequestsManager _mediatorRequestHandlersManager;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _mediatorRequestHandlersManager = MediatorRequestsManager.Instance;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var handlerInterfaceType = _mediatorRequestHandlersManager.GetRequestHandlerInterfaceType(request.GetType());
            dynamic handler = _serviceProvider.GetRequiredService(handlerInterfaceType);
            return await handler.Handle((dynamic)request);
        }

        public async Task Send(IRequest request)
        {
            var handlerInterfaceType = _mediatorRequestHandlersManager.GetRequestHandlerInterfaceType(request.GetType());
            dynamic handler = _serviceProvider.GetRequiredService(handlerInterfaceType);
            await handler.Handle((dynamic)request);
        }
    }
}
