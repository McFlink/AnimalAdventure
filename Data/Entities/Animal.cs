namespace AnimalAdventure.Data.Entities
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Habitat { get; set; }
        public string? ImagePath { get; set; }
    }
}
