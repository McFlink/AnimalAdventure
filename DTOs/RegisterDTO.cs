using System.ComponentModel.DataAnnotations;

namespace AnimalAdventure.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Namn är obligatoriskt")]
        [StringLength(20, ErrorMessage = "Namnet kan vara högst 20 tecken")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Du måste ange PIN-kod")]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "PIN-koden måste bestå av 4 siffror")]
        public string PinCode { get; set; }
    }
}
