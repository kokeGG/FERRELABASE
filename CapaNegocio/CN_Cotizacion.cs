using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Cotizacion
    {
        private CD_Cotizacion objcd_cotizacion = new CD_Cotizacion();

        public int ObtenerCorrelativo()
        {
            return objcd_cotizacion.ObtenerCorrelativo();
        }

        public bool Registrar(Cotizacion obj, DataTable DetalleCotizacion, out string Mensaje)
        {
            return objcd_cotizacion.Registrar(obj, DetalleCotizacion, out Mensaje);
        }

        public Cotizacion ObtenerCotizacion(string numero)
        {
            Cotizacion oCotizacion = objcd_cotizacion.ObtenerCotizacion(numero);

            if (oCotizacion.IdCotizacion != 0)
            {
                List<Detalle_Cotizacion> oDetalleCotizacion = objcd_cotizacion.ObtenerDetalleCotizacion(oCotizacion.IdCotizacion);
                oCotizacion.oDetalleCotizacion = oDetalleCotizacion;
            }

            return oCotizacion;
        }
    }
}
