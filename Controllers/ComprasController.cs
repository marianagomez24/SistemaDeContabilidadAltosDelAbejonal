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
    public class ComprasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Compras
        public ActionResult Index()
        {
            var compras = db.Compras.Include(c => c.PedidoProducto).Include(c => c.Proveedor);
            return View(compras.ToList());
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.IDPedido = new SelectList(db.PedidoProducto, "IDPedido", "Observaciones");
            ViewBag.IDProveedor = new SelectList(db.Proveedores, "IDProveedor", "Nombre");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCompra,IDPedido,IDProveedor,FechaCompra,TotalCompra")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Compras.Add(compra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDPedido = new SelectList(db.PedidoProducto, "IDPedido", "Observaciones", compra.IDPedido);
            ViewBag.IDProveedor = new SelectList(db.Proveedores, "IDProveedor", "Nombre", compra.IDProveedor);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPedido = new SelectList(db.PedidoProducto, "IDPedido", "Observaciones", compra.IDPedido);
            ViewBag.IDProveedor = new SelectList(db.Proveedores, "IDProveedor", "Nombre", compra.IDProveedor);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCompra,IDPedido,IDProveedor,FechaCompra,TotalCompra")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDPedido = new SelectList(db.PedidoProducto, "IDPedido", "Observaciones", compra.IDPedido);
            ViewBag.IDProveedor = new SelectList(db.Proveedores, "IDProveedor", "Nombre", compra.IDProveedor);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compra compra = db.Compras.Find(id);
            db.Compras.Remove(compra);
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
