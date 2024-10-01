using System;
using System.Linq;
using System.Web.Mvc;
using SistemaContabilidadAltosDelAbejonal.Models;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    public class VentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentasController()
        {
            _context = new ApplicationDbContext(); 
        }

        public ActionResult Index()
        {
            var ventas = _context.Venta.ToList(); 
            return View(ventas);
        }

        // Acción para crear los reportes.
        public ActionResult GenerarReporte(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var ventas = _context.Venta.AsQueryable();

            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                ventas = ventas.Where(v => v.FechaVenta >= fechaInicio.Value && v.FechaVenta <= fechaFin.Value);
            }

            var ventasReporte = ventas.ToList();

            // Calcula el monto total de las ventas
            decimal totalVentas = ventasReporte.Sum(v => v.TotalVenta);

            // Añadir el total de ventas al modelo
            ViewBag.TotalVentas = totalVentas;

            return View(ventasReporte); // Retorna directamente las ventas
        }



    }
}
