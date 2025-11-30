using System.ComponentModel.DataAnnotations;

namespace AsistenciaUniversitaria.Web.Models
{
    public class Usuario

    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Correo { get; set; }

        [Required]
        [StringLength(255)]
        [DataType(DataType.Password)]
        public string Password { get; set; }  // por ahora texto plano (académico)

        [Required]
        [StringLength(20)]
        public string Rol { get; set; }   // "admin", "docente", "responsable"

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = "activo";
    
}
}
