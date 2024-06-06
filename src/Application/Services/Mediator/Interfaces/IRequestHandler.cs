using Domain.Shared;

namespace Application.Services.Mediator.Interfaces
{
    public interface IRequestHandler<TRequest, TResponse>
    {
        Task<ResultDto<TResponse>> Handle(TRequest request);
    }

    public interface IRequestHandler<TRequest>
    {
        Task<ResultDto<bool>> Handle(TRequest request);
    }
}
