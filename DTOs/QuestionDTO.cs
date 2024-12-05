using AnimalAdventure.Data.Entities;
using System.Runtime.InteropServices.Marshalling;

namespace AnimalAdventure.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public List<AnswerOptionsDTO> AnswerOptions { get; set; } = new List<AnswerOptionsDTO>();
    }
}
