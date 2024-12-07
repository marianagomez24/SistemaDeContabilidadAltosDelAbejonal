using System;
using System.Linq;
using System.Web.Mvc;
using SistemaContabilidadAltosDelAbejonal.Models;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    public class VentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentasController()
        {
            _context = new ApplicationDbContext(); 
        }

        public ActionResult Index()
        {
            var ventas = _context.Venta.ToList(); 
            return View(ventas);
        }
    }
}
