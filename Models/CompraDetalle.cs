using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("CompraDetalle")]
    public class CompraDetalle
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Compra")]
        public int? IDCompra { get; set; }
        public Compra Compra { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Producto")]
        public int? IDProducto { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal TotalProducto { get; set; }
    }
}