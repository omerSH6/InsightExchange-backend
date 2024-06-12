namespace Application.Common.Services.Mediator.Interfaces
{
    public interface IMediatorRequestsManager
    {
        Type GetRequestHandlerInterfaceType(Type requestType);
        List<(Type, Type)> GetAllRequestHandlersInterfacesAndImplementation();
    }
}
