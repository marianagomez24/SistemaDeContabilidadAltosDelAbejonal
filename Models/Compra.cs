using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    public class Compra
    {
        [Key]
        public int IdCompra { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(50)]

        public string Proveedor { get; set; }

        [Required]
        [StringLength(50)]

        public string Producto  { get; set; }

        public int Cantidad     { get; set; }

        public decimal PrecioTotal { get; set; }

    }
}