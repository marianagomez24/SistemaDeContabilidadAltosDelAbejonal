using SistemaContabilidadAltosDelAbejonal.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Net;


namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
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

            return View(productos);
        }

        public ActionResult Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);

            if (producto != null)
            {
                db.Productos.Remove(producto);
                db.SaveChanges();
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
    }
}