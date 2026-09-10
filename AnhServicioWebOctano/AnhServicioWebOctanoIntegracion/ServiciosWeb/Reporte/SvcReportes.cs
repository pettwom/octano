using AnhPersistenciaCore.Aplicacion.Listados;
using AnhPersistenciaCore.Aplicacion.Reportes;
using AnhPersistenciaCore.Core;
using AnhPersistenciaCore.Entidades;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AnhServicioWebOctano.ServiciosWeb.Reporte
{
    public class SvcReportes : IService
    {
        #region Variables de Entorno

        private CPersistenciaReportes gReporte;

        public class EResultado
        {
            public decimal decCodigo { get; set; }
            public string strMensaje { get; set; }
            public object oResultado { get; set; }
        }

        #endregion

        #region Constructor de la Clase
        public SvcReportes()
        {
            gReporte = new CPersistenciaReportes();
        }
        #endregion

        #region Metodos y Funciones

        /// <summary>
        /// Reporte de calidad principal.
        /// </summary>
        /// <param name="request">Objeto para el reporte de calidad principal.</param>
        /// <returns>Lista de objetos O_REPORTE_CALIDAD_PRINCIPAL.</returns>
        public List<E_REPORTE_CALIDAD> GET(ReportarRegistroCalidad request)
        {
            string strMensajeError = "";
            List<E_REPORTE_CALIDAD> vListPruebasCalidad = null;
            vListPruebasCalidad = gReporte.ReportarRegistroCalidad(request.strLlave, request.strCorrelativo.Replace("_","/"), request.decFechaInicial, request.decFechaFinal, ref strMensajeError);
            return vListPruebasCalidad;
        }
        
        #endregion

        #region Decalaracion de Servicios

        [Route("/ReportarRegistroCalidad/{strLlave}/{strCorrelativo}/{decFechaInicial}/{decFechaFinal}", "GET")]
        public class ReportarRegistroCalidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strCorrelativo { get; set; }
            public decimal decFechaInicial{ get; set; }
            public decimal decFechaFinal{ get; set; }
        }

        #endregion
    }
}