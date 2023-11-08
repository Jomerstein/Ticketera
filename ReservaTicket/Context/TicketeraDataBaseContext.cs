using Microsoft.EntityFrameworkCore;
using ReservaTicket.Models;

namespace ReservaTicket.Context
{
    public class TicketeraDataBaseContext :DbContext
    {
        public TicketeraDataBaseContext(DbContextOptions<TicketeraDataBaseContext> options) : base(options)
        {
        }
        public DbSet<Entrada> entrada { get; set; }

        public DbSet<Usuario> usuarios { get; set; }

        public DbSet<Espectaculo> espectaculo { get; set; }
    }
}
