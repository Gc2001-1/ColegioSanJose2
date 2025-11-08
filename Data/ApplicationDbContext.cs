using Microsoft.EntityFrameworkCore;
using ColegioSanJose.Models;

namespace ColegioSanJose.Data
{
    public class ApplicationDbContext : DbContext
   
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Expediente> Expedientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar precisión de NotaFinal
            modelBuilder.Entity<Expediente>()
                .Property(e => e.NotaFinal)
                .HasColumnType("decimal(5,2)");
        }
    }
}
