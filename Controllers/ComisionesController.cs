using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SistemaContabilidadAltosDelAbejonal.Datos;
using SistemaContabilidadAltosDelAbejonal.Models;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    public class ComisionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comisiones
        public ActionResult Comisiones()
        {
            try
            {

                var comisiones = db.Comisiones
                    .Include(c => c.Venta)
                    .Include(c => c.Usuario)
                    .ToList();


                var modelo = comisiones.Select(c => new ComisionViewModel
                {
                    IDComision = c.IDComision,
                    UsuarioNombre = $"{c.Usuario?.Nombre} {c.Usuario?.PrimerApellido} {c.Usuario?.SegundoApellido}",
                    VentaDescripcion = $"Venta ID: {c.Venta?.IDVenta}",
                    PorcentajeComision = c.PorcentajeComision,
                    MontoComision = c.MontoComision,
                    FechaRegistro = c.FechaRegistro
                }).ToList();


                return View(modelo);
            }
            catch (Exception ex)
            {
                // Opcional: Log o manejar la excepción adecuadamente
                ViewBag.Error = $"Error al cargar las comisiones: {ex.Message}";
                return View(new List<ComisionViewModel>()); // Devuelve una lista vacía en caso de error
            }
        }



        // GET: Comisiones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comision = db.Comisiones.Include(c => c.Venta).Include(c => c.Usuario)
                                        .FirstOrDefault(c => c.IDComision == id);

            if (comision == null)
            {
                // Puedes enviar un mensaje a la vista para mostrar un error
                TempData["Error"] = "No se encontró la comisión.";
                return RedirectToAction("Comisiones");
            }

            return View(comision);
        }

        // GET: Comisiones/Create
        public ActionResult Create()
        {
            ViewBag.IDVenta = new SelectList(db.Ventas.ToList(), "IDVenta", "TotalVenta");
            ViewBag.IDUsuario = new SelectList(db.Usuarios.ToList(), "IDUsuario", "Nombre");
            return View();
        }

        // POST: Comisiones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDVenta,IDUsuario,PorcentajeComision,MontoComision,FechaRegistro")] Comision comision)
        {
            if (ModelState.IsValid)
            {
                db.Comisiones.Add(comision);
                db.SaveChanges();
                return RedirectToAction("Comisiones");
            }

            ViewBag.IDVenta = new SelectList(db.Ventas.ToList(), "IDVenta", "TotalVenta", comision.IDVenta);
            ViewBag.IDUsuario = new SelectList(db.Usuarios.ToList(), "IDUsuario", "Nombre", comision.IDUsuario);
            return View(comision);
        }

        // GET: Comisiones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Busca la comisión en la base de datos
            Comision comision = db.Comisiones.Include(c => c.Venta)
                                              .Include(c => c.Usuario)
                                              .FirstOrDefault(c => c.IDComision == id);

            // Verifica si la comisión existe
            if (comision == null)
            {
                return HttpNotFound("No se encontró la comisión solicitada.");
            }

            // Prepara los valores para las listas desplegables
            ViewBag.IDVenta = new SelectList(db.Ventas.ToList(), "IDVenta", "TotalVenta", comision.IDVenta);
            ViewBag.IDUsuario = new SelectList(db.Usuarios.ToList(), "IDUsuario", "Nombre", comision.IDUsuario);

            return View(comision); // Pasa la comisión encontrada a la vista
        }
        // POST: Comisiones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDComision,IDVenta,IDUsuario,PorcentajeComision,MontoComision,FechaRegistro")] Comision comision)
        {
            if (ModelState.IsValid)
            {
                // Busca la comisión en la base de datos
                Comision comisionExistente = db.Comisiones.Find(comision.IDComision);

                if (comisionExistente == null)
                {
                    // Si la comisión no se encuentra, se lanza un error
                    return HttpNotFound("No se encontró la comisión para editar.");
                }

                // Asigna los valores del formulario a la comisión existente
                comisionExistente.IDVenta = comision.IDVenta;
                comisionExistente.IDUsuario = comision.IDUsuario;
                comisionExistente.PorcentajeComision = comision.PorcentajeComision;
                comisionExistente.MontoComision = comision.MontoComision;
                comisionExistente.FechaRegistro = comision.FechaRegistro;

                // Marca la entidad como modificada
                db.Entry(comisionExistente).State = EntityState.Modified;
                db.SaveChanges(); // Guarda los cambios en la base de datos

                // Redirige a la lista de comisiones
                return RedirectToAction("Comisiones");
            }

            // Si el modelo no es válido, vuelve a cargar las listas y muestra los errores
            ViewBag.IDVenta = new SelectList(db.Ventas.ToList(), "IDVenta", "TotalVenta", comision.IDVenta);
            ViewBag.IDUsuario = new SelectList(db.Usuarios.ToList(), "IDUsuario", "Nombre", comision.IDUsuario);
            return View(comision); // Regresa a la vista con los errores de validación
        }

        // GET: Comisiones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comision = db.Comisiones.Find(id);

            if (comision == null)
            {
                return HttpNotFound();
            }

            return View(comision);
        }

        // POST: Comisiones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var comision = db.Comisiones.Find(id);
            if (comision != null)
            {
                db.Comisiones.Remove(comision);
                db.SaveChanges();
            }
            return RedirectToAction("Comisiones");
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