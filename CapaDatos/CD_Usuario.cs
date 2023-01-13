using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;

using CapaModelo;

namespace CapaDatos
{
    public class CD_Usuario : Conexion
    {
        private static CD_Usuario _instance = null;

        private CD_Usuario()
        {

        }

        public static CD_Usuario Instance => _instance ?? (_instance = new CD_Usuario());

        public Response<int> LoginUsuario(string Usuario, string Clave)
        {
            Response<int> resp = new Response<int>();
            
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Usuario_Login", oConexion);
                    cmd.Parameters.AddWithValue("Correo", Usuario);
                    cmd.Parameters.AddWithValue("Clave", Clave);
                    cmd.Parameters.Add("IdUsuario", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    resp.respuesta = Convert.ToInt32(cmd.Parameters["IdUsuario"].Value);
                    resp.mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
                catch (Exception ex)
                {
                    resp.mensaje = etag + ex.Message;
                    resp.respuesta = 0;
                }
            }
            return resp;
        }

        public Response<Usuario> ObtenerDetalleUsuario(int IdUsuario)
        {
            Response<Usuario> resp = new Response<Usuario>();
            Usuario usu =new Usuario();
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("Usuario_ObtenerDetalle", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                try
                {
                    oConexion.Open();
                    using (XmlReader dr = cmd.ExecuteXmlReader())
                    {
                        while (dr.Read())
                        {
                            XDocument doc = XDocument.Load(dr);
                            if (doc.Element("Usuario") != null)
                            {
                                     usu = (from dato in doc.Elements("Usuario")
                                              select new Usuario()
                                              {
                                                  IdUsuario = int.Parse(dato.Element("IdUsuario").Value),
                                                  Nombres = dato.Element("Nombres").Value,
                                                  Apellidos = dato.Element("Apellidos").Value,
                                                  Correo = dato.Element("Correo").Value,
                                                  Clave = dato.Element("Clave").Value,
                                                  RutaImagen = dato.Element("RutaImagen").Value,
                                                  NombreImagen = dato.Element("NombreImagen").Value,

                                              }).FirstOrDefault();
                                     usu.oTienda = (from dato in doc.Element("Usuario").Elements("DetalleTienda")
                                                      select new Tienda()
                                                      {
                                                          IdTienda = int.Parse(dato.Element("IdTienda").Value),
                                                          Nombre = dato.Element("Nombre").Value,
                                                          RUC = dato.Element("RUC").Value,
                                                          Direccion = dato.Element("Direccion").Value,
                                                          Telefono = dato.Element("Telefono").Value
                                                      }).FirstOrDefault();
                                     usu.oRol = (from dato in doc.Element("Usuario").Elements("DetalleRol")
                                                   select new Rol()
                                                   {
                                                       Descripcion = dato.Element("Descripcion").Value
                                                   }).FirstOrDefault();
                                     usu.oListaMenu = (from menu in doc.Element("Usuario").Element("DetalleMenu").Elements("Menu")
                                                         select new Menu()
                                                         {
                                                             Nombre = menu.Element("NombreMenu").Value,
                                                             Icono = menu.Element("Icono").Value,
                                                             oSubMenu = (from submenu in menu.Element("DetalleSubMenu").Elements("SubMenu")
                                                                         select new SubMenu()
                                                                         {
                                                                             Nombre = submenu.Element("NombreSubMenu").Value,
                                                                             Controller = submenu.Element("Controller").Value,
                                                                             Action = submenu.Element("Action").Value,
                                                                             Icono = submenu.Element("Icono").Value,
                                                                             Estado = (submenu.Element("Activo").Value.ToString() == "1" ? true : false),

                                                                         }).ToList()

                                                         }).ToList();

                                resp.respuesta = usu; 
                            }
                            else
                            {
                                resp.respuesta = null;
                            }
                        }

                        dr.Close();

                    }

                    return resp;
                }
                catch (Exception ex)
                {
                    resp.mensaje = etag + ex.Message;            
                    resp.respuesta = null;
                    return resp;
                }
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> rptListaUsuario = new List<Usuario>();
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand("Usuario_List", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaUsuario.Add(new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString()),
                            Nombres = dr["Nombres"].ToString(),
                            Apellidos = dr["Apellidos"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Clave = dr["Clave"].ToString(),
                            IdTienda = Convert.ToInt32(dr["IdTienda"].ToString()),
                            IdRol = Convert.ToInt32(dr["IdRol"].ToString()),
                            oRol = new Rol() { Descripcion = dr["DescripcionRol"].ToString() },
                            RutaImagen = dr["RutaImagen"].ToString(),
                            NombreImagen = dr["NombreImagen"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"])

                        });
                    }
                    dr.Close();

                    return rptListaUsuario;

                }
                catch (Exception ex)
                {
                    rptListaUsuario = null;
                    return rptListaUsuario;
                }
            }
        }

        public int RegistrarUsuario(Usuario oUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;
            int IdGenerado;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Usuario_Registrar", oConexion);
                    cmd.Parameters.AddWithValue("Nombres", oUsuario.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", oUsuario.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                    cmd.Parameters.AddWithValue("Clave", oUsuario.Clave);
                    cmd.Parameters.AddWithValue("IdTienda", oUsuario.IdTienda);
                    cmd.Parameters.AddWithValue("IdRol", oUsuario.IdRol);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    IdGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    Mensaje =  etag + ex.Message;
                    IdGenerado = 0;
                }

            }

            return IdGenerado;

        }

        public bool ModificarUsuario(Usuario oUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Usuario_Editar", oConexion);
                    cmd.Parameters.AddWithValue("IdUsuario", oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombres", oUsuario.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", oUsuario.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                    cmd.Parameters.AddWithValue("Clave", oUsuario.Clave);
                    cmd.Parameters.AddWithValue("IdTienda", oUsuario.IdTienda);
                    cmd.Parameters.AddWithValue("IdRol", oUsuario.IdRol);
                    cmd.Parameters.AddWithValue("Activo", oUsuario.Activo);
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
                    respuesta = false;
                    Mensaje =  etag + ex.Message;
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
                    SqlCommand cmd = new SqlCommand("Usuario_Eliminar", conn);
                    cmd.Parameters.AddWithValue("IdUsuario", id);
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


        public bool GurdarDatosImagenUsu(Usuario obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                string query = "UPDATE Usuario SET RutaImagen = @RutaImagen, NombreImagen = @NombreImagen WHERE IdUsuario = @IdUsuario";
                using (SqlConnection conn = new SqlConnection(cn))
                {
                    
                    SqlCommand cmd = new SqlCommand(query ,conn);
                    cmd.Parameters.AddWithValue("@RutaImagen", obj.RutaImagen);
                    cmd.Parameters.AddWithValue("@NombreImagen", obj.NombreImagen);
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();

                    if(cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = "No se pudo guardar la imagen";
                    }


                }


            }
            catch (Exception ex)
            {
                resultado=false;
                Mensaje = etag + ex.Message;
            }

            return resultado;
        }



    }
}
