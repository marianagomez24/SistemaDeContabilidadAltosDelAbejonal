using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BCrypt.Net;
using SistemaContabilidadAltosDelAbejonal.Models;

namespace SistemaContabilidadAltosDelAbejonal.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ApplicationDbContext db = new ApplicationDbContext();

        public LoginController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IniciarSesion(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = db.Usuarios.SingleOrDefault(u => u.Correo == model.Correo);

                if (usuario != null)
                {
                    if (usuario.Contraseña == null)
                    {
                        ModelState.AddModelError("", "La contraseña no está almacenada.");
                        return View(model);
                    }

                    if (BCrypt.Net.BCrypt.Verify(model.Contraseña, usuario.Contraseña))
                    {
                        var rol = db.Rols.SingleOrDefault(r => r.IDRol == usuario.IDRol);
                        if (rol != null)
                        {
                            Session["RolUsuario"] = rol.NombreRol;
                        }
                        Session["UsuarioID"] = usuario.IDUsuario;
                        Session["NombreUsuario"] = usuario.Nombre;

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Contraseña incorrecta.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "No se encontró un usuario con ese correo.");
                }
            }

            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Clear(); 
            return RedirectToAction("IniciarSesion");
        }
    }
}