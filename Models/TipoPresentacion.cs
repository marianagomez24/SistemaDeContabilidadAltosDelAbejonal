using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("TipoPresentacion")]
    public class TipoPresentacion
    {
        [Key]
        public int IDTipoPresentacion { get; set; }

        [Required]
        [StringLength(50)]
        public string NombrePresentacion { get; set; }

        public List<Producto> Productos { get; set; }
    }
}