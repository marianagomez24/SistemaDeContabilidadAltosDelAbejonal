using SistemaContabilidadAltosDelAbejonal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SistemaContabilidadAltosDelAbejonal.Datos
{
    public class DT_Reporte
    {
        public List<ReporteCompra> RetornarCompras()
        {
            List<ReporteCompra> objLista = new List<ReporteCompra>();

            using (SqlConnection oconexion = new SqlConnection("Data Source=FRANCISCOVICTUS\\SQLEXPRESS;Initial Catalog=AltosDelAbejonalDB; Integrated Security=True"))
            {
                string query = "SP_ComprasMensaules";

                SqlCommand cmd = new SqlCommand(query, oconexion);
                cmd.CommandType = CommandType.StoredProcedure;

                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objLista.Add(new ReporteCompra()
                        {
                            Mes = dr["Mes"].ToString(),
                            Cantidad = int.Parse(dr["Cantidad"].ToString()),
                        });
                    }
                }

                return objLista;

            }
        }

        public List<ReporteCompraProductos> RetornarProcutos()
        {
            List<ReporteCompraProductos> objLista = new List<ReporteCompraProductos>();

            using (SqlConnection oconexion = new SqlConnection("Data Source=FRANCISCOVICTUS\\SQLEXPRESS;Initial Catalog=AltosDelAbejonalDB; Integrated Security=True"))
            {
                string query = "SP_RetornarProductosCompra";

                SqlCommand cmd = new SqlCommand(query, oconexion);
                cmd.CommandType = CommandType.StoredProcedure;

                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objLista.Add(new ReporteCompraProductos()
                        {
                            Producto = dr["Producto"].ToString(),
                            Cantidad = int.Parse(dr["Cantidad"].ToString()),
                        });
                    }
                }

                return objLista;

            }
        }
    }
}