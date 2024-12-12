using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("Compra")]
    public class Compra
    {
        [Key]
        public int IDCompra { get; set; }

        [ForeignKey("Proveedor")]
        public int? IDProveedor { get; set; }
        public Proveedor Proveedor { get; set; }

        public DateTime FechaCompra { get; set; }
        public decimal TotalCompra { get; set; }
        public string Observaciones { get; set; }
        public ICollection<CompraDetalle> CompraDetalle { get; set; }
    }
}