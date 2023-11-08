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
    public class EntradaController : Controller
    {
        private readonly TicketeraDataBaseContext _context;

        public EntradaController(TicketeraDataBaseContext context)
        {
            _context = context;
        }

        // GET: Entrada
        public async Task<IActionResult> Index()
        {
            var ticketeraDataBaseContext = _context.entrada.Include(e => e.espectaculo);
            return View(await ticketeraDataBaseContext.ToListAsync());
        }

        // GET: Entrada/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.entrada == null)
            {
                return NotFound();
            }

            var entrada = await _context.entrada
                .Include(e => e.espectaculo)
                .FirstOrDefaultAsync(m => m.idEntrada == id);
            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // GET: Entrada/Create
        public IActionResult Create()
        {
            ViewData["idEspectaculo"] = new SelectList(_context.espectaculo, "idEspectaculo", "idEspectaculo");
            return View();
        }

        // POST: Entrada/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idEntrada,idEspectaculo,estaUsada")] Entrada entrada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entrada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idEspectaculo"] = new SelectList(_context.espectaculo, "idEspectaculo", "idEspectaculo", entrada.idEspectaculo);
            return View(entrada);
        }

        // GET: Entrada/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.entrada == null)
            {
                return NotFound();
            }

            var entrada = await _context.entrada.FindAsync(id);
            if (entrada == null)
            {
                return NotFound();
            }
            ViewData["idEspectaculo"] = new SelectList(_context.espectaculo, "idEspectaculo", "idEspectaculo", entrada.idEspectaculo);
            return View(entrada);
        }

        // POST: Entrada/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idEntrada,idEspectaculo,estaUsada")] Entrada entrada)
        {
            if (id != entrada.idEntrada)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entrada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntradaExists(entrada.idEntrada))
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
            ViewData["idEspectaculo"] = new SelectList(_context.espectaculo, "idEspectaculo", "idEspectaculo", entrada.idEspectaculo);
            return View(entrada);
        }

        // GET: Entrada/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.entrada == null)
            {
                return NotFound();
            }

            var entrada = await _context.entrada
                .Include(e => e.espectaculo)
                .FirstOrDefaultAsync(m => m.idEntrada == id);
            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // POST: Entrada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.entrada == null)
            {
                return Problem("Entity set 'TicketeraDataBaseContext.entrada'  is null.");
            }
            var entrada = await _context.entrada.FindAsync(id);
            if (entrada != null)
            {
                _context.entrada.Remove(entrada);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntradaExists(int id)
        {
          return (_context.entrada?.Any(e => e.idEntrada == id)).GetValueOrDefault();
        }
    }
}
