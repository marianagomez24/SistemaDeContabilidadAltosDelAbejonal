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
    public class TipoPresentacionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TipoPresentacions
        public ActionResult Index()
        {
            return View(db.TipoPresentaciones.ToList());
        }

        // GET: TipoPresentacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPresentacion tipoPresentacion = db.TipoPresentaciones.Find(id);
            if (tipoPresentacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoPresentacion);
        }

        // GET: TipoPresentacions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPresentacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTipoPresentacion,NombrePresentacion")] TipoPresentacion tipoPresentacion)
        {
            if (ModelState.IsValid)
            {
                db.TipoPresentaciones.Add(tipoPresentacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoPresentacion);
        }

        // GET: TipoPresentacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPresentacion tipoPresentacion = db.TipoPresentaciones.Find(id);
            if (tipoPresentacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoPresentacion);
        }

        // POST: TipoPresentacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTipoPresentacion,NombrePresentacion")] TipoPresentacion tipoPresentacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoPresentacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoPresentacion);
        }

        // GET: TipoPresentacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPresentacion tipoPresentacion = db.TipoPresentaciones.Find(id);
            if (tipoPresentacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoPresentacion);
        }

        // POST: TipoPresentacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoPresentacion tipoPresentacion = db.TipoPresentaciones.Find(id);
            db.TipoPresentaciones.Remove(tipoPresentacion);
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
