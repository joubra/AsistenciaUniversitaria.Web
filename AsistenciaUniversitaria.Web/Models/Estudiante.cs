using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistenciaUniversitaria.Web.Models
{
    [Table("estudiantes")]
    public class Estudiante
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("carnet")]
        public string Carnet { get; set; }

        [Required]
        [Column("estado")]
        public string Estado { get; set; }
    }
}

