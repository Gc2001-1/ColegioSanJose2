using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;
namespace ColegioSanJose.Models
{
    public class Materia
    {
        [Key]
        public int MateriaId { get; set; }

        [Required]
        [StringLength(100)]
        public required string NombreMateria { get; set; }

        [StringLength(100)]
        public required string Docente { get; set; }

        public required ICollection<Expediente> Expedientes { get; set; }
    }
}
