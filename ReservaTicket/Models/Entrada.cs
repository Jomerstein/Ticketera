using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaTicket.Models
{
    public class Entrada
    {
        [Key]
        public int idEntrada { get; set; }
        [Required]
        public Espectaculo espectaculo { get; set; }
        [Required]
        [ForeignKey("espectaculo")]
        public int idEspectaculo { get; set; }
        [Required]
        public bool estaUsada { get; set; }
        [Required]
        public string codigoEntrada { get; set; }
        [Required]
        public bool estaVendida { get; set; }
    }
}
