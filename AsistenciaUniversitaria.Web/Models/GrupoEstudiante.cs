using System.ComponentModel.DataAnnotations.Schema;

namespace AsistenciaUniversitaria.Web.Models
{
    [Table("grupo_estudiantes")]
    public class GrupoEstudiante
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("grupo_id")]
        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }

        [Column("estudiante_id")]
        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }
    }
}
