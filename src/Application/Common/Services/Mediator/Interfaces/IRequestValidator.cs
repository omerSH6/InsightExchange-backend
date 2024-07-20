namespace Application.Common.Services.Mediator.Interfaces
{
    public interface IRequestValidator<TRequest>
    {
        public bool IsValid(TRequest request);
    }
}
