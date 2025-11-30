using AsistenciaUniversitaria.Web.Data;
using AsistenciaUniversitaria.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaUniversitaria.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class GruposController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GruposController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Grupos
        public async Task<IActionResult> Index()
        {
            var grupos = await _context.Grupos
                .Include(g => g.Carrera)
                .ToListAsync();

            return View(grupos);
        }

        // GET: Grupos/Create
        public async Task<IActionResult> Create()
        {
            var carreras = await _context.Carreras
                .Include(c => c.Escuela)
                .ToListAsync();

            ViewBag.Carreras = carreras;
            return View();
        }

        // POST: Grupos/Create
        [HttpPost]
        public async Task<IActionResult> Create(Grupo grupo)
        {
            _context.Grupos.Add(grupo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Grupos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo == null) return NotFound();

            var carreras = await _context.Carreras
                .Include(c => c.Escuela)
                .ToListAsync();

            ViewBag.Carreras = carreras;
            return View(grupo);
        }

        // POST: Grupos/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Grupo grupo)
        {
            if (id != grupo.Id) return NotFound();

            _context.Update(grupo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Grupos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var grupo = await _context.Grupos
                .Include(g => g.Carrera)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (grupo == null) return NotFound();

            return View(grupo);
        }

        // POST: Grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo != null)
            {
                _context.Grupos.Remove(grupo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
