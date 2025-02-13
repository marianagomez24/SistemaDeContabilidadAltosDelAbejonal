using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaContabilidadAltosDelAbejonal.Datos;
using SistemaContabilidadAltosDelAbejonal.Models;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    public class VentasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ReporteVentas()
        {
            return View();
        }

        public ActionResult ReporteComparaciones()
        {
            return View();
        }

        public ActionResult Index()
        {
            var ventas = db.Ventas.Include(v => v.Cliente).Include(v => v.Usuario);
            return View(ventas.ToList());
        }
       
        

        // GET: Ventas/Details/5
        public ActionResult Details(int id)
        {
            var venta = db.Ventas
                .Where(c => c.IDVenta == id)
                .FirstOrDefault();

            if (venta == null)
            {
                return HttpNotFound();
            }

            var ventaDetalles = db.VentaDetalles
                .Where(cd => cd.IDVenta == id)
                .Include(cd => cd.Producto)
                .ToList();

            ViewBag.Venta = venta;
            return View(ventaDetalles);
        }

        // GET: Ventas/Create
        public ActionResult Create()
        {
            ViewBag.IDCliente = new SelectList(db.Cliente, "IDCliente", "Nombre");
            ViewBag.IDUsuario = new SelectList(db.Usuarios, "IDUsuario", "Nombre");
            return View();
        }

        // POST: Ventas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDVenta,IDCliente,IDUsuario,FechaVenta,TotalVenta,Observaciones")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Ventas.Add(venta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCliente = new SelectList(db.Cliente, "IDCliente", "Nombre", venta.IDCliente);
            ViewBag.IDUsuario = new SelectList(db.Usuarios, "IDUsuario", "Nombre", venta.IDUsuario);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCliente = new SelectList(db.Cliente, "IDCliente", "Nombre", venta.IDCliente);
            ViewBag.IDUsuario = new SelectList(db.Usuarios, "IDUsuario", "Nombre", venta.IDUsuario);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDVenta,IDCliente,IDUsuario,FechaVenta,TotalVenta,Observaciones")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "La información de la venta fue editada correctamente!";
                return RedirectToAction("Index");
            }
            ViewBag.IDCliente = new SelectList(db.Cliente, "IDCliente", "Nombre", venta.IDCliente);
            ViewBag.IDUsuario = new SelectList(db.Usuarios, "IDUsuario", "Nombre", venta.IDUsuario);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venta venta = db.Ventas.Find(id);
            db.Ventas.Remove(venta);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult ReporteVenta()
        {
            DT_Reporte objDT_Reporte = new DT_Reporte();

            List<ReporteVenta> objLista = objDT_Reporte.RetornarVentas();

            return Json(objLista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ReporteComparacion()
        {
            DT_Reporte dtReporte = new DT_Reporte();

            List<ReporteComparacion> lista = dtReporte.RetornarGastoIngreso();

            return Json(lista, JsonRequestBehavior.AllowGet);
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
