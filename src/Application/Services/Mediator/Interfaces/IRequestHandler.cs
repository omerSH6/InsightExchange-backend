namespace Application.Services.Mediator.Interfaces
{
    public interface IRequestHandler<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }

    public interface IRequestHandler<TRequest>
    {
        Task Handle(TRequest request);
    }
}
