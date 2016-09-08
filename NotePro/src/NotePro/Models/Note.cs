using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotePro.Models
{
    public class Note
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required(ErrorMessage ="Bitte geben Sie einen Titel ein"), StringLength(20, ErrorMessage = "Der Titel darf nicht mehr als 20 Zeichen lang sein.")]
        public string Title { get; set; }
        [StringLength(250, ErrorMessage = "Die Beschreibung darf nicht mehr als 250 Zeichen lang sein.")]
        public string Description { get; set; }
        [Required, Range(0, 5)]
        public int Importance { get; set; }
        [Required, Range(typeof(DateTime), "1900/01/01", "3000/01/01")]
        public DateTime DueDate { get; set; }
        [Range(typeof(DateTime), "1900/01/01", "3000/01/01")]
        public DateTime? FinishDate { get; set; }
        public long AuthorId { get; set; }
    }
}
