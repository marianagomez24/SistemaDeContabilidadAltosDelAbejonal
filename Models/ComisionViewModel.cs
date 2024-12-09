using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    public class ComisionViewModel
    {
        public int IDComision { get; set; }
        public string UsuarioNombre { get; set; }
        public string VentaDescripcion { get; set; }
        public decimal PorcentajeComision { get; set; }
        public decimal MontoComision { get; set; }
        public DateTime FechaRegistro { get; set; }


    }
}