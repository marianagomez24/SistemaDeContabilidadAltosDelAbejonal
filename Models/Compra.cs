using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("Compra")]
    public class Compra
    {
        [Key]
        public int IdCompra { get; set; }

        [ForeignKey("PedidoProducto")]
        public int? IDPedido { get; set; }
        public PedidoProducto PedidoProducto { get; set; }

        [ForeignKey("Proveedor")]
        public int? IDProveedor { get; set; }
        public Proveedor Proveedor { get; set; }
        public DateTime FechaCompra { get; set; }
        [Required]
        public decimal TotalCompra { get; set; }

    }
}