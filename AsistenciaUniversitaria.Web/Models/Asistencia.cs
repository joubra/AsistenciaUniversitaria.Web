using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistenciaUniversitaria.Web.Models
{
    [Table("asistencias")]
    public class Asistencia
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("grupo_id")]
        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }

        [Column("estudiante_id")]
        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        [Column("nota")]
        public string? Nota { get; set; }


        [Column("archivo")]
        public string? Archivo { get; set; }
    }
}
