using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("CompraDetalle")]
    public class CompraDetalle
    {
        [Key]
        public int IdCompraDetalle { get; set; }

        [ForeignKey("Compra")]
        public int? IDCompra { get; set; }
        public Compra Compra { get; set; }

        [ForeignKey("Producto")]
        public int? IDProducto { get; set; }
        public Producto Producto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal PrecioCompra { get; set; }
        public decimal TotalProducto { get; set; }
    }
}