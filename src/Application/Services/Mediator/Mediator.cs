using System.Reflection;
using Application.Services.Mediator.Interfaces;

namespace Application.Services.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMediatorRequestHandlersManager _mediatorRequestHandlersManager;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _mediatorRequestHandlersManager = MediatorRequestHandlersManager.Instance;
        }

        public async Task SendAsync<TRequest>(TRequest request)
        {

            var requestHandler = _mediatorRequestHandlersManager.GetRequestHandler(typeof(TRequest));
            var requestHandlerInstance = _serviceProvider.GetService(requestHandler) as IRequestHandler<TRequest>;
            if (requestHandlerInstance == null)
            {
                throw new InvalidOperationException($"No handler found for request of type {typeof(TRequest).Name}");
            }

            await requestHandlerInstance.Handle(request);
        }

        public async Task<TResponse> Send<TRequest, TResponse>(TRequest request)
        {
            var requestHandler = _mediatorRequestHandlersManager.GetRequestHandler(typeof(TRequest));
            var requestHandlerInstance = _serviceProvider.GetService(requestHandler) as IRequestHandler<TRequest, TResponse>;
            if (requestHandlerInstance == null)
            {
                throw new InvalidOperationException($"No handler found for request of type {typeof(TRequest).Name}");
            }

            return await requestHandlerInstance.Handle(request);
        }
    }
}
