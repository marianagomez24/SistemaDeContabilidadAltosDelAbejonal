using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("PedidoProducto")]
    public class PedidoProducto
    {
        [Key]
        public int IDPedidoProducto { get; set; }

        [ForeignKey("Proveedor")]
        public int? IDProveedor { get; set; }
        public Proveedor Proveedor { get; set; }

        public string Estado { get; set; }
        public DateTime FechaPedido { get; set; } = DateTime.Now;
        public DateTime FechaEntregaEstimada { get; set; } = DateTime.Now.AddDays(10);
        public string Observaciones { get; set; }
        public List<PedidoProductoDetalle> Detalles { get; set; }
    }
}