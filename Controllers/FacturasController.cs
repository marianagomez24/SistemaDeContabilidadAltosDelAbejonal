using SistemaContabilidadAltosDelAbejonal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    public class FacturasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacturasController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Facturas()
        {
            var facturas = _context.Facturas;
            return View(facturas);
        }
    }
}