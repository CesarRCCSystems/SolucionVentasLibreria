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

    public class CD_Dashboard : Conexion
    {
        private static CD_Dashboard _instance;

        public CD_Dashboard()
        {

        }

        public static CD_Dashboard Instance => _instance ?? ( _instance = new CD_Dashboard());

     public Response<Dashboard> GetDasboard_amounts()
     {
       Response<Dashboard> resp = new Response<Dashboard>();

            try
            {
                using (SqlConnection conn = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("DashboardAmount_Tops", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            resp.respuesta = new Dashboard()
                            {
                                TotalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                TotalVenta = Convert.ToInt32(dr["TotalVenta"]),
                                TotalProducto= Convert.ToInt32(dr["TotalProducto"])
                            };
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                resp.mensaje = etag + ex.Message;

            }
                                        
       return resp;
     }





    }
}
