using AnimalAdventure.Data.Repositories;
using AnimalAdventure.DTOs;
using System.Reflection.Metadata.Ecma335;

namespace AnimalAdventure.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<List<QuestionDTO>> GetAllQuestionsAsync()
        {
            var questions = await _questionRepository.GetAllQuestionsAsync();

            return questions.Select(q => new QuestionDTO
            {
                Id = q.Id,
                QuestionText = q.QuestionText,
                AnswerOptions = q.AnswerOptions.Select(ao => new AnswerOptionsDTO
                {
                    Option = ao.Option
                }).ToList()
            }).ToList();
        }
    }
}
