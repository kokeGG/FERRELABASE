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
        public string Documento { get; set; }
        public string RazonSocial { get; set; }
        public string RFC {  get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }
        public string CP {  get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public string Numero { get; set; }
        public string Ciudad {  get; set; }
        public string Edo {  get; set; }
    }
}
