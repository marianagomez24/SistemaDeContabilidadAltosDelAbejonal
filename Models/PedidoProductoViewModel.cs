using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    public class PedidoProductoViewModel
    {
        public int IDProveedor { get; set; }
        public List<ProductoSeleccionadoCompra> ProductoSeleccionadoCompras { get; set; } = new List<ProductoSeleccionadoCompra>();
        public string Observaciones { get; set; }
    }

    public class ProductoSeleccionadoCompra
    {
        public int IDProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; } 
        public int Stock { get; set; }

    }
}