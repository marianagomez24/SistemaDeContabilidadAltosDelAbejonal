using SistemaContabilidadAltosDelAbejonal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    public class GestionFacturasController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GestionFacturasController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: GestionFacturas
        public ActionResult Facturas()
        {
            var facturas = _context.Facturas.ToList();
            return View(facturas);
        }

        // GET: Crear Facturas
        public ActionResult Create()
        {
            return View();
        }

        // POST: Crear Facturas
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Facturas.Add(factura);
                _context.SaveChanges();
                return RedirectToAction("Facturas");
            }

            var errors = ModelState.Values.SelectMany(x => x.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(factura);
        }

        // GET: Editar Facturas
        public ActionResult Edit(int? id)
        {
            var factura = _context.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Editar Facturas
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(factura).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Facturas");
            }
            return View(factura);
        }
    }
}