using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotePro.Models
{
    public class Note
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessage ="Bitte geben Sie einen Titel ein"), StringLength(20, ErrorMessage = "Der Titel darf nicht mehr als 20 Zeichen lang sein."), Display(Name = "Titel")]
        public string Title { get; set; }

        [StringLength(250, ErrorMessage = "Die Beschreibung darf nicht mehr als 250 Zeichen lang sein."), Display(Name = "Beschreibung")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Bitte geben Sie eine Wichtigkeit an"), Range(0, 5), Display(Name = "Wichtigkeit")]
        public int Importance { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Bitte geben Sie ein Erledigungsdatum ein"), Display(Name = "Erledigt bis:")]
        public DateTime DueDate { get; set; }

        [Range(typeof(DateTime), "1900/01/01", "3000/01/01"), Display(Name = "Erledigt am:")]
        public DateTime? FinishDate { get; set; }

        public long AuthorId { get; set; }
    }
}
