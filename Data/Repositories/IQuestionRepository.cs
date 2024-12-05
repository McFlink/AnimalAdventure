using AnimalAdventure.Data.Entities;

namespace AnimalAdventure.Data.Repositories
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetAllQuestionsAsync();
    }
}
