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
    public class CD_Rol : Conexion
    {

    private static CD_Rol _instance = null; 

    public CD_Rol(){

    } 
    public static CD_Rol Instance => _instance ?? (_instance = new CD_Rol());

    public List<Rol> listar()
    {
            List<Rol> lista = new List<Rol>();

            try
            {
                using(SqlConnection conn = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("Rol_List", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Rol()
                            {
                                IdRol = Convert.ToInt32(dr["IdRol"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            }); ;   
                        }
                        conn.Close();
                    }
                }
            }
            catch
            {
                lista = new List<Rol>();
            }

            return lista;
        } 

    public int Registrar(Rol obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            int IdGenerado;

            try
            {
                using (SqlConnection conn = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("Rol_Registrar", conn);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@Activo", obj.Activo);
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                    IdGenerado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);

                }
            }
            catch (Exception ex)
            {
               IdGenerado=0;
               Mensaje = etag + ex.Message;
            }

            return IdGenerado;

        }

    public bool Editar(Rol obj, out string Mensaje)
        {
            Mensaje=string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("Rol_Editar", conn);
                    cmd.Parameters.AddWithValue("@IdRol", obj.IdRol);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@Activo", obj.Activo);
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);

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
                    SqlCommand cmd = new SqlCommand("Rol_Eliminar", conn);
                    cmd.Parameters.AddWithValue("IdRol", id);
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
