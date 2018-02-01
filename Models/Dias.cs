using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursosLivre.Models
{
    public class Dias
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDias { get; set; }
       
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Dia { get; set; }
       
        [Required]
        public int IdCronograma { get; set; }

        public Cronogramas Cronograma { get; set; }


    }
}