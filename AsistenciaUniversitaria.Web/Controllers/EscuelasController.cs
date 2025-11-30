using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AsistenciaUniversitaria.Web.Data;
using AsistenciaUniversitaria.Web.Models;

namespace AsistenciaUniversitaria.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class EscuelasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EscuelasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Escuelas
        public async Task<IActionResult> Index()
        {
            var escuelas = await _context.Escuelas
                .Include(e => e.Recinto)
                .ToListAsync();

            return View(escuelas);
        }

        // GET: Escuelas/Create
        public async Task<IActionResult> Create()
        {
            var recintos = await _context.Recintos.ToListAsync();
            ViewBag.Recintos = recintos;
            return View();
        }

        // POST: Escuelas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Escuela escuela)
        {
            // Solo para pruebas: si llega 0, sabemos que no se envió nada
            if (escuela.RecintoId == 0)
            {
                return Content("No llegó RecintoId. Valor: " + escuela.RecintoId);
            }

            _context.Escuelas.Add(escuela);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Escuelas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var escuela = await _context.Escuelas.FindAsync(id);
            if (escuela == null) return NotFound();

            ViewBag.RecintoId = new SelectList(_context.Recintos, "Id", "Nombre", escuela.RecintoId);
            return View(escuela);
        }

        // POST: Escuelas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Escuela escuela)
        {
            if (id != escuela.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(escuela);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.RecintoId = new SelectList(_context.Recintos, "Id", "Nombre", escuela.RecintoId);
            return View(escuela);
        }

        // GET: Escuelas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var escuela = await _context.Escuelas
                .Include(e => e.Recinto)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (escuela == null) return NotFound();

            return View(escuela);
        }

        // POST: Escuelas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var escuela = await _context.Escuelas.FindAsync(id);
            if (escuela != null)
            {
                _context.Escuelas.Remove(escuela);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
