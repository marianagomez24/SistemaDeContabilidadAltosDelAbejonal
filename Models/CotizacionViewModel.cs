using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    public class CotizacionViewModel
    {
        public int IDCliente { get; set; } 
        public List<ProductoSeleccionado> ProductosSeleccionados { get; set; } = new List<ProductoSeleccionado>();
        public string Observaciones { get; set; } 
    }

    public class ProductoSeleccionado
    {
        public int IDProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }

    }
}