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
    public class CD_Proveedores : Conexion
    {
        private static CD_Proveedores _instance;

        public CD_Proveedores()
        {
           
        }
        public  static CD_Proveedores Instance => _instance ?? (_instance = new CD_Proveedores());
     
        public List<Proveedor> listar()
        {
            List<Proveedor> lista;

            try
            {
                using (SqlConnection conn = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("Proveedor_List", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                       lista  = new List<Proveedor>();
                        while (dr.Read())
                        {
                            lista.Add(new Proveedor() { 
                              IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                              RUC = dr["RUC"].ToString(),
                              RazonSocial = dr["RazonSocial"].ToString(),
                              Telefono = dr["Telefono"].ToString(),
                              Correo = dr["Correo"].ToString(),
                              Direccion = dr["Direccion"].ToString(),
                              Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }
                        conn.Close();
                    }

                }
            }
            catch (Exception)
            {

                lista = new List<Proveedor>();
            }

            return lista;
        }

        public int Registar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            int IdGenereado;

            try
            {
                using(SqlConnection conn = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("Proveedor_Registrar", conn);
                    cmd.Parameters.AddWithValue("@RUC", obj.RUC);
                    cmd.Parameters.AddWithValue("@RazonSocial", obj.RazonSocial);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("@Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("@Activo", obj.Activo);
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    IdGenereado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Resultado"].Value.ToString();


                }
            }
            catch (Exception ex)
            {
                IdGenereado = 0;
                Mensaje =  etag + ex.Message;
            }

            return IdGenereado;

        }

        public bool Editar(Proveedor obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("Proveedor_Editar", conn);
                    cmd.Parameters.AddWithValue("@IdProveedor", obj.IdProveedor);
                    cmd.Parameters.AddWithValue("@RUC", obj.RUC);
                    cmd.Parameters.AddWithValue("@RazonSocial", obj.RazonSocial);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("@Direccion", obj.Direccion);
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
                    SqlCommand cmd = new SqlCommand("Proveedor_Eliminar", conn);
                    cmd.Parameters.AddWithValue("IdProveedor", id);
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
