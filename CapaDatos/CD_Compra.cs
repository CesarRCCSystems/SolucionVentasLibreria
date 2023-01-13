using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;

using CapaModelo;

namespace CapaDatos
{
    public class CD_Compra : Conexion
    {
        private static CD_Compra _instance = null;

        public CD_Compra()
        {

        }

        public static CD_Compra Instance => _instance ?? (_instance = new CD_Compra()); 

        public bool RegistrarCompra(string Detalle, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Compra_Registrar", oConexion);
                    cmd.Parameters.Add("Detalle", SqlDbType.Xml).Value = Detalle;
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
                    Mensaje = etag + ex.Message;
                    respuesta = false;
                }
            }
            return respuesta;
        }


        public Compra ObtenerDetalleCompra(int IdCompra)
        {
            Compra rptDetalleCompra = new Compra();
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("DetalleCompra_List", oConexion);
                cmd.Parameters.AddWithValue("@IdCompra", IdCompra);
                cmd.CommandType = CommandType.StoredProcedure;


                try
                {
                    oConexion.Open();
                    using (XmlReader dr = cmd.ExecuteXmlReader())
                    {
                        while (dr.Read())
                        {
                            XDocument doc = XDocument.Load(dr);
                            if (doc.Element("DETALLE_COMPRA") != null)
                            {
                                rptDetalleCompra = (from dato in doc.Elements("DETALLE_COMPRA")
                                                    select new Compra()
                                                    {
                                                        Codigo = dato.Element("Codigo").Value,
                                                        TotalCosto = Convert.ToDecimal(dato.Element("TotalCosto").Value, new CultureInfo("es-PE")),
                                                        FechaCompra = dato.Element("FechaCompra").Value
                                                    }).FirstOrDefault();
                                rptDetalleCompra.oProveedor = (from dato in doc.Element("DETALLE_COMPRA").Elements("DETALLE_PROVEEDOR")
                                                               select new Proveedor()
                                                               {
                                                                   RUC = dato.Element("RUC").Value,
                                                                   RazonSocial = dato.Element("RazonSocial").Value,
                                                               }).FirstOrDefault();
                                rptDetalleCompra.oTienda = (from dato in doc.Element("DETALLE_COMPRA").Elements("DETALLE_TIENDA")
                                                            select new Tienda()
                                                            {
                                                                RUC = dato.Element("RUC").Value,
                                                                Nombre = dato.Element("Nombre").Value,
                                                                Direccion = dato.Element("Direccion").Value
                                                            }).FirstOrDefault();
                                rptDetalleCompra.oListaDetalleCompra = (from producto in doc.Element("DETALLE_COMPRA").Element("DETALLE_PRODUCTO").Elements("PRODUCTO")
                                                                        select new DetalleCompra()
                                                                        {
                                                                            Cantidad = int.Parse(producto.Element("Cantidad").Value),
                                                                            oProducto = new Producto() { Nombre = producto.Element("NombreProducto").Value },
                                                                            PrecioUnitarioCompra = Convert.ToDecimal(producto.Element("PrecioUnitarioCompra").Value, new CultureInfo("es-PE")),
                                                                            TotalCosto = Convert.ToDecimal(producto.Element("TotalCosto").Value, new CultureInfo("es-PE"))
                                                                        }).ToList();
                            }
                            else
                            {
                                rptDetalleCompra = null;
                            }
                        }

                        dr.Close();

                    }

                    return rptDetalleCompra;
                }
                catch (Exception ex)
                {
                    rptDetalleCompra = null;
                    return rptDetalleCompra;
                }
            }
        }


        public List<Compra> ObtenerListaCompra(DateTime FechaInicio, DateTime FechaFin, int IdProveedor, int IdTienda)
        {
            List<Compra> rptListaCompra = new List<Compra>();
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("Compra_List", oConexion);
                cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                cmd.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                cmd.Parameters.AddWithValue("@IdTienda", IdTienda);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaCompra.Add(new Compra()
                        {
                            IdCompra = Convert.ToInt32(dr["IdCompra"].ToString()),
                            NumeroCompra = dr["NumeroCompra"].ToString(),
                            oProveedor = new Proveedor() { RazonSocial = dr["RazonSocial"].ToString() },
                            oTienda = new Tienda() { Nombre = dr["Nombre"].ToString() },
                            FechaCompra = dr["FechaCompra"].ToString(),
                            TotalCosto = Convert.ToDecimal(dr["TotalCosto"].ToString(), new CultureInfo("es-PE"))
                        });
                    }
                    dr.Close();

                    return rptListaCompra;

                }
                catch (Exception ex)
                {
                    rptListaCompra = null;
                    return rptListaCompra;
                }
            }
        }




    }
}
