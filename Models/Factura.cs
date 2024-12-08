using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("Facturas")]
    public class Factura
    {
        [Key]

        public int IdFacturas { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoFactura { get; set; }
        public DateTime FechaIngreso { get; set; }

        public DateTime? FechaEmision { get; set; }

        public DateTime? FechaPago { get; set; }

        public int Total { get; set; }

        public bool Activo { get; set; }


    }
}