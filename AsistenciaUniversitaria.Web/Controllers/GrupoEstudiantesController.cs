using AsistenciaUniversitaria.Web.Data;
using AsistenciaUniversitaria.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaUniversitaria.Web.Controllers
{
    public class GrupoEstudiantesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GrupoEstudiantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Asignar(int id)
        {
            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo == null)
                return NotFound();

            var estudiantes = await _context.Estudiantes.ToListAsync();
            var asignados = await _context.GrupoEstudiantes
                                          .Where(g => g.GrupoId == id)
                                          .Select(g => g.EstudianteId)
                                          .ToListAsync();

            ViewBag.Grupo = grupo;
            ViewBag.Asignados = asignados;

            return View(estudiantes);
        }

        [HttpPost]
        public async Task<IActionResult> Asignar(int id, int[] estudiantesSeleccionados)
        {
            var existentes = _context.GrupoEstudiantes.Where(g => g.GrupoId == id);
            _context.GrupoEstudiantes.RemoveRange(existentes);

            if (estudiantesSeleccionados != null)
            {
                foreach (var estId in estudiantesSeleccionados)
                {
                    _context.GrupoEstudiantes.Add(new GrupoEstudiante
                    {
                        GrupoId = id,
                        EstudianteId = estId
                    });
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Grupos");
        }
    }
}
