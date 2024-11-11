using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("Cotizacion")]
    public class Cotizacion
    {
        [Key]
        public int IDCotizacion { get; set; }

        [ForeignKey("Cliente")]
        public int? IDCliente { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime FechaCotizacion { get; set; }
        public string Estado { get; set; }
        public decimal TotalSinIVA { get; set; }
        public decimal IVA { get; set; }
        public decimal TotalConIVA { get; set; }
        public string Observaciones { get; set; }
    }
}