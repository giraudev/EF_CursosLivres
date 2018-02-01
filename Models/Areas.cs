using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursosLivre.Models
{
    public class Areas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdArea { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength=2)]
        public string NomeArea { get; set; }

        public ICollection<Cursos> Cursos{get;set;}
    }
}