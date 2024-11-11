using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("CotizacionDetalle")]
    public class CotizacionDetalle
    {
        [Key]
        public int IDCotizacionDetalle { get; set; }

        [ForeignKey("Cotizacion")]
        public int? IDCotizacion { get; set; }
        public Cotizacion Cotizacion { get; set; }
        [ForeignKey("Producto")]
        public int? IDProducto { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal TotalProducto { get; set; }
    }
}