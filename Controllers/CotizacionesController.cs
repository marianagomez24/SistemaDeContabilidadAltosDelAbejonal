using SistemaContabilidadAltosDelAbejonal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    public class CotizacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CotizacionesController()
        {

            _context = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var productos = _context.Productos.Select(p => new ProductoSeleccionado
            {
                IDProducto = p.IDProducto,
                NombreProducto = p.Nombre,
                PrecioUnitario = p.Precio,
                Stock = p.Stock
            }).ToList();

            var viewModel = new CotizacionViewModel
            {
                ProductosSeleccionados = productos
            };

            ViewBag.Clientes = _context.Cliente
                .Select(c => new { c.IDCliente, NombreCompleto = c.Nombre + " " + c.PrimerApellido })
                .ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CotizacionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Clientes = _context.Cliente
                    .Select(c => new { c.IDCliente, NombreCompleto = c.Nombre + " " + c.PrimerApellido })
                    .ToList();
                return View(viewModel);
            }

            var cotizacion = new Cotizacion
            {
                IDCliente = viewModel.IDCliente,
                FechaCotizacion = DateTime.Now,
                Estado = "Pendiente",
                TotalSinIVA = viewModel.ProductosSeleccionados.Sum(p => p.Cantidad * p.PrecioUnitario),
                IVA = viewModel.ProductosSeleccionados.Sum(p => p.Cantidad * p.PrecioUnitario) * 0.16m,
                TotalConIVA = viewModel.ProductosSeleccionados.Sum(p => p.Cantidad * p.PrecioUnitario * 1.16m),
                Observaciones = viewModel.Observaciones
            };

            _context.Cotizaciones.Add(cotizacion);
            _context.SaveChanges();

            foreach (var producto in viewModel.ProductosSeleccionados.Where(p => p.Cantidad > 0))
            {
                var detalle = new CotizacionDetalle
                {
                    IDCotizacion = cotizacion.IDCotizacion,
                    IDProducto = producto.IDProducto,
                    Cantidad = producto.Cantidad,
                    PrecioUnitario = producto.PrecioUnitario
                };

                _context.CotizacionDetalles.Add(detalle);
            }

            _context.SaveChanges();

            return RedirectToAction("Create");
        }

        public ActionResult Index()
        {
            var cotizaciones = _context.Cotizaciones.ToList();
            var clientes = _context.Cliente.ToList();
            ViewBag.Clientes = clientes;
            return View(cotizaciones);
        }

        public ActionResult Details(int id)
        {
            // Obtener la cotización con sus detalles
            var cotizacion = _context.Cotizaciones
                .Where(c => c.IDCotizacion == id)
                .FirstOrDefault();

            if (cotizacion == null)
            {
                return HttpNotFound();
            }

            // Obtener los detalles de la cotización
            var cotizacionDetalles = _context.CotizacionDetalles
                .Where(cd => cd.IDCotizacion == id)
                .Include(cd => cd.Producto) // Incluir información del producto
                .ToList();

            ViewBag.Cotizacion = cotizacion;
            return View(cotizacionDetalles);
        }
    }
}