using SistemaContabilidadAltosDelAbejonal.Models;
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
            //if (!context.Usuarios.Any())
            //{
            //    var usuarios = new List<Usuario>
            //    {
            //        new Usuario
            //        {
            //            Nombre = "Admin",
            //            PrimerApellido = "Altos del Abejonal",
            //            SegundoApellido = ".",
            //            Correo = "admin@gmail.com",
            //            Contraseña = BCrypt.Net.BCrypt.HashPassword("123"),
            //            Telefono = "1234567890",
            //            Direccion = "Oficina Principal",
            //            IDRol = 1
            //        },
            //        new Usuario
            //        {
            //            Nombre = "Pedro",
            //            PrimerApellido = "Rodríguez",
            //            SegundoApellido = "Salazar",
            //            Correo = "pedro@gmail.com",
            //            Contraseña = BCrypt.Net.BCrypt.HashPassword("123"),
            //            Telefono = "884349927",
            //            Direccion = "Heredia",
            //            IDRol = 2
            //        },

            //        new Usuario
            //        {
            //            Nombre = "María",
            //            PrimerApellido = "Fernández",
            //            SegundoApellido = "Lopez",
            //            Correo = "maria@gmail.com",
            //            Contraseña = BCrypt.Net.BCrypt.HashPassword("123"),
            //            Telefono = "872345678",
            //            Direccion = "San José",
            //            IDRol = 3
            //        },

            //        new Usuario
            //        {
            //            Nombre = "Carlos",
            //            PrimerApellido = "Gómez",
            //            SegundoApellido = "Ruiz",
            //            Correo = "carlos@gmail.com",
            //            Contraseña = BCrypt.Net.BCrypt.HashPassword("123"),
            //            Telefono = "899988776",
            //            Direccion = "Alajuela",
            //            IDRol = 2
            //        },

            //        new Usuario
            //        {
            //            Nombre = "Ana",
            //            PrimerApellido = "Ramírez",
            //            SegundoApellido = "Torres",
            //            Correo = "ana@gmail.com",
            //            Contraseña = BCrypt.Net.BCrypt.HashPassword("123"),
            //            Telefono = "865432198",
            //            Direccion = "Cartago",
            //            IDRol = 3
            //        },

            //        new Usuario
            //        {
            //            Nombre = "Luis",
            //            PrimerApellido = "Vargas",
            //            SegundoApellido = "Morales",
            //            Correo = "luis@gmail.com",
            //            Contraseña = BCrypt.Net.BCrypt.HashPassword("123"),
            //            Telefono = "876543210",
            //            Direccion = "Puntarenas",
            //            IDRol = 2
            //        }
            //    };

            //    context.Usuarios.AddRange(usuarios);
            //    context.SaveChanges();

            //}

            //if (!context.Ventas.Any())
            //{
            //    var ventas = new List<Venta>
            //    {
            //        new Venta
            //        {
            //        IDCliente = 1,
            //        IDUsuario = 3,
            //        FechaVenta = DateTime.Now,
            //        TotalVenta = 2000,
            //        Observaciones = "Falta confirmación"
            //        },

            //        new Venta
            //        {
            //        IDCliente = 2,
            //        IDUsuario = 2,
            //        FechaVenta = DateTime.Now,
            //        TotalVenta = 5090,
            //        Observaciones = "Productos nuevos"
            //        }
            //    };

            //    context.Ventas.AddRange(ventas);
            //    context.SaveChanges();

            //}
        }
    }
}

