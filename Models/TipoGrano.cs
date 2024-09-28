using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("TipoGrano")]
    public class TipoGrano
    {
        [Key]
        public int IDTipoGrano { get; set; }
        [Required]
        [StringLength(50)]
        public string NombreGrano { get; set; }  

        public List <Producto> Productos { get; set; }
    }
}