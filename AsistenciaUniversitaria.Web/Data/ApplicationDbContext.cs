using Microsoft.EntityFrameworkCore;
using AsistenciaUniversitaria.Web.Models;

namespace AsistenciaUniversitaria.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Recinto> Recintos { get; set; }
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<GrupoEstudiante> GrupoEstudiantes { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
    }
}

