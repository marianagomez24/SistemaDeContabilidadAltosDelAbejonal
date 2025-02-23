using SistemaContabilidadAltosDelAbejonal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            TempData["SuccessMessage"] = "Cotización creada correctamente!";
            return RedirectToAction("Create");
        }

        public ActionResult Index()
        {
            var cotizaciones = _context.Cotizaciones.ToList()
            .Where(c => c.Estado != "Eliminado")
            .ToList();
            var clientes = _context.Cliente.ToList();
            ViewBag.Clientes = clientes;
            return View(cotizaciones);
        }

        public ActionResult Details(int id)
        {
            var cotizacion = _context.Cotizaciones
                .Where(c => c.IDCotizacion == id)
                .FirstOrDefault();

            if (cotizacion == null)
            {
                return HttpNotFound();
            }

            var cotizacionDetalles = _context.CotizacionDetalles
                .Where(cd => cd.IDCotizacion == id)
                .Include(cd => cd.Producto) 
                .ToList();

            ViewBag.Cotizacion = cotizacion;
            return View(cotizacionDetalles);
        }


        public ActionResult Edit(int id)
        {
            var cotizacion = _context.Cotizaciones.FirstOrDefault(c => c.IDCotizacion == id);

            if (cotizacion == null)
            {
                return HttpNotFound();
            }

            return View(cotizacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cotizacion cotizacion)
        {
            if (ModelState.IsValid)
            {
                var cotizacionDb = _context.Cotizaciones.FirstOrDefault(c => c.IDCotizacion == cotizacion.IDCotizacion);

                if (cotizacionDb == null)
                {
                    return HttpNotFound();
                }

                cotizacionDb.Estado = cotizacion.Estado;
                cotizacionDb.TotalSinIVA = cotizacion.TotalSinIVA;
                cotizacionDb.IVA = cotizacion.IVA;
                cotizacionDb.TotalConIVA = cotizacion.TotalConIVA;
                cotizacionDb.Observaciones = cotizacion.Observaciones;

                if (cotizacionDb.Estado == "Pagado")
                {
                    var usuarioSesion = Session["UsuarioID"] as int?;

                    if (usuarioSesion == null)
                    {
                        
                        return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                    }
                    var venta = new Venta
                    {
                        IDCliente = cotizacionDb.IDCliente,
                        IDUsuario = usuarioSesion.Value,
                        FechaVenta = DateTime.Now,
                        TotalVenta = cotizacionDb.TotalConIVA, 
                        Observaciones = cotizacionDb.Observaciones
                    };

                    _context.Ventas.Add(venta);
                    _context.SaveChanges();

                    var cotizacionDetalles = _context.CotizacionDetalles.Where(cd => cd.IDCotizacion == cotizacionDb.IDCotizacion).ToList();

                    foreach (var detalle in cotizacionDetalles)
                    {
                        var ventaDetalle = new VentaDetalle
                        {
                            IDVenta = venta.IDVenta,
                            IDProducto = detalle.IDProducto,
                            Cantidad = detalle.Cantidad,
                            PrecioUnitario = detalle.PrecioUnitario,
                        };

                        _context.VentaDetalles.Add(ventaDetalle);
                    }

                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "La cotización ha sido registrada en las ventas!";
                }

                _context.SaveChanges();
                TempData["SuccessMessage"] = "La información de la cotización fue editada correctamente!";
                return RedirectToAction("Index");
            }
            return View(cotizacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var cotizacion = _context.Cotizaciones.FirstOrDefault(c => c.IDCotizacion == id);

            if (cotizacion == null)
            {
                return HttpNotFound();
            }

            cotizacion.Estado = "Eliminado";

            _context.SaveChanges();

            TempData["SuccessMessage"] = "La cotización ha sido marcada como eliminada.";
            return RedirectToAction("Index");
        }


    }
}