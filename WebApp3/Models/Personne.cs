using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp3.Models
{
    public class Personne
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Num { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Nom { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Prenom { get; set; }
    }
}
