namespace AsistenciaUniversitaria.Web.Models
{
    public class Asistencia
    {
        public int Id { get; set; }
        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }

        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }

        public DateTime Fecha { get; set; }
        public string Estado { get; set; } // asistio, ausente, justificado
        public string Nota { get; set; }
        public string Archivo { get; set; } // ruta del archivo
    }
}
