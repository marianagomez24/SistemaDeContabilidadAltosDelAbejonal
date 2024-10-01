using SistemaContabilidadAltosDelAbejonal.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

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