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
    public class CD_Permisos : Conexion
    {

        private static CD_Permisos _instance = null;

        public CD_Permisos()
        {

        }

        public static CD_Permisos Instance => _instance ?? (_instance = new CD_Permisos());

        public List<Permisos> ObtenerPermisos(int IdRol)
        {
            List<Permisos> rptListaPermisos = new List<Permisos>();
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("Permisos_Listar", oConexion);
                cmd.Parameters.AddWithValue("@IdRol", IdRol);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaPermisos.Add(new Permisos()
                        {
                            IdPermisos = Convert.ToInt32(dr["IdPermisos"].ToString()),
                            Menu = dr["Menu"].ToString(),
                            SubMenu = dr["SubMenu"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"].ToString())
                        });
                    }
                    dr.Close();

                    return rptListaPermisos;

                }
                catch (Exception ex)
                {
                    rptListaPermisos = null;
                    return rptListaPermisos;
                }
            }
        }

        public bool ActualizarPermisos(string Detalle)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Permisos_Editar", oConexion);
                    cmd.Parameters.Add("Detalle", SqlDbType.Xml).Value = Detalle;
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

    }
}
