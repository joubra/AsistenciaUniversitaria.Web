using System.ComponentModel.DataAnnotations.Schema;

namespace AsistenciaUniversitaria.Web.Models
{
    [Table("grupos")]   // nombre real de la tabla
    public class Grupo
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("carrera_id")]
        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }

        // si en la tabla existe docente_id, lo mapeamos:
        [Column("docente_id")]
        public int? DocenteId { get; set; }   // lo dejo nullable por si tienes nulos
        public Usuario Docente { get; set; }  // navegación al docente (Usuario)

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("turno")]
        public string Turno { get; set; }

        [Column("estado")]
        public string Estado { get; set; }
    }
}
