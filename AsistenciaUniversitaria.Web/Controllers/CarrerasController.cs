using AsistenciaUniversitaria.Web.Data;
using AsistenciaUniversitaria.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaUniversitaria.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class CarrerasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarrerasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Carreras
        public async Task<IActionResult> Index()
        {
            var carreras = await _context.Carreras
                .Include(c => c.Escuela)
                .ToListAsync();

            return View(carreras);
        }

        // GET: Carreras/Create
        public async Task<IActionResult> Create()
        {
            var escuelas = await _context.Escuelas
                .Include(e => e.Recinto)
                .ToListAsync();

            ViewBag.Escuelas = escuelas;
            return View();
        }

        // POST: Carreras/Create
        [HttpPost]
        public async Task<IActionResult> Create(Carrera carrera)
        {
            _context.Carreras.Add(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Carreras/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            if (carrera == null) return NotFound();

            var escuelas = await _context.Escuelas
                .Include(e => e.Recinto)
                .ToListAsync();
            ViewBag.Escuelas = escuelas;

            return View(carrera);
        }

        // POST: Carreras/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Carrera carrera)
        {
            if (id != carrera.Id) return NotFound();

            _context.Update(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Carreras/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var carrera = await _context.Carreras
                .Include(c => c.Escuela)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (carrera == null) return NotFound();

            return View(carrera);
        }

        // POST: Carreras/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            if (carrera != null)
            {
                _context.Carreras.Remove(carrera);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
