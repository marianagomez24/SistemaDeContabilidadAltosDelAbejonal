using SistemaContabilidadAltosDelAbejonal.Controllers;
using System.Data.Entity;
using System.Web.Services.Description;

namespace SistemaContabilidadAltosDelAbejonal.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }
        public DbSet<TipoGrano> TipoGranos { get; set; }
        public DbSet<TipoPresentacion> TipoPresentaciones { get; set; }
        public DbSet<CategoriaProducto> CategoriaProductos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<CompraDetalle> CompraDetalle { get; set; }
        public DbSet<PedidoProducto> PedidoProducto { get; set; }
        public DbSet<PedidoProductoDetalle> PedidoProductoDetalle { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }
        public DbSet<CotizacionDetalle> CotizacionDetalles { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaDetalle> VentaDetalles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}