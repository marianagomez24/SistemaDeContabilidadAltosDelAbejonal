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
    [AuthorizeRole("Administrador", "Contador")]
    public class ComprasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Compras
        public ActionResult Index()
        {
            var compra = db.Compra.Include(c => c.Proveedor);
            return View(compra.ToList());
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            var compra = db.Compra
                .Where(c => c.IDCompra == id)
                .FirstOrDefault();

            if (compra == null)
            {
                return HttpNotFound();
            }

            var compraDetalles = db.CompraDetalle
                .Where(cd => cd.IDCompra == id)
                .Include(cd => cd.Producto)
                .ToList();

            ViewBag.Compra = compra;
            return View(compraDetalles);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.IDPedidoProducto = new SelectList(db.PedidoProducto, "IDPedidoProducto", "Observaciones");
            ViewBag.IDProveedor = new SelectList(db.Proveedor, "IDProveedor", "Nombre");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCompra,IDPedidoProducto,IDProveedor,FechaCompra,TotalCompra")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Compra.Add(compra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDPedidoProducto = new SelectList(db.PedidoProducto, "IDPedidoProducto", "Observaciones");
            ViewBag.IDProveedor = new SelectList(db.Proveedor, "IDProveedor", "Nombre", compra.IDProveedor);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPedidoProducto = new SelectList(db.PedidoProducto, "IDPedidoProducto", "Observaciones");
            ViewBag.IDProveedor = new SelectList(db.Proveedor, "IDProveedor", "Nombre", compra.IDProveedor);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCompra,IDPedidoProducto,IDProveedor,FechaCompra,TotalCompra, Observaciones")] Compra compra)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(compra).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Compra editada correctamente!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al editar la compra.";
                return RedirectToAction("Error", "Home");
            }

            ViewBag.IDPedidoProducto = new SelectList(db.PedidoProducto, "IDPedidoProducto", "Observaciones");
            ViewBag.IDProveedor = new SelectList(db.Proveedor, "IDProveedor", "Nombre", compra.IDProveedor);
            return View(compra);
        }


        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compra.Find(id);
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
            try
            {
                Compra compra = db.Compra.Find(id);

                if (compra == null)
                {
                    return HttpNotFound();
                }

                db.Compra.Remove(compra);
                db.SaveChanges();

                TempData["SuccessMessage"] = "La compra ha sido eliminada correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al intentar eliminar la compra.";
                return RedirectToAction("Error", "Home");
            }
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
