using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    public class VentaDetalle
    {
        [Key]
        public int IDVentaDetalle { get; set; }

        [ForeignKey("Venta")]
        public int IDVenta { get; set; }
        public Venta Venta { get; set; }

        [ForeignKey("Producto")]
        public int? IDProducto { get; set; }
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal TotalProducto => Cantidad * PrecioUnitario;
    }
}