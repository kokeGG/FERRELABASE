using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Usuario
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

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

                    string query = "SELECT IdUsuario, Documento, NombreCompleto, Correo, Clave, Estado FROM USUARIO";

                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new Usuario()
                                {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    Documento = dr["Documento"].ToString(),
                                    NombreCompleto = dr["NombreCompleto"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    Estado = Convert.ToBoolean(dr["Estado"])
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
    }
}
 