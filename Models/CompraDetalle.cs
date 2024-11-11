using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("CompraDetalle")]
    public class CompraDetalle
    {
        [ForeignKey("Compra")]
        public int? IDCompra { get; set; }

        [ForeignKey("Producto")]
        public int? IDProducto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal PrecioCompra { get; set; }
        public decimal TotalProducto { get; set; }
    }
}