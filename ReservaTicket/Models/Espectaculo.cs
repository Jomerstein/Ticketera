using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ReservaTicket.Models
{
    public class Espectaculo
    {
        [Key]
        public int idEspectaculo { get; set; }

        [Display(Name = "UsuarioId")]
        public int usuarioID { get; set; }

    

        [Required]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fechaEspectaculo { get; set; }

        [Required]
        [ForeignKey("usuarioID")]
        public virtual Usuario creador { get; set; }

        [Required]
        public int cantEntradas { get; set; }

  
    }
}
