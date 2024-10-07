using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("Proveedor")]
    public class Proveedor
    {
        [Key]
        public int IDProveedor { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]
        public string Telefono { get; set; }
        [Required]
        [StringLength(255)]
        public string Direccion { get; set; }
        public DateTime FechaIngreso { get; set; }
        [Required]
        [StringLength(50)]
        public string Estado { get; set; }
    }
}