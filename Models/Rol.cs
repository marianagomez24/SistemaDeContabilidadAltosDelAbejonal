using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("Rol")]
    public class Rol
    {
        [Key]
        public int IDRol { get; set; }
        [Required]
        [StringLength(50)]
        public string NombreRol { get; set; }
        [StringLength(255)]
        public string Descripcion { get; set; }
    }
}