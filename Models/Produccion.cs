using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    [Table("Produccion")]
    public class Produccion
	{
        [Key] 
        public int ID { get; set; }

        [Required]
        public int Dia { get; set; } 

        [Required]
        public DateTime Fecha { get; set; }  

        [Required]
        [Range(0, 999.99)]
        public decimal Altura { get; set; }  

        [Required]
        [StringLength(100)]
        public string MicroRegion { get; set; }  

        [Required]
        [StringLength(100)]
        public string Finca { get; set; }  

        [Required]
        [StringLength(100)]
        public string Producto { get; set; }  

        [Required]
        [StringLength(100)]
        public string Proceso { get; set; }  

        [Required]
        [StringLength(100)]
        public string Variedad { get; set; }  

        [Required]
        [Range(0, 999.99)]
        public decimal CJ { get; set; }  

        [Required]
        [StringLength(50)]
        public string Flote { get; set; }  

        [Required]
        [Range(0, 9999.99)]
        public decimal Neto { get; set; }  

        [Required]
        [Range(0, 9999.99)]
        public decimal FF { get; set; }  

        [StringLength(50)]
        public string LoteAltos { get; set; }  

        [StringLength(50)]
        public string LoteExclusive { get; set; }  

        [Required]
        [Range(0, 100)]
        public decimal Humedad { get; set; }  

        [StringLength(50)]
        public string Calificacion { get; set; }  
    }
}