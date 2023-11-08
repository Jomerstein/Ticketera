using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservaTicket.Context;
using ReservaTicket.Models;

namespace ReservaTicket.Controllers
{
    public class EspectaculoController : Controller
    {
        private readonly TicketeraDataBaseContext _context;

        public EspectaculoController(TicketeraDataBaseContext context)
        {
            _context = context;
        }

        // GET: Espectaculo
        public async Task<IActionResult> Index()
        {
            var ticketeraDataBaseContext = _context.espectaculo.Include(e => e.creador);
            return View(await ticketeraDataBaseContext.ToListAsync());
        }

        // GET: Espectaculo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.espectaculo == null)
            {
                return NotFound();
            }

            var espectaculo = await _context.espectaculo
                .Include(e => e.creador)
                .FirstOrDefaultAsync(m => m.idEspectaculo == id);
            if (espectaculo == null)
            {
                return NotFound();
            }

            return View(espectaculo);
        }

        // GET: Espectaculo/Create
        public IActionResult Create()
        {
            ViewData["usuarioID"] = new SelectList(_context.usuarios, "ID", "Apellido");
            return View();
        }

        // POST: Espectaculo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idEspectaculo,usuarioID,fechaEspectaculo,cantEntradas")] Espectaculo espectaculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(espectaculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["usuarioID"] = new SelectList(_context.usuarios, "ID", "Apellido", espectaculo.usuarioID);
            return View(espectaculo);
        }

        // GET: Espectaculo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.espectaculo == null)
            {
                return NotFound();
            }

            var espectaculo = await _context.espectaculo.FindAsync(id);
            if (espectaculo == null)
            {
                return NotFound();
            }
            ViewData["usuarioID"] = new SelectList(_context.usuarios, "ID", "Apellido", espectaculo.usuarioID);
            return View(espectaculo);
        }

        // POST: Espectaculo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idEspectaculo,usuarioID,fechaEspectaculo,cantEntradas")] Espectaculo espectaculo)
        {
            if (id != espectaculo.idEspectaculo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(espectaculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspectaculoExists(espectaculo.idEspectaculo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["usuarioID"] = new SelectList(_context.usuarios, "ID", "Apellido", espectaculo.usuarioID);
            return View(espectaculo);
        }

        // GET: Espectaculo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.espectaculo == null)
            {
                return NotFound();
            }

            var espectaculo = await _context.espectaculo
                .Include(e => e.creador)
                .FirstOrDefaultAsync(m => m.idEspectaculo == id);
            if (espectaculo == null)
            {
                return NotFound();
            }

            return View(espectaculo);
        }

        // POST: Espectaculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.espectaculo == null)
            {
                return Problem("Entity set 'TicketeraDataBaseContext.espectaculo'  is null.");
            }
            var espectaculo = await _context.espectaculo.FindAsync(id);
            if (espectaculo != null)
            {
                _context.espectaculo.Remove(espectaculo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspectaculoExists(int id)
        {
          return (_context.espectaculo?.Any(e => e.idEspectaculo == id)).GetValueOrDefault();
        }
    }
}
