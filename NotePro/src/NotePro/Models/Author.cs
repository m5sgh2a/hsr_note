using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotePro.Models
{
    public class AuthorLogin
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, StringLength(100, ErrorMessage = "Bitte verwenden Sie für das E-Mail nicht mehr als 100 Zeichen.")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), StringLength(20, ErrorMessage = "Bitte verwenden Sie für das Passwort nicht mehr als 20 Zeichen.")]
        public string Password { get; set; }
        public List<Note> Notes { get; set; }
    }

    public class AuthorNew : AuthorLogin
    {
        [Required, StringLength(50, ErrorMessage = "Bitte verwenden Sie für den Vornamen nicht mehr als 50 Zeichen.")]
        public string FirstName { get; set; }

        [Required, StringLength(50, ErrorMessage = "Bitte verwenden Sie für den Nachnamen nicht mehr als 50 Zeichen.")]
        public string LastName { get; set; }
    }
}
