using System.ComponentModel.DataAnnotations.Schema;

namespace AsistenciaUniversitaria.Web.Models
{
    [Table("carreras")]   // opcional pero ayuda a que sepa el nombre exacto de la tabla
    public class Carrera
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("escuela_id")]
        public int EscuelaId { get; set; }
        public Escuela Escuela { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("estado")]
        public string Estado { get; set; }
    }
}
