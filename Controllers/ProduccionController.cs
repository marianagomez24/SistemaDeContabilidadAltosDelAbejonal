using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaContabilidadAltosDelAbejonal.Models;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    [AuthorizeRole("Administrador")]
    public class ProduccionController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProduccionController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Produccion
        public ActionResult Produccion()
        {
            var producciones = _context.Produccion.ToList();
            return View(producciones);
        }

        // GET: Produccion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produccion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produccion produccion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Produccion.Add(produccion);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Producción agregada correctamente!";
                    return RedirectToAction("Produccion");
                }

                return View(produccion);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al agregar la producción.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Produccion/Edit/5
        public ActionResult Edit(int id)
        {
            var produccion = _context.Produccion.Find(id);
            if (produccion == null)
            {
                return HttpNotFound();
            }
            return View(produccion);
        }

        // POST: Produccion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produccion produccion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Entry(produccion).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "La producción fue editada correctamente!";
                    return RedirectToAction("Produccion");
                }
                return View(produccion);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al editar la producción.";
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Produccion/Delete/5
        public ActionResult Delete(int id)
        {
            var produccion = _context.Produccion.Find(id);
            if (produccion == null)
            {
                return HttpNotFound();
            }
            return View(produccion);
        }

        // POST: Produccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var produccion = _context.Produccion.Find(id);
                if (produccion == null)
                {
                    return HttpNotFound();
                }

                _context.Produccion.Remove(produccion);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "La producción ha sido eliminada correctamente.";
                return RedirectToAction("Produccion");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al intentar eliminar la producción.";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
