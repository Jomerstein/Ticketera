using System.ComponentModel.DataAnnotations;

namespace ReservaTicket.Models
{
    public class Usuario
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(100)]
        public string NomUsuario { get; set; }

        [Required]
        public string contrasenia  { get; set; }

        [Required]
        public string Direccion { get; set; }

        
        public string Telefono { get; set; }

        [Required]
        [EmailAddress]
        public string Mail { get; set; }

        
    }
}
