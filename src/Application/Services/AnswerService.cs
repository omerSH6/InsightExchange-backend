using Domain.Interfaces;

namespace Application.Services
{
    public class AnswerService
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswerService(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        // Implement service methods
    }
}
