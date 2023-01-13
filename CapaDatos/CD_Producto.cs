using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using CapaModelo;


namespace CapaDatos
{
    public class CD_Producto : Conexion
    {
        private static CD_Producto _instance = null;

        public CD_Producto()
        {

        }
        public static CD_Producto Instance => _instance ?? (_instance = new CD_Producto());

        public List<Producto> Listar()
        {

            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection conn = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("Producto_List", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {                         
                            lista.Add(new Producto() { 
                               IdProducto = Convert.ToInt32(dr["IdProducto"]),
                               Codigo = dr["Codigo"].ToString(),
                               ValorCodigo = dr["ValorCodigo"].ToString(),
                               Nombre = dr["Nombre"].ToString(),
                               Descripcion = dr["Descripcion"].ToString(),
                               oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]) , Descripcion= dr["DescCategoria"].ToString() },
                               oMarca = new Marca() { IdMarca= Convert.ToInt32(dr["IdMarca"]), Descripcion = dr["DescMarca"].ToString() },
                               FlagEliminado = Convert.ToBoolean(dr["FlagEliminado"]),
                               Activo = Convert.ToBoolean(dr["Activo"]),
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {

                lista = new List<Producto>(); 
            }

            return lista;

        }

        public int RegistrarProducto(Producto oProducto, out string Mensaje)
        {
            int IdGenerado;
            Mensaje = string.Empty;

            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Producto_Registrar", oConexion);
                    cmd.Parameters.AddWithValue("Nombre", oProducto.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", oProducto.Descripcion);
                    cmd.Parameters.AddWithValue("IdCategoria", oProducto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("IdMarca", oProducto.oMarca.IdMarca);
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    IdGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
                catch (Exception ex)
                {
                    IdGenerado = 0;
                    Mensaje = etag + ex.Message;
                }
            }
            return IdGenerado;
        }

        public bool ModificarProducto(Producto oProducto, out string Mensaje)
        {
            bool respuesta = true;
            Mensaje = string.Empty;

            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Producto_Editar", oConexion);
                    cmd.Parameters.AddWithValue("IdProducto", oProducto.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", oProducto.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", oProducto.Descripcion);
                    cmd.Parameters.AddWithValue("IdCategoria", oProducto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("IdMarca", oProducto.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("Activo", oProducto.Activo);
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    Mensaje = etag + ex.Message;
                }

            }

            return respuesta;

        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("Producto_Eliminar", conn);
                    cmd.Parameters.AddWithValue("IdProducto", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = etag + ex.Message;
            }

            return resultado;
        }



    }
}
