using Application.Services.Mediator.Interfaces;
using Domain.Shared;

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

        public async Task<ResultDto<bool>> SendAsync<TRequest>(TRequest request)
        {

            var requestHandler = _mediatorRequestHandlersManager.GetRequestHandler(typeof(TRequest));
            var @interface = requestHandler.GetInterfaces().Where(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) || i.GetGenericTypeDefinition() == typeof(IRequestHandler<>))).First();
            var requestHandlerInstance = _serviceProvider.GetService(@interface) as IRequestHandler<TRequest>;
            if (requestHandlerInstance == null)
            {
                throw new InvalidOperationException($"No handler found for request of type {typeof(TRequest).Name}");
            }

            return await requestHandlerInstance.Handle(request);
        }

        public async Task<ResultDto<TResponse>> Send<TRequest, TResponse>(TRequest request)
        {
            var requestHandler = _mediatorRequestHandlersManager.GetRequestHandler(typeof(TRequest));
            var @interface = requestHandler.GetInterfaces().Where(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) || i.GetGenericTypeDefinition() == typeof(IRequestHandler<>))).First();
            var requestHandlerInstance = _serviceProvider.GetService(@interface) as IRequestHandler<TRequest, TResponse>;
            if (requestHandlerInstance == null)
            {
                throw new InvalidOperationException($"No handler found for request of type {typeof(TRequest).Name}");
            }

            return await requestHandlerInstance.Handle(request);
        }
    }
}
