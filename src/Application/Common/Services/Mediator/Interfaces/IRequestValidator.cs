namespace Application.Common.Services.Mediator.Interfaces
{
    public interface IRequestValidator<TCommand>
    {
        public bool IsValid(TCommand request);
    }
}
