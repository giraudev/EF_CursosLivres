using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursosLivre.Models
{
    public class Cronogramas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCronograma { get; set; }
        
        [Required]
        public int IdCursos { get; set; }
        
        [Required]
        [DataType(DataType.Time)]
        public DateTime HoraInicio { get; set; }
       
        [Required]
        [DataType(DataType.Time)]
        public DateTime HoraFim { get; set; }
       
        [Required]
        [DataType(DataType.Date)]
        public DateTime DiaInicio { get; set; }
       
        [Required]
        [DataType(DataType.Date)]
        public DateTime DiaFim { get; set; }

        public Cursos Curso { get; set; }

        public ICollection<Dias> Dias { get; set; }



    }
}