using System.Reflection;
using Application.Answers.Commands;
using Application.Common.Services.Mediator.Interfaces;

namespace Application.Common.Services.Mediator
{
    public class MediatorRequestsManager : IMediatorRequestsManager
    {
        public static IMediatorRequestsManager Instance = new MediatorRequestsManager();
        
        private readonly List<RequestTypes> _requestTypes = new List<RequestTypes>();
        private MediatorRequestsManager()
        {
            LoadRequestHandlers();
        }

        public Type? GetRequestHandlerInterfaceType(Type requestType)
        {
             return _requestTypes.First(types=>types.RequestType.Equals(requestType)).RequestHandlerInterfaceType;
        }
        
        public Type? GetRequestValidatorInterfaceType(Type requestType)
        {
             return _requestTypes.First(types=>types.RequestType.Equals(requestType)).RequestValidatorInterfaceType;
        }

        public List<(Type, Type)> GetAllRequestHandlersInterfacesAndImplementation()
        {
            if (_requestTypes != null)
            {
                return _requestTypes.Select(types => (types.RequestHandlerInterfaceType, types.RequestHandlerType)).ToList();
            }
            
            return [];
        }
        
        public List<(Type, Type)> GetAllRequestValidatorsInterfacesAndImplementation()
        {
            if (_requestTypes != null)
            {
                return _requestTypes.Where(type => type.RequestValidatorType != null && type.RequestValidatorInterfaceType != null).Select(type => (type.RequestValidatorInterfaceType, type.RequestValidatorType)).ToList();
            }
            
            return [];
        }

        private void LoadRequestHandlers()
        {
            var assemblies = Assembly.GetExecutingAssembly().GetTypes();
            var handlerTypes = assemblies.Where(t => t.GetInterfaces().Any(i =>
             i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) || i.GetGenericTypeDefinition() == typeof(IRequestHandler<>))));

            foreach (var handlerType in handlerTypes)
            {
                var requestHandlerType = handlerType;
                var requestHandlerInterfaceType = requestHandlerType.GetInterfaces().Where(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) || i.GetGenericTypeDefinition() == typeof(IRequestHandler<>))).First();
                var requestType = requestHandlerInterfaceType.GetGenericArguments()[0];
                var requestValidatorType = assemblies.Where(a => a.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestValidator<>) && i.GetGenericArguments().Any() && i.GetGenericArguments()[0] == requestType)).FirstOrDefault();
                var requestValidatorInterfaceType = requestValidatorType?.GetInterfaces().Where(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IRequestValidator<>))).FirstOrDefault();


                _requestTypes.Add(new RequestTypes()
                {
                    RequestHandlerInterfaceType = requestHandlerInterfaceType,
                    RequestHandlerType = requestHandlerType,    
                    RequestType = requestType,
                    RequestValidatorInterfaceType = requestValidatorInterfaceType,
                    RequestValidatorType = requestValidatorType,
                });
            }
        }
    }
}
