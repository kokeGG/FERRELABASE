using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Cliente
    {
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    oconexion.Open();
                    if (oconexion.State == ConnectionState.Open)
                    {
                        Console.WriteLine("Conexion iniciada correctamente");
                    }
                    else
                    {
                        Console.WriteLine("Conexion no se abrió correctamente");
                        return lista; // Salir del método si la conexión no se abrió correctamente
                    }

                    //string query = "SELECT IdCliente, Documento, NombreCompleto, Correo, Clave, Estado FROM Cliente";
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT IdCliente, Codigo, NombreCompleto, RFC, Regimen, Calle, NoExt, NoInt, Colonia, Estado, CodigoPostal, Municipio, Poblacion, Edo, Correo FROM CLIENTE");
                    
                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oconexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new Cliente()
                                {
                                    IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                    Codigo = dr["Codigo"].ToString(),
                                    NombreCompleto = dr["NombreCompleto"].ToString(),
                                    RFC = dr["RFC"].ToString(),
                                    Regimen = dr["Regimen"].ToString(),
                                    Calle = dr["Calle"].ToString(),
                                    NoExt = dr["NoExt"].ToString(),
                                    NoInt = dr["NoInt"].ToString(),
                                    Colonia = dr["Colonia"].ToString(),
                                    Estado = Convert.ToBoolean(dr["Estado"]),
                                    CodigoPostal = dr["CodigoPostal"].ToString(),
                                    Municipio = dr["Municipio"].ToString(),
                                    Poblacion = dr["Poblacion"].ToString(),
                                    Edo = dr["Edo"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Excepción producida: {ex.GetType().Name} - {ex.Message}");
                }

                return lista;
            }

        }
        public int Registrar(Cliente obj, out string Mensaje)
        {
            int idClientegenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", oconexion);
                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Calle", obj.Calle);
                    cmd.Parameters.AddWithValue("RFC", obj.RFC);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("NoExt", obj.NoExt);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("NoInt", obj.NoInt);
                    cmd.Parameters.AddWithValue("Colonia", obj.Colonia);
                    cmd.Parameters.AddWithValue("CodigoPostal", obj.CodigoPostal);
                    cmd.Parameters.AddWithValue("Municipio", obj.Municipio);
                    cmd.Parameters.AddWithValue("Poblacion", obj.Poblacion);
                    cmd.Parameters.AddWithValue("Edo", obj.Edo);
                    cmd.Parameters.AddWithValue("Regimen", obj.Regimen);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idClientegenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                idClientegenerado = 0;
                Mensaje = ex.Message;
            }

            return idClientegenerado;
        }

        public bool Editar(Cliente obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarCliente", oconexion);
                    cmd.Parameters.AddWithValue("IdCliente", obj.IdCliente);
                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("RFC", obj.RFC);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Calle", obj.Calle);
                    cmd.Parameters.AddWithValue("NoExt", obj.NoExt);
                    cmd.Parameters.AddWithValue("NoInt", obj.NoInt);
                    cmd.Parameters.AddWithValue("Colonia", obj.Colonia);
                    cmd.Parameters.AddWithValue("CodigoPostal", obj.CodigoPostal);
                    cmd.Parameters.AddWithValue("Municipio", obj.Municipio);
                    cmd.Parameters.AddWithValue("Poblacion", obj.Poblacion);
                    cmd.Parameters.AddWithValue("Edo", obj.Edo);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Regimen", obj.Regimen);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }

        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM CLIENTE WHERE IdCliente = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", obj.IdCliente);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }
    }
}
