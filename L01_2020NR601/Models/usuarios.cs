using System.ComponentModel.DataAnnotations;

namespace L01_2020NR601.Models
{
    public class usuarios
    {
        [Key]
        public int usuarioID { get; set; }
        public int rolID { get; set; }

        public string? nombreUsuario { get; set; }

        public string? clave { get; set; }

        public string? nombre { get; set; }

        public string? apellido { get; set; }
    }
}
