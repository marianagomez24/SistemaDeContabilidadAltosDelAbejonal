using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("PedidoProductoDetalle")]
    public class PedidoProductoDetalle
    {
        [Key]
        public int IDPedido { get; set; }

        [ForeignKey("Producto")]
        public int? IDProducto { get; set; }
        public Producto Producto { get; set; }
        [Required]
        public int CantidadPedido { get; set; }
        [Required]
        public decimal PrecioPedido { get; set; }
        public decimal TotalProducto { get; set; }
    }
}