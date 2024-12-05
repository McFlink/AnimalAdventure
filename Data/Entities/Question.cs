namespace AnimalAdventure.Data.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }

        // Navigation
        public List<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();
    }
}
