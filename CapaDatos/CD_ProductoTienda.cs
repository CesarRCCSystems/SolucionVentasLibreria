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
    public class CD_ProductoTienda : Conexion
    {
        private static CD_ProductoTienda _instance = null;

        public  CD_ProductoTienda()
        {

        }

        public static CD_ProductoTienda Instance => _instance ?? (_instance = new CD_ProductoTienda());


        public List<ProductoTienda> ObtenerProductoTienda()
        {
            List<ProductoTienda> rptListaProductoTienda = new List<ProductoTienda>();
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("ProductoTienda_List", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaProductoTienda.Add(new ProductoTienda()
                        {
                            IdProductoTienda = Convert.ToInt32(dr["IdProductoTienda"].ToString()),
                            oProducto = new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"].ToString()),
                                Codigo = dr["CodigoProducto"].ToString(),
                                Nombre = dr["NombreProducto"].ToString(),
                                Descripcion = dr["DescripcionProducto"].ToString(),
                            },
                            oTienda = new Tienda()
                            {
                                IdTienda = Convert.ToInt32(dr["IdTienda"].ToString()),
                                RUC = dr["RUC"].ToString(),
                                Nombre = dr["NombreTienda"].ToString(),
                                Direccion = dr["DireccionTienda"].ToString(),
                            },
                            PrecioUnidadCompra = Convert.ToDecimal(dr["PrecioUnidadCompra"].ToString(), new CultureInfo("es-PE")),
                            PrecioUnidadVenta = Convert.ToDecimal(dr["PrecioUnidadVenta"].ToString(), new CultureInfo("es-PE")),
                            Stock = Convert.ToInt32(dr["Stock"].ToString()),
                            Iniciado = Convert.ToBoolean(dr["Iniciado"].ToString())
                        });
                    }
                    dr.Close();

                    return rptListaProductoTienda;

                }
                catch (Exception ex)
                {
                    rptListaProductoTienda = null;
                    return rptListaProductoTienda;
                }
            }
        }

        public bool RegistrarProductoTienda(ProductoTienda oProductoTienda, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProductoTienda_Registrar", oConexion);
                    cmd.Parameters.AddWithValue("IdProducto", oProductoTienda.oProducto.IdProducto);
                    cmd.Parameters.AddWithValue("IdTienda", oProductoTienda.oTienda.IdTienda);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
                catch (Exception ex)
                {
                    Mensaje= etag + ex.Message;
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ModificarProductoTienda(ProductoTienda oProductoTienda)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarProductoTienda", oConexion);
                    cmd.Parameters.AddWithValue("IdProductoTienda", oProductoTienda.IdProductoTienda);
                    cmd.Parameters.AddWithValue("IdProducto", oProductoTienda.oProducto.IdProducto);
                    cmd.Parameters.AddWithValue("IdTienda", oProductoTienda.oTienda.IdTienda);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

        public bool EliminarProductoTienda(int IdProductoTienda)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_EliminarProductoTienda", oConexion);
                    cmd.Parameters.AddWithValue("IdProductoTienda", IdProductoTienda);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

        public Response<bool> ControlarStock(int IdProducto, int IdTienda, int Cantidad, bool Restar)
        {
            Response<bool> resp = new Response<bool>();
            
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ProductoTienda_ControlarStock", oConexion);
                    cmd.Parameters.AddWithValue("IdProducto", IdProducto);
                    cmd.Parameters.AddWithValue("IdTienda", IdTienda);
                    cmd.Parameters.AddWithValue("Cantidad", Cantidad);
                    cmd.Parameters.AddWithValue("Restar", Restar);
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    resp.respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    resp.mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    resp.respuesta = false;
                    resp.mensaje = etag + ex.Message;
                }
            }
            return resp;
        }



    }
}
