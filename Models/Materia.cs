using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ColegioSanJose.Models
{
    public class Materia
    {
        [Key]
        public int MateriaId { get; set; }

        [Required(ErrorMessage = "El nombre de la materia es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string NombreMateria { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "El nombre del docente no puede exceder los 100 caracteres")]
        public string? Docente { get; set; }

        // Propiedad de navegación - debe ser nullable
        public ICollection<Expediente>? Expedientes { get; set; }
    }
}