using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Codigo { get; set; }
        public string RFC {  get; set; }
        public string NombreCompleto { get; set; }
        public string Calle {  get; set; }
        public string NoExt {  get; set; }
        public string NoInt { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal {  get; set; }
        public string Municipio {  get; set; }
        public string Poblacion { get; set; }
        public bool Estado { get; set; }
        public string Correo { get; set; }
        public string Edo {  get; set; }
        public string FechaRegistro { get; set; }
        public string Regimen { get; set; }
    }
}
