using Application.Common.Services.Mediator.Interfaces;

namespace Application.Common.Services.Mediator
{
    public class RequestTypes
    {
        public required Type RequestHandlerType { get; set; }
        public Type? RequestValidatorInterfaceType { get; set; }
        public Type? RequestValidatorType { get; set; }
        public required Type RequestHandlerInterfaceType { get; set; }
        public required Type RequestType { get; set; }
    }
}
