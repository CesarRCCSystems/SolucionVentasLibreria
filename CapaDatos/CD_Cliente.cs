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
    public class CD_Cliente : Conexion
    {
        private static CD_Cliente _instance = null;

        public CD_Cliente()
        {

        }

        public static CD_Cliente Instance => _instance ?? (_instance = new CD_Cliente());

        public List<Cliente> listar()
        {
            List<Cliente> lista;

            try
            {
                using(SqlConnection conn = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("Cliente_List", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();    
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        lista = new List<Cliente>();
                        while (dr.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Activo =  Convert.ToBoolean(dr["Activo"]),

                            });
                        }
                        conn.Close();
                    }
                }
            }
            catch 
            {
                lista = new List<Cliente>();
                throw;
            }

            return lista;

        }

        public int RegistrarCliente(Cliente oCliente, out string Mensaje)
        {
            Mensaje = string.Empty;
            int respuesta;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Cliente_Registrar", oConexion);
                    cmd.Parameters.AddWithValue("TipoDocumento", oCliente.TipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento", oCliente.NumeroDocumento);
                    cmd.Parameters.AddWithValue("Nombre", oCliente.Nombre);
                    cmd.Parameters.AddWithValue("Direccion", oCliente.Direccion);
                    cmd.Parameters.AddWithValue("Telefono", oCliente.Telefono);
                    cmd.Parameters.AddWithValue("Activo", oCliente.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
                catch (Exception ex)
                {
                    Mensaje = etag + ex.Message;
                    respuesta = 0;
                }

            }

            return respuesta;

        }

        public bool ModificarCliente(Cliente oCliente, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Cliente_Editar", oConexion);
                    cmd.Parameters.AddWithValue("IdCliente", oCliente.IdCliente);
                    cmd.Parameters.AddWithValue("TipoDocumento", oCliente.TipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento", oCliente.NumeroDocumento);
                    cmd.Parameters.AddWithValue("Nombre", oCliente.Nombre);
                    cmd.Parameters.AddWithValue("Direccion", oCliente.Direccion);
                    cmd.Parameters.AddWithValue("Telefono", oCliente.Telefono);
                    cmd.Parameters.AddWithValue("Activo", oCliente.Activo);
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
                    Mensaje = etag + ex.Message;
                    respuesta = false;
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
                    SqlCommand cmd = new SqlCommand("Cliente_Eliminar", conn);
                    cmd.Parameters.AddWithValue("IdCliente", id);
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
