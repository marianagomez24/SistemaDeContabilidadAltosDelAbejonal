using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Venta> Ventas { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
        public IEnumerable<Factura> Facturas { get; set; }
        public IEnumerable<PedidoProducto> Pedidos { get; set; }
        public Venta UltimaVenta { get; set; }
        public decimal TotalVentas { get; set; }
        public List<Factura> FacturasEnProceso { get; set; }
        public List<Factura> FacturasPagadas { get; set; }
        public int MontoTotalStock { get; set; }
        public decimal TotalPagosPendientes { get; set; }
        public int TotalVentasRealizadas { get; set; }
        public IEnumerable<Producto> Productos { get; set; }
        public int TotalProductos { get; set; }
    }
}