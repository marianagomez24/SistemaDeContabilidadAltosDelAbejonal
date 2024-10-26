using SistemaContabilidadAltosDelAbejonal.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Net;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ClientesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Clientes()
        {
            var clientes = _context.Cliente.ToList();
            return View(clientes);
        }

        public ActionResult AgregarCliente()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarCliente([Bind(Include = "IDCliente,Nombre,PrimerApellido,SegundoApellido,Cedula,Telefono,Direccion")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Clientes");
            }
            return View(cliente);
        }

        public ActionResult EditarCliente(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCliente([Bind(Include = "IDCliente,Nombre,PrimerApellido,SegundoApellido,Cedula,Telefono,Direccion")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Clientes");
            }
            return View(cliente);
        }

        public ActionResult EliminarCliente(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            return View(cliente);
        }

        [HttpPost, ActionName("EliminarCliente")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            if (cliente != null)
            {
                db.Cliente.Remove(cliente);
                db.SaveChanges();
            }
            return RedirectToAction("Clientes");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult BuscarClientes(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                var clientes = db.Cliente.ToList();
                return View("Clientes", clientes);
            }

            var clientesFiltrados = db.Cliente
                                      .Where(c => c.Nombre.Contains(searchQuery) ||
                                                  c.PrimerApellido.Contains(searchQuery) ||
                                                  c.SegundoApellido.Contains(searchQuery) ||
                                                  c.Cedula.Contains(searchQuery))
                                      .ToList();
            return View("Clientes", clientesFiltrados);
        }
    }
}
