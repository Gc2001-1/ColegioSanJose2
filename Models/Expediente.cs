using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColegioSanJose.Models
{
    public class Expediente
    {
        [Key]
        public int ExpedienteId { get; set; }

        [Required]
        [ForeignKey("Alumno")]
        public int AlumnoId { get; set; }
        public Alumno? Alumno { get; set; }  // ← Cambiar a nullable

        [Required]
        [ForeignKey("Materia")]
        public int MateriaId { get; set; }
        public Materia? Materia { get; set; }  // ← Cambiar a nullable

        [Range(0, 20)]
        public decimal NotaFinal { get; set; }

        [StringLength(500)]
        public string? Observaciones { get; set; }  // ← Cambiar a nullable (o quitar required)
    }
}