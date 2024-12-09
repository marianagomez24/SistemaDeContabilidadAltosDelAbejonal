using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("Comision")]
    public class Comision
    {
        [Key]
        public int IDComision { get; set; } 

        [ForeignKey("Venta")]
        public int IDVenta { get; set; }    
        public Venta Venta { get; set; }   

        [ForeignKey("Usuario")]
        public int IDUsuario { get; set; } 
        public Usuario Usuario { get; set; } 

        [Required]
        public decimal PorcentajeComision { get; set; } 

        [Required]
        public decimal MontoComision { get; set; } 

        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now; 
    }
}