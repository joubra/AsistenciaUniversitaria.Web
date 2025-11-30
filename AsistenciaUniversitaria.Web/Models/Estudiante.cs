namespace AsistenciaUniversitaria.Web.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Carnet { get; set; }
        public string Estado { get; set; } = "activo";
    }
}
