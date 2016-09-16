﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotePro.Models
{
    public class Login
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Bitte verwenden Sie für das E-Mail nicht mehr als 100 Zeichen.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "Bitte verwenden Sie für das Passwort zwischen 6 und 20 Zeichen.", MinimumLength = 6)]
        [Display(Name = "Passwort")]
        public string Password { get; set; }
    }
}
