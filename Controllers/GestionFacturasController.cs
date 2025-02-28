using SistemaContabilidadAltosDelAbejonal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    [AuthorizeRole("Administrador", "Contador")]
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
            var facturas = _context.Facturas.Where(f => !f.Activo).ToList();
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
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Facturas.Add(factura);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Factura agregada correctamente!";
                    return RedirectToAction("Facturas");
                }
                return View(factura);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al agregar la factura.";
                return RedirectToAction("Error", "Home");
            }
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
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Entry(factura).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "La información de la factura fue editada correctamente!";
                    return RedirectToAction("Facturas");
                }
                return View(factura);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al editar la factura.";
                return RedirectToAction("Error", "Home");
            }
        }


        // GET: Eliminar Facturas
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var factura = _context.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }

            return View(factura);
        }

        // POST: Eliminar Facturas
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var factura = _context.Facturas.Find(id);

                if (factura == null)
                {
                    return HttpNotFound();
                }

                factura.Activo = true;

                _context.Entry(factura).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();

                TempData["SuccessMessage"] = "La factura ha sido eliminada correctamente.";
                return RedirectToAction("Facturas");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al intentar eliminar la factura.";
                return RedirectToAction("Error", "Home");
            }
        }

    }
}