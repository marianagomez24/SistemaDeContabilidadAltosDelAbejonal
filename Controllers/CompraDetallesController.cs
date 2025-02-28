using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaContabilidadAltosDelAbejonal.Models;
using SistemaContabilidadAltosDelAbejonal.Datos;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    [AuthorizeRole("Administrador", "Contador")]
    public class CompraDetallesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ReporteCompras()
        {
            return View();
        }

        public ActionResult ReporteComprasProveedores()
        {
            return View();
        }

        public ActionResult Index()
        {
            var compraDetalle = db.CompraDetalle.Include(c => c.Compra).Include(c => c.Producto);
            return View(compraDetalle.ToList());
        }

        // GET: CompraDetalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Buscar el detalle de compra por su ID
            CompraDetalle compraDetalle = db.CompraDetalle
                .Include(c => c.Compra) // Incluir datos relacionados de Compra
                .Include(c => c.Producto) // Incluir datos relacionados de Producto
                .FirstOrDefault(c => c.IDCompraDetalle == id);

            if (compraDetalle == null)
            {
                return HttpNotFound();
            }

            return View(compraDetalle);
        }

        // GET: CompraDetalles/Create
        public ActionResult Create()
        {
            ViewBag.IDCompra = new SelectList(db.Compra, "IDCompra", "IDCompra");
            ViewBag.IDProducto = new SelectList(db.Productos, "IDProducto", "Nombre");
            return View();
        }

        // POST: CompraDetalles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCompra,IDProducto,Cantidad,PrecioCompra,TotalProducto")] CompraDetalle compraDetalle)
        {
            if (ModelState.IsValid)
            {
                db.CompraDetalle.Add(compraDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCompra = new SelectList(db.Compra, "IDCompra", "IDCompra", compraDetalle.IDCompra);
            ViewBag.IDProducto = new SelectList(db.Productos, "IDProducto", "Nombre", compraDetalle.IDProducto);
            return View(compraDetalle);
        }

        // GET: CompraDetalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompraDetalle compraDetalle = db.CompraDetalle.Find(id);
            if (compraDetalle == null)
            {
                return HttpNotFound();
            }
            return View(compraDetalle);
        }

        // POST: CompraDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompraDetalle compraDetalle = db.CompraDetalle.Find(id);
            db.CompraDetalle.Remove(compraDetalle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult ReporteCompra()
        {
            try
            {
                DT_Reporte objDT_Reporte = new DT_Reporte();
                List<ReporteCompra> objLista = objDT_Reporte.RetornarCompras();
                return Json(objLista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = "Ocurrió un error al obtener el reporte de compras." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ReporteCompraProveedor()
        {
            try
            {
                DT_Reporte objDT_Reporte = new DT_Reporte();
                List<ReporteCompraProveedor> objLista = objDT_Reporte.RetornarProveedores();
                return Json(objLista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = "Ocurrió un error al obtener el reporte de compras por proveedor." }, JsonRequestBehavior.AllowGet);
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
