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

        public List<ReporteCompraProveedor> RetornarProveedores()
        {
            List<ReporteCompraProveedor> objLista = new List<ReporteCompraProveedor>();

            using (SqlConnection oconexion = new SqlConnection("Data Source=FRANCISCOVICTUS\\SQLEXPRESS;Initial Catalog=AltosDelAbejonalDB; Integrated Security=True"))
            {
                string query = "SP_ComprasProveedor";

                SqlCommand cmd = new SqlCommand(query, oconexion);
                cmd.CommandType = CommandType.StoredProcedure;

                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objLista.Add(new ReporteCompraProveedor()
                        {
                            Proveedor = dr["Proveedor"].ToString(),
                            Compras = int.Parse(dr["Compras"].ToString()),
                        });
                    }
                }

                return objLista;

            }
        }

        public List<ReporteVenta> RetornarVentas()
        {
            List<ReporteVenta> objLista = new List<ReporteVenta>();

            using (SqlConnection oconexion = new SqlConnection("Data Source=FRANCISCOVICTUS\\SQLEXPRESS;Initial Catalog=AltosDelAbejonalDB; Integrated Security=True"))
            {
                string query = "SP_VentasMensuales"; 

                SqlCommand cmd = new SqlCommand(query, oconexion);
                cmd.CommandType = CommandType.StoredProcedure;

                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objLista.Add(new ReporteVenta()
                        {
                            Mes = dr["Mes"].ToString(),
                            Cantidad = int.Parse(dr["Cantidad"].ToString()),
                        });
                    }
                }

                return objLista;
            }
        }

        public List<ReporteComparacion> RetornarGastoIngreso()
        {
            List<ReporteComparacion> lista = new List<ReporteComparacion>();

            using (SqlConnection conexion = new SqlConnection("Data Source=FRANCISCOVICTUS\\SQLEXPRESS;Initial Catalog=AltosDelAbejonalDB; Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("SP_GastosIngresos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                conexion.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ReporteComparacion()
                        {
                            Mes = dr["Mes"].ToString(),
                            CantidadCompras = Convert.ToInt32(dr["CantidadCompras"]),
                            MontoTotalCompras = Convert.ToDecimal(dr["MontoTotalCompras"]),
                            CantidadVentas = Convert.ToInt32(dr["CantidadVentas"]),
                            MontoTotalVentas = Convert.ToDecimal(dr["MontoTotalVentas"])
                        });
                    }
                }
            }
            return lista;
        }

    }
}