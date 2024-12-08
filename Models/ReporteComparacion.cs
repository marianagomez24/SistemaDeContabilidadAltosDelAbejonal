using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    public class ReporteComparacion
    {
        public string Mes { get; set; }
        public int CantidadCompras { get; set; }
        public decimal MontoTotalCompras { get; set; }
        public int CantidadVentas { get; set; }
        public decimal MontoTotalVentas { get; set; }
    }
}