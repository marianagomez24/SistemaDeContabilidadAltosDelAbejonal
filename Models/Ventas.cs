using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("Venta")]
    public class Ventas
    {
        [Key]
        public int IDVenta { get; set; }

        [ForeignKey("Cliente")]
        public int IDCliente { get; set; }

        [ForeignKey("Usuario")]
        public int IDUsuario { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal TotalVenta { get; set; }
        public string Observaciones { get; set; }

        
        public virtual Cliente Cliente { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
