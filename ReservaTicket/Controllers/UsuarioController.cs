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
    public class UsuarioController : Controller
    {
        private readonly TicketeraDataBaseContext _context;
      //  private Usuario usuarioCont;

        public UsuarioController(TicketeraDataBaseContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
              return _context.usuarios != null ? 
                          View(await _context.usuarios.ToListAsync()) :
                          Problem("Entity set 'TicketeraDataBaseContext.usuarios'  is null.");
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m.ID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Apellido,NomUsuario,contrasenia,Direccion,Telefono,Mail")] Usuario usuario)
        {
           
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("elloco", usuario.Mail);
               
                return RedirectToAction("Index", "Home");
                
                

            }
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Apellido,NomUsuario,contrasenia,Direccion,Telefono,Mail")] Usuario usuario)
        {
            if (id != usuario.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.ID))
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
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m.ID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.usuarios == null)
            {
                return Problem("Entity set 'TicketeraDataBaseContext.usuarios'  is null.");
            }
            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.usuarios?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(String Mail, String contrasenia)
        {
            if (ModelState.IsValid)
            {
                var usuarioDb = await _context.usuarios.FirstOrDefaultAsync(us => us.Mail == Mail && us.contrasenia == contrasenia);
                if (usuarioDb == null)
                {
                    ViewBag.Error = "Datos erroneos, intente nuevamente.";
                    return View();
                }
               // usuarioCont = usuarioDb;
               HttpContext.Session.SetString("elloco", usuarioDb.Mail);
                Console.WriteLine(HttpContext.Session.GetString("elloco"));
                return RedirectToAction("Index", "Home");
            }
            return View(null);
        }

        public RedirectToActionResult Logout()
        {
            HttpContext.Session.SetString("elloco", string.Empty);
            Console.WriteLine(HttpContext.Session.GetString("elloco"));
            return RedirectToAction("Index", "Home");

        }
    }
}
