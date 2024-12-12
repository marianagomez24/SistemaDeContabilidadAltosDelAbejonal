using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    public class PedidoProductoDetalle
    {
        [Key]
        public int IDPedidoProductoDetalle { get; set; }

        [ForeignKey("PedidoProducto")]
        public int? IDPedidoProducto { get; set; }
        public PedidoProducto PedidoProducto { get; set; }

        [ForeignKey("Producto")]
        public int? IDProducto { get; set; }
        public Producto Producto { get; set; }

        public int CantidadPedido { get; set; }
        public decimal PrecioPedido { get; set; }
        public decimal TotalProducto => CantidadPedido * PrecioPedido;
    }
}