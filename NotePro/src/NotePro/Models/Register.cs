using System.ComponentModel.DataAnnotations;

namespace NotePro.Models
{
    public class Register : Login
    {
        [Required]
        [StringLength(50, ErrorMessage = "Bitte verwenden Sie für den Vornamen nicht mehr als 50 Zeichen.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Bitte verwenden Sie für den Nachnamen nicht mehr als 50 Zeichen.")]
        public string LastName { get; set; }
    }
}
