using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("Cliente")] // Especifica el nombre de la tabla en la base de datos
    public class Cliente
    {
        [Key] 
        public int IDCliente { get; set; } 
        [Required] 
        [StringLength(100)] 
        public string Nombre { get; set; } 
        [StringLength(100)]
        public string PrimerApellido { get; set; } 
        [StringLength(100)] 
        public string SegundoApellido { get; set; } 
        [StringLength(20)] 
        public string Cedula { get; set; } 
        [StringLength(20)] 
        public string Telefono { get; set; } 
        [StringLength(255)] 
        public string Direccion { get; set; } 
        public bool Activo { get; set; }
    }
}
