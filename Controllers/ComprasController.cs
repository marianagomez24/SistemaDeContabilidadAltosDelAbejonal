using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    public class ComprasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComprasController()
        {
            _context = new ApplicationDbContext();

        }

        // GET: Compras
        public ActionResult Compras()
        {
            var compras = _context.Compras.ToList();
            return View(compras);
        }

        // GET: Compras/Details
        public ActionResult Details(int id)
        {
            var compra = _context.Compras.Find(id);

            if (compra == null) return NotFound();
            return View(compra);
        }

        // GET: Compras/Buscar
        public ActionResult Buscar(string proveedor, string producto, DateTime? fecha, int cantidad, decimal precioTotal)
        {
            var compras = _context.Compras.AsQueryable();

            //Busqueda Proveedor
            if (!string.IsNullOrEmpty(proveedor))
            {
                compras = compras.Where(c => c.Proveedor.Contains(proveedor));
            }

            //Busqueda Producto
            if (!string.IsNullOrEmpty(producto))
            {
                compras = compras.Where(c => c.Producto.Contains(producto));
            }

            //Busqueda Fecha
            if (fecha.HasValue)
            {
                compras = compras.Where(c => c.Fecha.Date == fecha.Value.Date);
            }

            //Busqueda Catidad
            if (cantidad.HasValue) 
            { 
                compras = compras.Where(c => c.Cantidad == cantidad.Value);
            }

            //Busqueda PrecioTotal
            if (precioTotal.HasValue)
            {
                compras = compras.Where(c => c.PrecioTotal == precioTotal.Value);
            }

            //Se realiza la consulta
            var resultados = compras.ToList();

            // Si no se encuentran resultados
            if (!resultados.Any()) 
            {
                ViewBag.Mensaje = "No se encontraron datos.";
            }

            return View("Compras",  resultados);

        }
    }
}