using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ColegioSanJose.Models
{
    public class Alumno
    {
        [Key]
        public int AlumnoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [StringLength(50)]
        public string Grado { get; set; }

        public ICollection<Expediente> Expedientes { get; set; } = new List<Expediente>();
    }
}
