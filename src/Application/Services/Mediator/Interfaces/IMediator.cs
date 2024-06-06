﻿using Domain.Shared;

namespace Application.Services.Mediator.Interfaces
{
    public interface IMediator
    {
        Task<ResultDto<bool>> SendAsync<TRequest>(TRequest request);
        Task<ResultDto<TResponse>> Send<TRequest, TResponse>(TRequest request);
    }
}
