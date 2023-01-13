using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

using CapaModelo;

namespace CapaDatos
{
    public class CD_Reportes : Conexion
    {
        private static CD_Reportes _instance = null;
        
        public CD_Reportes()
        {

        }

        public static CD_Reportes Instance => _instance ?? (_instance = new CD_Reportes());

        public List<ReporteProducto> ReporteProductoTienda(int IdTienda, string CodigoProducto)
        {
            List<ReporteProducto> lista = new List<ReporteProducto>();

            NumberFormatInfo formato = new CultureInfo("es-PE").NumberFormat;
            formato.CurrencyGroupSeparator = ".";

            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("ReporteProductoTienda_List", oConexion);
                cmd.Parameters.AddWithValue("@IdTienda", IdTienda);
                cmd.Parameters.AddWithValue("@Codigo", CodigoProducto);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteProducto()
                            {
                                RucTienda = dr["Ruc Tienda"].ToString(),
                                NombreTienda = dr["Nombre Tienda"].ToString(),
                                DireccionTienda = dr["Direccion Tienda"].ToString(),
                                CodigoProducto = dr["Codigo Producto"].ToString(),
                                NombreProducto = dr["Nombre Producto"].ToString(),
                                DescripcionProducto = dr["Descripcion Producto"].ToString(),
                                StockenTienda = dr["Stock en tienda"].ToString(),
                                PrecioCompra = Convert.ToDecimal(dr["Precio Compra"].ToString(), new CultureInfo("es-PE")).ToString("N", formato),
                                PrecioVenta = Convert.ToDecimal(dr["Precio Venta"].ToString(), new CultureInfo("es-PE")).ToString("N", formato)
                            });
                        }

                    }

                }
                catch (Exception ex)
                {
                    lista = new List<ReporteProducto>();
                }
            }

            return lista;
        }

        public List<ReporteVenta> ReporteVenta(DateTime FechaInicio, DateTime FechaFin, int IdTienda)
        {
            List<ReporteVenta> lista = new List<ReporteVenta>();

            NumberFormatInfo formato = new CultureInfo("es-PE").NumberFormat;
            formato.CurrencyGroupSeparator = ".";

            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("ReporteVenta_List", oConexion);
                cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                cmd.Parameters.AddWithValue("@IdTienda", IdTienda);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteVenta()
                            {
                                FechaVenta = dr["Fecha Venta"].ToString(),
                                NumeroDocumento = dr["Numero Documento"].ToString(),
                                TipoDocumento = dr["Tipo Documento"].ToString(),
                                NombreTienda = dr["Nombre Tienda"].ToString(),
                                RucTienda = dr["Ruc Tienda"].ToString(),
                                NombreEmpleado = dr["Nombre Empleado"].ToString(),
                                CantidadUnidadesVendidas = dr["Cantidad Unidades Vendidas"].ToString(),
                                CantidadProductos = dr["Cantidad Productos"].ToString(),
                                TotalVenta = Convert.ToDecimal(dr["Total Venta"].ToString(), new CultureInfo("es-PE")).ToString("N", formato)
                            });
                        }

                    }

                }
                catch
                {
                    lista = new List<ReporteVenta>();
                }
            }

            return lista;

        }


    }
}
