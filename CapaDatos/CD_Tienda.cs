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
    public class CD_Tienda : Conexion
    {
        private static CD_Tienda _instance = null;

        public CD_Tienda()
        {

        }

        public static CD_Tienda Instance => _instance ?? (_instance = new CD_Tienda());

        public List<Tienda> listar()
        {
            List<Tienda> lista;

            try
            {
                using (SqlConnection conn = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("Tienda_List", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                  
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                       lista = new List<Tienda>();
                        while (dr.Read())
                        {
                            lista.Add(new Tienda()
                            {
                                IdTienda = Convert.ToInt32(dr["IdTienda"]),
                                Nombre = dr["Nombre"].ToString(),
                                RUC = dr["RUC"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"]),                               
                            });
                        }
                    }

                }
            }
            catch (Exception)
            {
                lista = new List<Tienda>();
            }

            return lista;
        }

        public int Guardar(Tienda obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            int IdGenerado;

            try
            {
                using(SqlConnection conn = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("Tienda_Registrar", conn);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("RUC", obj.RUC);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    IdGenerado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                Mensaje =  etag + ex.Message;
                IdGenerado = 0;
            }

            return IdGenerado;
        }

        public bool Editar(Tienda obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("Tienda_Editar", conn);
                    cmd.Parameters.AddWithValue("IdTienda", obj.IdTienda);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("RUC", obj.RUC);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                Mensaje =  etag + ex.Message;
            }

            return resultado;

        }


        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("Tienda_Eliminar", conn);
                    cmd.Parameters.AddWithValue("IdTienda", id);
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
                Mensaje =  etag + ex.Message;
            }

            return resultado;
        }


    }
}
