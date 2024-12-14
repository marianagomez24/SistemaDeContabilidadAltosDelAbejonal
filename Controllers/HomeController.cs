using SistemaContabilidadAltosDelAbejonal.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            
            var ventas = await db.Ventas.ToListAsync();
            var usuarios = await db.Usuarios.ToListAsync();
            var facturas = await db.Facturas.ToListAsync();
            var productos = await db.Productos.ToListAsync();
            var pedidos = await db.PedidoProducto.ToListAsync();

            // Obtener la última venta 
            var ultimaVenta = ventas.OrderByDescending(v => v.FechaVenta).FirstOrDefault();

            // Calcular el total de las ventas
            var totalVentas = ventas.Sum(v => v.TotalVenta);

            // Dividir las facturas en proceso de pago y pagadas
            var facturasEnProceso = facturas.Where(f => f.FechaPago == null).ToList();
            var facturasPagadas = facturas.Where(f => f.FechaPago != null).ToList();

            // Filtrar las facturas que están pendientes de pago (FechaPago == null)
            var facturasPendientes = facturas.Where(f => f.FechaPago == null).ToList();

            // Calcular el total de los pagos pendientes (sumando los montos de las facturas pendientes)
            var totalPagosPendientes = facturasPendientes.Sum(f => f.Total);

            // Calcular el total de ventas realizadas (suponiendo que tienes una entidad de ventas como antes)
            var totalVentasRealizadas = ventas.Count();

            // Calcular el monto total del stock
            var totalStock = productos.Sum(p => p.Stock);

            // Calcular el total de productos
            var totalProductos = productos.Count();

            // Crear el ViewModel y pasar las colecciones a él
            var model = new IndexViewModel
            {
                Ventas = ventas,
                Usuarios = usuarios,
                Facturas = facturas,
                Pedidos = pedidos,
                TotalVentas = totalVentas,
                FacturasEnProceso = facturasEnProceso,
                FacturasPagadas = facturasPagadas,
                UltimaVenta = ultimaVenta,
                MontoTotalStock = totalStock,
                TotalPagosPendientes = totalPagosPendientes,
                TotalVentasRealizadas = totalVentasRealizadas,
                Productos = productos,
                TotalProductos = totalProductos
            };

            return View(model);
        }

        public ActionResult AccesoDenegado()
        {
            return View();
        }

        
    }
}