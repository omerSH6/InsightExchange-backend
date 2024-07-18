namespace Application.Common.Services.Mediator.Interfaces
{
    public interface IMediatorRequestsManager
    {
        Type? GetRequestHandlerInterfaceType(Type requestType);
        Type? GetRequestValidatorInterfaceType(Type requestType);
        List<(Type, Type)> GetAllRequestHandlersInterfacesAndImplementation();
        List<(Type, Type)> GetAllRequestValidatorsInterfacesAndImplementation();
    }
}
