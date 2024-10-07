using System;
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
    public class PedidoProductosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var pedidoProductos = db.PedidoProductos.Include(p => p.EstadoEntrega).Include(p => p.Proveedor);
            return View(pedidoProductos.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.IDEstado = new SelectList(db.EstadoEntregas, "IDEstado", "Estado");
            ViewBag.IDProveedor = new SelectList(db.Proveedores, "IDProveedor", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPedido,IDProveedor,IDEstado,FechaPedido,FechaEntregaEstimada,Observaciones")] PedidoProducto pedidoProducto)
        {
            if (ModelState.IsValid)
            {
                db.PedidoProductos.Add(pedidoProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDEstado = new SelectList(db.EstadoEntregas, "IDEstado", "Estado", pedidoProducto.IDEstado);
            ViewBag.IDProveedor = new SelectList(db.Proveedores, "IDProveedor", "Nombre", pedidoProducto.IDProveedor);
            return View(pedidoProducto);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoProducto pedidoProducto = db.PedidoProductos.Find(id);
            if (pedidoProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDEstado = new SelectList(db.EstadoEntregas, "IDEstado", "Estado", pedidoProducto.IDEstado);
            ViewBag.IDProveedor = new SelectList(db.Proveedores, "IDProveedor", "Nombre", pedidoProducto.IDProveedor);
            return View(pedidoProducto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPedido,IDProveedor,IDEstado,FechaPedido,FechaEntregaEstimada,Observaciones")] PedidoProducto pedidoProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidoProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDEstado = new SelectList(db.EstadoEntregas, "IDEstado", "Estado", pedidoProducto.IDEstado);
            ViewBag.IDProveedor = new SelectList(db.Proveedores, "IDProveedor", "Nombre", pedidoProducto.IDProveedor);
            return View(pedidoProducto);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoProducto pedidoProducto = db.PedidoProductos.Find(id);
            if (pedidoProducto == null)
            {
                return HttpNotFound();
            }
            return View(pedidoProducto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PedidoProducto pedidoProducto = db.PedidoProductos.Find(id);
            db.PedidoProductos.Remove(pedidoProducto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
