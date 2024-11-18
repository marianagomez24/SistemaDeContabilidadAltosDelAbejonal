using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("PedidoProducto")]
    public class PedidoProducto
    {
        [Key]
        public int IDPedido { get; set; }

        [ForeignKey("Proveedor")]
        public int? IDProveedor { get; set; }
        public Proveedor Proveedor { get; set; }

        [ForeignKey("EstadoEntrega")]
        public int? IDEstado { get; set; }
        public EstadoEntrega EstadoEntrega { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaEntregaEstimada { get; set; }
        public string Observaciones { get; set; }
    }
}