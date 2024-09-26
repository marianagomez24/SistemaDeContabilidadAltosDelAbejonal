using SistemaContabilidadAltosDelAbejonal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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