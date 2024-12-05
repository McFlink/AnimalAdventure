using AnimalAdventure.DTOs;

namespace AnimalAdventure.Services
{
    public interface IQuestionService
    {
        Task<List<QuestionDTO>> GetAllQuestionsAsync();
    }
}
