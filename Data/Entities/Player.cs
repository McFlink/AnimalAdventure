using System.ComponentModel.DataAnnotations;

namespace AnimalAdventure.Data.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PinCode { get; set; }
    }
}
