namespace AnimalAdventure.Data.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string CorrectAnswer {  get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
    }
}
