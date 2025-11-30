using AsistenciaUniversitaria.Web.Data;
using AsistenciaUniversitaria.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaUniversitaria.Web.Controllers
{
    [Authorize(Roles = "admin,docente")]
    public class AsistenciasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AsistenciasController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Tomar asistencia
        public async Task<IActionResult> Tomar(int grupoId, DateTime? fecha)
        {
            if (fecha == null)
                fecha = DateTime.Today;

            var grupo = await _context.Grupos.FindAsync(grupoId);
            if (grupo == null)
                return NotFound();

            var estudiantes = await _context.GrupoEstudiantes
                .Where(x => x.GrupoId == grupoId)
                .Include(x => x.Estudiante)
                .Select(x => x.Estudiante)
                .ToListAsync();

            var asistenciasPrevias = await _context.Asistencias
                .Where(x => x.GrupoId == grupoId && x.Fecha == fecha.Value.Date)
                .ToListAsync();

            ViewBag.Grupo = grupo;
            ViewBag.Fecha = fecha.Value;
            ViewBag.Asistencias = asistenciasPrevias;

            return View(estudiantes);  
        }



        // POST: Guardar asistencia
        [HttpPost]
        public async Task<IActionResult> Tomar(
     int grupoId,
     DateTime fecha,
     List<int> estudianteId,
     List<string> estado,
     List<string?> nota,
     List<IFormFile>? archivo)
        {
            // 1. Borrar asistencias previas de ese día y grupo
            var previas = _context.Asistencias
                .Where(x => x.GrupoId == grupoId && x.Fecha == fecha.Date);

            _context.Asistencias.RemoveRange(previas);
            await _context.SaveChangesAsync();

            // 2. Volver a crear según lo que venga del formulario
            for (int i = 0; i < estudianteId.Count; i++)
            {
                IFormFile? file = null;

                // Protegernos por si archivo es null o tiene menos elementos
                if (archivo != null && archivo.Count > i)
                {
                    file = archivo[i];
                }

                string? archivoNombre = null;

                if (file != null && file.Length > 0)
                {
                    var uploads = Path.Combine(_env.WebRootPath, "justificaciones");
                    Directory.CreateDirectory(uploads);

                    archivoNombre = Guid.NewGuid() + "_" + file.FileName;
                    var ruta = Path.Combine(uploads, archivoNombre);

                    using var stream = new FileStream(ruta, FileMode.Create);
                    file.CopyTo(stream);
                }

                var nuevaAsistencia = new Asistencia
                {
                    GrupoId = grupoId,
                    EstudianteId = estudianteId[i],
                    Fecha = fecha.Date,
                    Estado = estado[i],
                    Nota = (nota != null && nota.Count > i) ? nota[i] : null,
                    Archivo = archivoNombre
                };

                _context.Asistencias.Add(nuevaAsistencia);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Grupos");
        }
        public async Task<IActionResult> Ver(int grupoId, DateTime? fecha)
        {
            if (fecha == null)
                fecha = DateTime.Today;

            var grupo = await _context.Grupos.FindAsync(grupoId);
            if (grupo == null) return NotFound();

            var asistencias = await _context.Asistencias
                .Where(a => a.GrupoId == grupoId && a.Fecha == fecha.Value.Date)
                .Include(a => a.Estudiante)
                .ToListAsync();

            ViewBag.Grupo = grupo;
            ViewBag.Fecha = fecha.Value;

            return View(asistencias);
        }


    }
}
