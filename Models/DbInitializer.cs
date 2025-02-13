using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Usuarios.Any())
            {
                return;
            }
            var admin = new Usuario
            {
                Nombre = "prueba",
                PrimerApellido = "prueba",
                SegundoApellido = "",
                Correo = "prueba@gmail.com",
                Contraseña = BCrypt.Net.BCrypt.HashPassword("123"),
                Telefono = "1234567890",
                Direccion = "Oficina Principal",
                IDRol = 1, 
                Activo = true
            };

            context.Usuarios.Add(admin);
            context.SaveChanges();
        }       
    }
}