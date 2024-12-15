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

            var ultimaVenta = ventas.OrderByDescending(v => v.FechaVenta).FirstOrDefault();
            
            var totalVentas = ventas.Sum(v => v.TotalVenta);
            
            var facturasEnProceso = facturas.Where(f => f.FechaPago == null).ToList();
           
            var facturasPagadas = facturas.Where(f => f.FechaPago != null).ToList();        
            
            var facturasPendientes = facturas.Where(f => f.FechaPago == null).ToList();
            
            var totalPagosPendientes = facturasPendientes.Sum(f => f.Total);
            
            var totalVentasRealizadas = ventas.Count();
           
            var totalStock = productos.Sum(p => p.Stock);
            
            var totalProductos = productos.Count();
            
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