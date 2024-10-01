using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("CategoriaProducto")]
    public class CategoriaProducto
    {
        [Key]
        public int IDCategoria { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreCategoria { get; set; }
        public List<Producto> Productos { get; set; }
    }
}