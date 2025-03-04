using SistemaContabilidadAltosDelAbejonal.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using System;


namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    [AuthorizeRole("Administrador", "Vendedor", "Contador")]
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ProductosController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Stock()
        {
            var productos = _context.Productos
                .Include(p => p.TipoGrano)
                .Include(p => p.TipoPresentacion)
                .Include(p => p.CategoriaProducto)
                .ToList();

            var totalStock = productos.Sum(p => p.Stock);
            ViewBag.TotalStock = totalStock;        
            return View(productos);
            
        }

        [AuthorizeRole("Administrador")]
        public ActionResult AgregarProducto()
        {
            ViewBag.IDTipoGrano = new SelectList(db.TipoGranos, "IDTipoGrano", "NombreGrano");
            ViewBag.IDTipoPresentacion = new SelectList(db.TipoPresentaciones, "IDTipoPresentacion", "NombrePresentacion");
            ViewBag.IDCategoria = new SelectList(db.CategoriaProductos, "IDCategoria", "NombreCategoria");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarProducto([Bind(Include = "IDProducto,Nombre,IDTipoGrano,IDTipoPresentacion,IDCategoria,Precio,Stock,FechaActualizacion")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    producto.FechaActualizacion = DateTime.Now;
                    db.Productos.Add(producto);
                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Producto agregado correctamente!";
                    return RedirectToAction("Stock");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Hubo un error al agregar el producto. Intenta nuevamente.";
                    return RedirectToAction("Error", "Home");
                }
            }

            ViewBag.IDTipoGrano = new SelectList(db.TipoGranos, "IDTipoGrano", "NombreGrano", producto.IDTipoGrano);
            ViewBag.IDTipoPresentacion = new SelectList(db.TipoPresentaciones, "IDTipoPresentacion", "NombrePresentacion", producto.IDTipoPresentacion);
            ViewBag.IDCategoria = new SelectList(db.CategoriaProductos, "IDCategoria", "NombreCategoria", producto.IDCategoria);
            return View(producto);
        }


        [AuthorizeRole("Administrador")]
        public ActionResult EditarProducto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }

            ViewBag.IDTipoGrano = new SelectList(db.TipoGranos, "IDTipoGrano", "NombreGrano", producto.IDTipoGrano);
            ViewBag.IDTipoPresentacion = new SelectList(db.TipoPresentaciones, "IDTipoPresentacion", "NombrePresentacion", producto.IDTipoPresentacion);
            ViewBag.IDCategoria = new SelectList(db.CategoriaProductos, "IDCategoria", "NombreCategoria", producto.IDCategoria);
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProducto([Bind(Include = "IDProducto,Nombre,IDTipoGrano,IDTipoPresentacion,IDCategoria,Precio,Stock,FechaActualizacion")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.FechaActualizacion = DateTime.Now;
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "La información del producto fue editada correctamente!";
                return RedirectToAction("Stock");
            }

            ViewBag.IDTipoGrano = new SelectList(db.TipoGranos, "IDTipoGrano", "NombreGrano", producto.IDTipoGrano);
            ViewBag.IDTipoPresentacion = new SelectList(db.TipoPresentaciones, "IDTipoPresentacion", "NombrePresentacion", producto.IDTipoPresentacion);
            ViewBag.IDCategoria = new SelectList(db.CategoriaProductos, "IDCategoria", "NombreCategoria", producto.IDCategoria);
            return View(producto);
        }

        public ActionResult EliminarProducto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }

            return View(producto);
        }

        [HttpPost, ActionName("EliminarProducto")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Producto producto = db.Productos.Find(id);
                if (producto == null)
                {
                    TempData["ErrorMessage"] = "El producto que intentas eliminar no existe.";
                    return RedirectToAction("Stock");
                }
                db.Productos.Remove(producto);
                db.SaveChanges();
                TempData["SuccessMessage"] = "El producto fue eliminado correctamente!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Hubo un error al eliminar el producto. Intenta nuevamente.";
            }

            return RedirectToAction("Stock");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult BuscarProductos(string searchQuery)
        {
            
            if (string.IsNullOrEmpty(searchQuery))
            {
                var productos = db.Productos
                                  .Include(p => p.TipoGrano)
                                  .Include(p => p.TipoPresentacion)
                                  .Include(p => p.CategoriaProducto)
                                  .ToList();
                return View("Stock", productos);  
            }

           
            var productosFiltrados = db.Productos
                           .Include(p => p.TipoGrano)
                           .Include(p => p.TipoPresentacion)
                           .Include(p => p.CategoriaProducto)
                           .Where(p => p.Nombre.Contains(searchQuery) ||
                                       (p.TipoGrano != null && p.TipoGrano.NombreGrano.Contains(searchQuery)) ||
                                       (p.TipoPresentacion != null && p.TipoPresentacion.NombrePresentacion.Contains(searchQuery)) ||
                                       (p.CategoriaProducto != null && p.CategoriaProducto.NombreCategoria.Contains(searchQuery)))
                           .ToList();
            
            return View("Stock", productosFiltrados);
        }


    }
}