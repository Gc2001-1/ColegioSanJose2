namespace ColegioSanJose.Models
{
    public class AlumnoPromedioViewModel
    {
        public int AlumnoId { get; set; }
        public string NombreCompleto { get; set; }
        public decimal Promedio { get; set; }
        public int CantMaterias { get; set; }
    }
}
