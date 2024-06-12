using System.Reflection;
using Application.Common.Services.Mediator.Interfaces;

namespace Application.Common.Services.Mediator
{
    public class MediatorRequestHandlersManager : IMediatorRequestHandlersManager
    {
        public static IMediatorRequestHandlersManager Instance { get; } = new MediatorRequestHandlersManager();
        private readonly Dictionary<Type, Type> _requestHandlers = new Dictionary<Type, Type>();
        public MediatorRequestHandlersManager()
        {
            LoadRequestHandlers();
        }

        public Type GetRequestHandler(Type requestType)
        {
            return _requestHandlers[requestType];
        }

        public IReadOnlyDictionary<Type, Type> GetAllRequestHandlers()
        {
            return _requestHandlers;
        }

        private void LoadRequestHandlers()
        {
            var handlerTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Any(i =>
             i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) || i.GetGenericTypeDefinition() == typeof(IRequestHandler<>))));

            foreach (var handlerType in handlerTypes)
            {
                var @interface = handlerType.GetInterfaces().Where(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) || i.GetGenericTypeDefinition() == typeof(IRequestHandler<>))).First();
                var requestType = @interface.GetGenericArguments()[0];
                _requestHandlers[requestType] = handlerType;
            }
        }
    }
}
