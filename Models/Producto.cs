using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        public int IDProducto { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [ForeignKey("TipoGrano")]
        public int? IDTipoGrano { get; set; }
        public TipoGrano TipoGrano { get; set; }

        [ForeignKey("TipoPresentacion")]
        public int? IDTipoPresentacion { get; set; }
        public TipoPresentacion TipoPresentacion { get; set; }

        [ForeignKey("CategoriaProducto")]
        public int? IDCategoria { get; set; }
        public CategoriaProducto CategoriaProducto { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public int Stock { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}