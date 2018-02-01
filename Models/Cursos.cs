using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursosLivre.Models
{
    public class Cursos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCursos { get; set; }
       
        [Required]
        public int IdAreas { get; set; }
       
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string NomeCurso { get; set; }

        public Areas AreaCurso { get; set; }

        public ICollection<Cronogramas> Cronogramas { get; set; }

    }
}