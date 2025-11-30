using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;  // 👈 IMPORTANTE

namespace AsistenciaUniversitaria.Web.Models
{
    public class Escuela
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Recinto")]
        [Column("recinto_id")]
        public int RecintoId { get; set; }   // <-- Cambiado a int (no nullable)

        public Recinto Recinto { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = "activo";
    }
}
