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
    public class CD_Permiso
    {
        public List<Permiso> Listar(int idusuario)
        {
            List<Permiso> lista = new List<Permiso>();

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

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT p.IdRol, p.NombreMenu FROM PERMISO p ");
                    query.AppendLine("INNER JOIN ROL r ON r.IdRol = p.IdRol \r\n");
                    query.AppendLine("INNER JOIN USUARIO u ON u.IdRol = r.IdRol ");
                    query.AppendLine("WHERE u.IdUsuario = @idusuario");


     

                    using (SqlCommand cmd = new SqlCommand(query.ToString(), oconexion))
                    {
                        cmd.Parameters.AddWithValue("@idusuario", idusuario);
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new Permiso()
                                {
                                    oRol = new Rol() { IdRol = Convert.ToInt32(dr["IdRol"]) } ,
                                    NombreMenu = dr["NombreMenu"].ToString(),
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
