using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace NotePro.Models
{
    public class Note
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } = -1;

        [Required(ErrorMessage ="Bitte geben Sie einen Titel ein")]
        [StringLength(20, ErrorMessage = "Der Titel darf nicht mehr als 20 Zeichen lang sein.")]
        [Display(Name = "Titel")]
        public string Title { get; set; }

        [StringLength(250, ErrorMessage = "Die Beschreibung darf nicht mehr als 250 Zeichen lang sein.")]
        [Display(Name = "Beschreibung")]
        public string Description { get; set; }

        [Display(Name = "Wichtigkeit")]
        [HiddenInput(DisplayValue = false)]
        public int Importance { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage ="Bitte geben Sie ein Erledigungsdatum ein")]
        [Display(Name = "Erledigt bis:")]
        public DateTime DueDate { get; set; }

        [Range(typeof(DateTime), "1900/01/01", "3000/01/01")]
        [Display(Name = "Erledigt am:")]
        public DateTime? FinishDate { get; set; }

        [Display(Name = "Erledigt")]
        public bool Finished { get; set; }

        
        public DateTime CreateDate { get; set; }

        public long AuthorId { get; set; }
    }
}
