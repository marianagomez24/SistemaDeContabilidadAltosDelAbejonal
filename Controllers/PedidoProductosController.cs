﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaContabilidadAltosDelAbejonal.Models;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    [AuthorizeRole("Administrador")]
    public class PedidoProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoProductosController()
        {

            _context = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var productos = _context.Productos.Select(p => new ProductoSeleccionadoCompra
            {
                IDProducto = p.IDProducto,
                NombreProducto = p.Nombre,
                PrecioUnitario = p.Precio,
                Stock = p.Stock
            }).ToList();

            var viewModel = new PedidoProductoViewModel
            {
                ProductoSeleccionadoCompras = productos
            };

            ViewBag.Proveedores = _context.Proveedor
                .Where(c => c.Activo == true)
                .Select(c => new { c.IDProveedor, NombreCompleto = c.Nombre})
                .ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PedidoProductoViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Proveedores = _context.Proveedor
                        .Where(c => c.Activo == true)
                        .Select(c => new { c.IDProveedor, NombreCompleto = c.Nombre })
                        .ToList();
                    return View(viewModel);
                }

                var pedidoProducto = new PedidoProducto
                {
                    IDProveedor = viewModel.IDProveedor,
                    FechaPedido = DateTime.Now,
                    FechaEntregaEstimada = DateTime.Now.AddDays(10),
                    Estado = "Pendiente",
                    Observaciones = viewModel.Observaciones
                };

                _context.PedidoProducto.Add(pedidoProducto);
                _context.SaveChanges();

                foreach (var producto in viewModel.ProductoSeleccionadoCompras.Where(p => p.Cantidad > 0))
                {
                    var detalle = new PedidoProductoDetalle
                    {
                        IDPedidoProducto = pedidoProducto.IDPedidoProducto,
                        IDProducto = producto.IDProducto,
                        CantidadPedido = producto.Cantidad,
                        PrecioPedido = producto.PrecioUnitario
                    };

                    _context.PedidoProductoDetalle.Add(detalle);
                    var productoDb = _context.Productos.FirstOrDefault(p => p.IDProducto == producto.IDProducto);
                    if (productoDb != null && productoDb.Stock >= producto.Cantidad)
                    {
                        productoDb.Stock += producto.Cantidad;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Ocurrió un error añadiendo el producto {producto.IDProducto} al Stock.";
                        return RedirectToAction("Create");
                    }
                }

                _context.SaveChanges();
                TempData["SuccessMessage"] = "Pedido agregado correctamente!";
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al intentar crear el pedido.";
                return RedirectToAction("Error", "Home");
            }
        }


        public ActionResult Index()
        {
            var pedidoProducto = _context.PedidoProducto.ToList()
            .Where(c => c.Estado != "Eliminado")
            .ToList();
            var proveedores = _context.Proveedor.Where(c => c.Activo == true).ToList();
            ViewBag.Proveedores = proveedores;
            return View(pedidoProducto);
        }

        public ActionResult Details(int id)
        {
            var pedidoProducto = _context.PedidoProducto
                .Where(c => c.IDPedidoProducto == id)
                .FirstOrDefault();

            if (pedidoProducto == null)
            {
                return HttpNotFound();
            }

            var pedidoProductoDetalles = _context.PedidoProductoDetalle
                .Where(cd => cd.IDPedidoProducto == id)
                .Include(cd => cd.Producto)
                .ToList();

            ViewBag.PedidoProducto = pedidoProducto;
            return View(pedidoProductoDetalles);
        }


        public ActionResult Edit(int id)
        {
            var pedidoProducto = _context.PedidoProducto.FirstOrDefault(c => c.IDPedidoProducto == id);

            if (pedidoProducto == null)
            {
                return HttpNotFound();
            }

            return View(pedidoProducto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PedidoProducto pedidoProducto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pedidoDb = _context.PedidoProducto.FirstOrDefault(c => c.IDPedidoProducto == pedidoProducto.IDPedidoProducto);

                    if (pedidoDb == null)
                    {
                        return HttpNotFound();
                    }

                    pedidoDb.Estado = pedidoProducto.Estado;
                    pedidoDb.Observaciones = pedidoProducto.Observaciones;

                    if (pedidoDb.Estado == "Pagado")
                    {
                        var compra = new Compra
                        {
                            IDProveedor = pedidoDb.IDProveedor,
                            FechaCompra = DateTime.Now
                        };

                        _context.Compra.Add(compra);
                        _context.SaveChanges();

                        var pedidoProductoDetalle = _context.PedidoProductoDetalle
                            .Where(cd => cd.IDPedidoProducto == pedidoDb.IDPedidoProducto)
                            .ToList();

                        foreach (var detalle in pedidoProductoDetalle)
                        {
                            var compraDetalle = new CompraDetalle
                            {
                                IDCompra = compra.IDCompra,
                                IDProducto = detalle.IDProducto,
                                Cantidad = detalle.CantidadPedido,
                                PrecioCompra = detalle.PrecioPedido,
                            };

                            _context.CompraDetalle.Add(compraDetalle);
                        }

                        _context.SaveChanges();
                    }

                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "La información del pedido fue editada correctamente!";
                    return RedirectToAction("Index");
                }
                return View(pedidoProducto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al intentar editar el pedido.";
                return RedirectToAction("Error", "Home");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var pedidoProducto = _context.PedidoProducto.FirstOrDefault(p => p.IDPedidoProducto == id);

                if (pedidoProducto == null)
                {
                    return HttpNotFound();
                }

                pedidoProducto.Estado = "Eliminado";

                _context.SaveChanges();

                TempData["SuccessMessage"] = "El pedido ha sido marcado como eliminado.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al intentar eliminar el pedido.";
                return RedirectToAction("Error", "Home");
            }
        }

    }
}
