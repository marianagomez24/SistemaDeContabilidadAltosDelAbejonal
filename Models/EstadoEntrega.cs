using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("EstadoEntrega")]
    public class EstadoEntrega
    {
        [Key]
        public int IDEstado { get; set; }

        public string Estado { get; set; }
    }
}