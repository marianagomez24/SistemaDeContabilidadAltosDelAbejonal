using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int IDUsuario { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string PrimerApellido { get; set; }
        [Required]
        [StringLength(50)]
        public string SegundoApellido { get; set; }
        [Required]
        [StringLength(500)]
        public string Contraseña { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        [ForeignKey("Rol")]
        public int? IDRol { get; set; }
        public Rol Rol { get; set; }

    }
}