using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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
            [EmailAddress] 
            public string Email { get; set; } 

            [StringLength(20)] 
            public string Telefono { get; set; } 

            [StringLength(255)] 
            public string Direccion { get; set; } 

            [Column(TypeName = "datetime")] 
            public DateTime FechaIngreso { get; set; } = DateTime.Now;
            public bool Activo { get; set; } = true;
        }
    }
