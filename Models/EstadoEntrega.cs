using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("EstadoEntrega")]
    public class EstadoEntrega
    {
        [Key]
        public int IDEstado { get; set; }
        [Required]
        [StringLength(50)]
        public string Estado { get; set; }
    }
}