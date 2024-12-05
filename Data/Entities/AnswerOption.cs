namespace AnimalAdventure.Data.Entities
{
    public class AnswerOption
    {
        public int Id { get; set; }
        public string Option { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }

        // Navigation
        public Question Question { get; set; }
    }
}
