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
            var model = new LoginViewModel();
            var cookieCorreo = Request.Cookies["Correo"];
            var cookieContraseña = Request.Cookies["Contraseña"];

            if (cookieCorreo != null && cookieContraseña != null)
            {
                model.Correo = cookieCorreo.Value;
                model.Contraseña = cookieContraseña.Value;
                model.RecordarCredenciales = true;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IniciarSesion(LoginViewModel model)
        {
            try
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

                            if (model.RecordarCredenciales)
                            {
                                HttpCookie cookieCorreo = new HttpCookie("Correo", model.Correo);
                                HttpCookie cookieContrasena = new HttpCookie("Contrasena", model.Contraseña);

                                cookieCorreo.Expires = DateTime.Now.AddMonths(1);
                                cookieContrasena.Expires = DateTime.Now.AddMonths(1);

                                Response.Cookies.Add(cookieCorreo);
                                Response.Cookies.Add(cookieContrasena);
                            }

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
            }
            catch (Exception ex)
            {
   
                return RedirectToAction("Error", "Home");
            }

            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Clear();

            if (Request.Cookies["Correo"] != null)
            {
                var cookieCorreo = new HttpCookie("Correo")
                {
                    Expires = DateTime.Now.AddDays(-1)

                };

                Response.Cookies.Add(cookieCorreo);
            }

            if (Request.Cookies["Contraseña"] != null)
            {
                var cookieContraseña = new HttpCookie("Contraseña")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };

                Response.Cookies.Add(cookieContraseña);
            }
            return RedirectToAction("IniciarSesion");
        }
    }
}