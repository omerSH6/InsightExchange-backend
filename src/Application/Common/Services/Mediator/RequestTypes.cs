using Application.Common.Services.Mediator.Interfaces;

namespace Application.Common.Services.Mediator
{
    public class RequestTypes
    {
        public Type RequestHandlerType { get; }
        public Type RequestHandlerInterfaceType { get; }
        public Type RequestType { get; }
        public Type RequestTypeInterface { get; }

        public RequestTypes(Type requestHandlerType) 
        {
            RequestHandlerType = requestHandlerType;
            RequestHandlerInterfaceType = RequestHandlerType.GetInterfaces().Where(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) || i.GetGenericTypeDefinition() == typeof(IRequestHandler<>))).First();
            RequestType = RequestHandlerInterfaceType.GetGenericArguments()[0];
            RequestTypeInterface = RequestType.GetInterfaces().Where(i => (i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IRequest<>))) || i == typeof(IRequest)).First();
        }
    }
}
