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
    [AuthorizeRole("Administrador")]
    public class TipoGranoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TipoGranoes
        public ActionResult Index()
        {
            return View(db.TipoGranos.ToList());
        }

        // GET: TipoGranoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoGrano tipoGrano = db.TipoGranos.Find(id);
            if (tipoGrano == null)
            {
                return HttpNotFound();
            }
            return View(tipoGrano);
        }

        // GET: TipoGranoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoGranoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTipoGrano,NombreGrano")] TipoGrano tipoGrano)
        {
            if (ModelState.IsValid)
            {
                db.TipoGranos.Add(tipoGrano);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoGrano);
        }

        // GET: TipoGranoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoGrano tipoGrano = db.TipoGranos.Find(id);
            if (tipoGrano == null)
            {
                return HttpNotFound();
            }
            return View(tipoGrano);
        }

        // POST: TipoGranoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTipoGrano,NombreGrano")] TipoGrano tipoGrano)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoGrano).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoGrano);
        }

        // GET: TipoGranoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoGrano tipoGrano = db.TipoGranos.Find(id);
            if (tipoGrano == null)
            {
                return HttpNotFound();
            }
            return View(tipoGrano);
        }

        // POST: TipoGranoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoGrano tipoGrano = db.TipoGranos.Find(id);
            db.TipoGranos.Remove(tipoGrano);
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
