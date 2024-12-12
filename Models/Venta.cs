using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    public class Venta
    {
        [Key]
        public int IDVenta { get; set; }

        [ForeignKey("Cliente")]
        public int? IDCliente { get; set; }
        public Cliente Cliente { get; set; }

        [ForeignKey("Usuario")]
        public int? IDUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public DateTime FechaVenta { get; set; }
        public decimal TotalVenta { get; set; }
        public string Observaciones { get; set; }
        public ICollection<VentaDetalle> VentaDetalles { get; set; }
    }
}