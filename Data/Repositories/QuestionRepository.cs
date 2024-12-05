
using AnimalAdventure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimalAdventure.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _context;

        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            try
            {
                return await _context.Questions
                    .Include(q => q.AnswerOptions)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching list of questions.", ex.Message);
                return new List<Question>();
            }
        }
    }
}
