using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistenciaUniversitaria.Web.Data;
using AsistenciaUniversitaria.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace AsistenciaUniversitaria.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class RecintosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecintosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Recintos.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Recinto recinto)
        {
            if (ModelState.IsValid)
            {
                _context.Recintos.Add(recinto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recinto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var recinto = await _context.Recintos.FindAsync(id);
            if (recinto == null) return NotFound();
            return View(recinto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Recinto recinto)
        {
            if (id != recinto.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(recinto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recinto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var recinto = await _context.Recintos.FindAsync(id);
            if (recinto == null) return NotFound();
            return View(recinto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recinto = await _context.Recintos.FindAsync(id);
            _context.Recintos.Remove(recinto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
