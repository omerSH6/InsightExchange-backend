namespace Application.Services.Mediator.Interfaces
{
    public interface IMediator
    {
        Task SendAsync<TRequest>(TRequest request);
        Task<TResponse> Send<TRequest, TResponse>(TRequest request);
    }
}
