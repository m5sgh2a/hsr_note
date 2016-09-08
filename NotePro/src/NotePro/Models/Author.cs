using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotePro.Models
{
    public class Author
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required, StringLength(50, ErrorMessage = "Der Vorname darf nicht mehr als 50 Zeichen lang sein.")]
        public string FirstName { get; set; }
        [Required, StringLength(50, ErrorMessage = "Der Nachname darf nicht mehr als 50 Zeichen lang sein.")]
        public string LastName { get; set; }
        [Required, StringLength(100, ErrorMessage = "Das E-Mail darf nicht mehr als 100 Zeichen lang sein.")]
        public string Email { get; set; }
        [Required, StringLength(20, ErrorMessage = "Das Passwort darf nicht mehr als 20 Zeichen lang sein.")]
        public string Password { get; set; }
        public List<Note> Notes { get; set; }
    }
}
