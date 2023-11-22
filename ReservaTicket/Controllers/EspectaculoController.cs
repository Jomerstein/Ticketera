using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservaTicket.Context;
using ReservaTicket.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net.Mail;
using iTextSharp.text.pdf.qrcode;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing.Imaging;
using System.Net;

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
            if (System.String.IsNullOrEmpty(HttpContext.Session.GetString("elloco")))
            {
                TempData["Error"] = "Debe iniciar sesion para poder sacar entradas";
                return RedirectToAction("Indexeado", "Espectaculo");
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
        public async Task<IActionResult> Create([Bind("idEspectaculo,nombreEspectaculo,fechaEspectaculo,cantEntradas,descripcionEspectaculo")] Espectaculo espectaculo)
        {
            var listaUsuarios = from u in _context.usuarios.ToList() select u;
            var usuarioCreador = listaUsuarios.FirstOrDefault(usuario => usuario.Mail == HttpContext.Session.GetString("elloco"));

            ModelState.Remove("usuarioID");
            ModelState.Remove("creador");

            if(espectaculo.fechaEspectaculo < DateTime.Today)
            {
                ModelState.AddModelError("fechaEspectaculo", "Fecha inválida");
                return View(espectaculo);
            }
            
         if (ModelState.IsValid)
            {
                espectaculo.usuarioID = usuarioCreador.ID;


                espectaculo.creador = usuarioCreador;
                _context.Add(espectaculo);
            
            
                await _context.SaveChangesAsync();
                await generarEntradas(espectaculo.cantEntradas, espectaculo);
            
               return RedirectToAction("Indexeado", "Espectaculo");
               
          
           }
            ViewData["usuarioID"] = new SelectList(_context.usuarios, "ID", "Apellido", espectaculo.usuarioID);
            return View(espectaculo);
        }



        private async Task generarEntradas(int cantidad, Espectaculo espectaculo)
        {
            
            for (int i = 0; i < cantidad; i++)
            {
                Entrada entradaNueva = new Entrada
                {
                    estaVendida = false,
                estaUsada = false,
                espectaculo = espectaculo,
                idEspectaculo = espectaculo.idEspectaculo,
                codigoEntrada = generarCodigoEntradas()
            };
                
                _context.Add(entradaNueva);
                

            }
            await _context.SaveChangesAsync();
        }

        private string generarCodigoEntradas()
        {
            const string caracteresPosibles = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder stringBuilder = new StringBuilder();

            Random random = new Random();

            for (int i = 0; i < 20; i++)
            {
                int indiceCaracter = random.Next(caracteresPosibles.Length);
                char caracterAleatorio = caracteresPosibles[indiceCaracter];
                stringBuilder.Append(caracterAleatorio);
            }

            return stringBuilder.ToString();
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

        public async Task<IActionResult> Indexeado()
        {
            var ticketeraDataBaseContext = _context.espectaculo.Include(e => e.creador);
            var lista = await ticketeraDataBaseContext.ToListAsync();
            lista.Reverse();
            return View(lista) ;
           
        }



        public async Task<IActionResult> Reservar(int id)
        {
            var espectaculo = _context.espectaculo.FirstOrDefault(e => e.idEspectaculo == id);
            var entradaReservada =  _context.entrada.FirstOrDefault(e => e.estaVendida == false && e.idEspectaculo == espectaculo.idEspectaculo);

            if (entradaReservada != null)
            {


                entradaReservada.estaVendida = true;


                QrEncoder encoder = new QrEncoder(Gma.QrCodeNet.Encoding.ErrorCorrectionLevel.M);
                QrCode qrCode;
                encoder.TryEncode(entradaReservada.codigoEntrada, out qrCode);

                GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
                MemoryStream ms = new MemoryStream();
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);

                Bitmap codigoQRELLOCO = new Bitmap(ms);
                




                _context.SaveChanges();
               


                //EnviarCorreoConAdjunto(HttpContext.Session.GetString("elloco"),"este es el asunto", "este es el cuerpo", byteImage , "Entrada_para_espectaculo.pdf");
                return new Rotativa.AspNetCore.ViewAsPdf("Reservar", codigoQRELLOCO)
                {
                    FileName = $"Entrada_para_espectaculo.pdf"

                };
                }
            else
            {
                TempData["Error"] = "Lo lamentamos, no quedaron mas entradas";
                return RedirectToAction("Indexeado", "Espectaculo");
            }
            
        }

     //   private void EnviarCorreoConAdjunto(string destinatario, string asunto, string cuerpo, byte[] vista, string nombreAdjunto)
     //   {
      //      using (MailMessage mail = new MailMessage())
      //      {
      //          mail.From = new MailAddress("ticketeraort@gmail.com");
       //         mail.To.Add(destinatario);
       //         mail.Subject = asunto;
       //         mail.Body = cuerpo;
                
                // Adjunta el PDF al correo
       //         Attachment attachment = new Attachment(new MemoryStream(vista), nombreAdjunto, "entrada.pdf");
       //         mail.Attachments.Add(attachment);
                
                
           

       //         SmtpClient smtp = new SmtpClient("smtp.gmail.com");
        //        smtp.Port = 587;
       //         smtp.Credentials = new NetworkCredential("ticketeraort@gmail.com", "fztv nkpr ceiv wquw");
       //         smtp.EnableSsl = true;

           //     smtp.Send(mail);
            //}
       // }



            public async Task<IActionResult> CheckearEntrada(string id)
        {
           var entradaEnCuestion = _context.entrada.FirstOrDefault(e => e.codigoEntrada == id);
            if(entradaEnCuestion == null)
            {
                TempData["ResultadoOperacion"] = "Error de admisión: La entrada no existe / eso no es una entrada brother";
                return View();
            }
            var espectaculo = _context.espectaculo.FirstOrDefault(e => e.idEspectaculo == entradaEnCuestion.idEspectaculo);
            if (espectaculo == null)
            {
                TempData["ResultadoOperacion"] = "Error de admisión: El espectáculo no existe";
                return View(entradaEnCuestion);
            }
            if(espectaculo.idEspectaculo != entradaEnCuestion.idEspectaculo)
            {
                TempData["ResultadoOperacion"] = "Error de admisión: La entrada es de otro espectaculo";
            }
          
            var usuario = _context.usuarios.FirstOrDefault(e => e.Mail == HttpContext.Session.GetString("elloco"));
            if(usuario == null || espectaculo.usuarioID != usuario.ID)
            {
                TempData["ResultadoOperacion"] = "Error de admisión: Usted no tiene permiso para scannear esta entrada";
                return View(entradaEnCuestion);
            }

            if(entradaEnCuestion.estaUsada == true)
            {
                TempData["ResultadoOperacion"] = "Error de admisión: La entrada ya fué canjeada";
                return View(entradaEnCuestion);
            }

            entradaEnCuestion.estaUsada = true;

            _context.SaveChanges();

            TempData["ResultadoOperacion"] = "Entrada canjeada con éxito";
            return View(entradaEnCuestion);







        }
        

    }
    

}
