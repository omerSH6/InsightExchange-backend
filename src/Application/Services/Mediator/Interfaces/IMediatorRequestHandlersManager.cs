namespace Application.Services.Mediator.Interfaces
{
    public interface IMediatorRequestHandlersManager
    {
        Type GetRequestHandler(Type requestType);
        IReadOnlyDictionary<Type, Type> GetAllRequestHandlers();
    }
}
