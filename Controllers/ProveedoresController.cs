using SistemaContabilidadAltosDelAbejonal.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProveedoresController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Proveedores()
        {
            var proveedoresActivos = _context.Proveedor.Where(c => c.Activo == true).ToList();
            return View(proveedoresActivos);
        }

        public ActionResult AgregarProveedor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarProveedor([Bind(Include = "IDProveedor,Nombre,Email,Telefono,Direccion,FechaIngreso,Estado")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                proveedor.FechaIngreso = DateTime.Now;
                _context.Proveedor.Add(proveedor);
                _context.SaveChanges();
                return RedirectToAction("Proveedores");
            }
            return View(proveedor);
        }

        public ActionResult EditarProveedor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Proveedor proveedor = _context.Proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProveedor([Bind(Include = "IDProveedor,Nombre,Email,Telefono,Direccion,FechaIngreso,Estado")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(proveedor).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Proveedores");
            }
            return View(proveedor);
        }

        public ActionResult EliminarProveedor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Proveedor proveedor = _context.Proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }

            return View(proveedor);
        }

        [HttpPost, ActionName("EliminarProveedor")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedor proveedor = _context.Proveedor.Find(id);
            if (proveedor != null)
            {
                proveedor.Activo = false;
                _context.Entry(proveedor).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return RedirectToAction("Proveedores");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult BuscarProveedores(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                var proveedores = _context.Proveedor.ToList();
                return View("Proveedores", proveedores);
            }

            var proveedoresFiltrados = _context.Proveedor
                                      .Where(p => p.Nombre.Contains(searchQuery) ||
                                                  p.Email.Contains(searchQuery) ||
                                                  p.Telefono.Contains(searchQuery) ||
                                                  p.Direccion.Contains(searchQuery))
                                      .ToList();
            return View("Proveedores", proveedoresFiltrados);
        }
    }
}
