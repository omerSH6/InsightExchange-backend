using System.Reflection;
using Application.Common.Services.Mediator.Interfaces;

namespace Application.Common.Services.Mediator
{
    public class MediatorRequestsManager : IMediatorRequestsManager
    {
        public static IMediatorRequestsManager Instance = new MediatorRequestsManager();
        
        private readonly List<RequestTypes> _requestTypes = new List<RequestTypes>();
        public MediatorRequestsManager()
        {
            LoadRequestHandlers();
        }

        public Type GetRequestHandlerInterfaceType(Type requestType)
        {
             return _requestTypes.First(types=>types.RequestType.Equals(requestType)).RequestHandlerInterfaceType;
        }

        public List<(Type, Type)> GetAllRequestHandlersInterfacesAndImplementation()
        {
            return _requestTypes.Select(types=>(types.RequestHandlerInterfaceType, types.RequestHandlerType)).ToList();
        }

        private void LoadRequestHandlers()
        {
            var handlerTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Any(i =>
             i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) || i.GetGenericTypeDefinition() == typeof(IRequestHandler<>))));

            foreach (var handlerType in handlerTypes)
            {
                _requestTypes.Add(new RequestTypes(handlerType));
            }
        }
    }
}
