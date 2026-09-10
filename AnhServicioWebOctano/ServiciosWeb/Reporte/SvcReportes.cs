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

        #region Certificados de calidad

        /// <summary>
        /// Reporte de calidad.
        /// </summary>
        /// <param name="request">Objeto para el reporte de calidad.</param>
        /// <returns>Lista de objetos O_REPORTE_CALIDAD.</returns>
        public List<O_REPORTE_CALIDAD> GET(ReportarCalidad request)
        {
            string strMensajeError = "";
            string decIdRegistroCalidadPrincipal = request.decIdRegistroCalidadPrincipal.Replace("_","/");
            List<O_REPORTE_CALIDAD> vListPruebasCalidad = null;
            vListPruebasCalidad = gReporte.ReportarCalidad(request.strLlave, decIdRegistroCalidadPrincipal, ref strMensajeError);
            return vListPruebasCalidad;
        }

        /// <summary>
        /// Reporte de calidad principal.
        /// </summary>
        /// <param name="request">Objeto para el reporte de calidad principal.</param>
        /// <returns>Lista de objetos O_REPORTE_CALIDAD_PRINCIPAL.</returns>
        public List<O_REPORTE_CALIDAD_PRINCIPAL> GET(ReportarCalidadPrincipal request)
        {
            string strMensajeError = "";
            List<O_REPORTE_CALIDAD_PRINCIPAL> vListPruebasCalidad = null;
            string strFechaInicial = request.strFechaInicial.Replace("-", "/");
            string strFechaFinal = request.strFechaFinal.Replace("-", "/");
            vListPruebasCalidad = gReporte.ReportarCalidadPrincipal(request.strLlave, request.decIdEntidad, request.decIdUsuarioANH, strFechaInicial,strFechaFinal, request.decEstado, ref strMensajeError);
            return vListPruebasCalidad;
        }
        /*****REPORTE EXCEL REFINERIA*/
        public List<O_REPORTE_CALIDAD_REG_ADM> POST(ReportarCalidadPrincipalExcel request)
        { 
            string strMensajeError = "";
            List<O_REPORTE_CALIDAD_REG_ADM> vListPruebasCalidad = null;
            string strFechaInicial = request.strFechaInicial.Replace("-", "/");
            string strFechaFinal = request.strFechaFinal.Replace("-", "/");
            vListPruebasCalidad = gReporte.ReportarCalidadPrincipalExcel(request.strLlave, request.decIdEntidad, request.decIdUsuarioANH, strFechaInicial, strFechaFinal, request.decEstado, ref strMensajeError);
            return vListPruebasCalidad;
        }

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
        
        
        /// <summary>
        /// Reporte de alertas del calidad principal.
        /// </summary>
        /// <param name="request">Objeto para el reporte de calidad principal.</param>
        /// <returns>Lista de objetos O_REPORTE_ALERTA_CERT_CTY.</returns>
        public List<O_REPORTE_ALERTA_CERT_CTY> GET(ReportarAlertaCertificado request)
        {
            string strMensajeError = "";
            List<O_REPORTE_ALERTA_CERT_CTY> vListAlertaCertificado = null;
            vListAlertaCertificado = gReporte.ReportarAlertaCertificado(request.strLlave, request.decIdEntidad, request.decIdUsuarioAnh, request.decFechaInicial, request.decFechaFinal, ref strMensajeError);
            return vListAlertaCertificado;
        }

        /// <summary>
        /// Reporte de alertas del calidad principal.
        /// </summary>
        /// <param name="request">Objeto para el reporte de calidad principal.</param>
        /// <returns>Lista de objetos O_REPORTE_CALIDAD_PIVOTE_CTY.</returns>
        public List<O_REPORTE_CALIDAD_PIVOTE_CTY> GET(ReportarCertificadosPivote request)
        {
            string strMensajeError = "";
            List<O_REPORTE_CALIDAD_PIVOTE_CTY> vListAlertaCertificado = null;
            vListAlertaCertificado = gReporte.ReportarCertificadosPivote(request.strLlave, request.decIdEntidad, request.decIdTipoActividad, request.strCite, request.decFechaInicio, request.decFechaFin, request.decIdUsuario, ref strMensajeError);
            return vListAlertaCertificado;
        }

        public EResultado POST(GestionModificarDatos request)
        {
            string strMensajeError = "";
            //EResultado sResultado = new EResultado();
            O_RESULTADO_INF_CTY EResultadoRes = gReporte.GestionModificaDatos(request.strConsulta, ref strMensajeError);
            EResultado resultado = new EResultado();
            if (string.IsNullOrEmpty(strMensajeError))
            {
                resultado.decCodigo = 1;
                resultado.strMensaje = "OK";
                resultado.oResultado = EResultadoRes;
            }
            else
            {
                resultado.decCodigo = -1;
                resultado.strMensaje = strMensajeError;
            }
            return resultado;
        }
        

        #endregion

        #region Movimiento de volúmenes

        /// <summary>
        /// Reporte del gas de alimento regitrado para una entidad (planta).
        /// </summary>
        /// <param name="request">Objeto para la búsqueda del reporte.</param>
        /// <returns>Lista de objetos O_REPORTE_VOL_DTEP.</returns>
        public List<O_REPORTE_VOL_DTEP> GET(ReportarDtepGasAlimento request)
        {
            string strMensajeError = "";
            List<O_REPORTE_VOL_DTEP> vListReporteVolumen = null;
            vListReporteVolumen = gReporte.ReportarDtepGasAlimento(request.strLlave, request.decIdTipoReporte, request.decIdEntidad, request.decIdUnidadMedidaDestino, request.decFechaIni, request.decFechaFin, ref strMensajeError);
            return vListReporteVolumen;
        }

        /// <summary>
        /// Reporte de la producción regitrada para una entidad (planta).
        /// </summary>
        /// <param name="request">Objeto para la búsqueda del reporte.</param>
        /// <returns>Lista de objetos O_REPORTE_VOL_DTEP.</returns>
        public List<O_REPORTE_VOL_DTEP> GET(ReportarDtepProduccion request)
        {
            string strMensajeError = "";
            List<O_REPORTE_VOL_DTEP> vListReporteVolumen = null;
            vListReporteVolumen = gReporte.ReportarDtepProduccion(request.strLlave, request.decIdTipoReporte, request.decIdEntidad, request.decIdUnidadMedidaDestino, request.decFechaIni, request.decFechaFin, ref strMensajeError);
            return vListReporteVolumen;
        }

        /// <summary>
        /// Reporte del gas residual regitrado para una entidad (planta).
        /// </summary>
        /// <param name="request">Objeto para la búsqueda del reporte.</param>
        /// <returns>Lista de objetos O_REPORTE_VOL_DTEP.</returns>
        public List<O_REPORTE_VOL_DTEP> GET(ReportarDtepGasResidual request)
        {
            string strMensajeError = "";
            List<O_REPORTE_VOL_DTEP> vListReporteVolumen = null;
            vListReporteVolumen = gReporte.ReportarDtepGasResidual(request.strLlave, request.decIdTipoReporte, request.decIdEntidad, request.decIdUnidadMedidaDestino, request.decFechaIni, request.decFechaFin, ref strMensajeError);
            return vListReporteVolumen;
        }
        
        /// <summary>
        /// Listar los tipos de reporte.
        /// </summary>
        /// <param name="request">Objeto para la búsqueda del reporte.</param>
        /// <returns>Lista de objetos O_REPORTE_VOL_DTEP.</returns>
        public List<O_TIPOS_REPORTE_CTY> GET(ListarTiposReporte request)
        {
            string strMensajeError = "";
            List<O_TIPOS_REPORTE_CTY> vListReporteVolumen = null;
            vListReporteVolumen = gReporte.ListarTiposReporte(request.strLlave, request.strDescripcion, ref strMensajeError);
            return vListReporteVolumen;
        }
        
        #endregion

        #endregion

        #region Decalaracion de Servicios

        #region Certificados de calidad

        [Route("/ReportarCalidad/{strLlave}/{decIdRegistroCalidadPrincipal}", "GET")]
        public class ReportarCalidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string decIdRegistroCalidadPrincipal { get; set; }
        }


        [Route("/ReportarCalidadPrincipal/{strLlave}/{decIdEntidad}/{decIdUsuarioANH}/{strFechaInicial}/{strFechaFinal}/{decEstado}", "GET")]
        public class ReportarCalidadPrincipal : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdEntidad{ get; set; }
            public decimal decIdUsuarioANH{ get; set; }
            public string strFechaInicial{ get; set; }
            public string strFechaFinal{ get; set; }
            public decimal decEstado { get; set; }

        }
        [Route("/ReportarCalidadPrincipalExcel", "POST")]
        public class ReportarCalidadPrincipalExcel : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdEntidad { get; set; } 
            public decimal decIdUsuarioANH { get; set; }
            public string strFechaInicial { get; set; }
            public string strFechaFinal { get; set; }
            public decimal decEstado { get; set; }

        }


        [Route("/ReportarRegistroCalidad/{strLlave}/{strCorrelativo}/{decFechaInicial}/{decFechaFinal}", "GET")]
        public class ReportarRegistroCalidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strCorrelativo { get; set; }
            public decimal decFechaInicial{ get; set; }
            public decimal decFechaFinal{ get; set; }
        }


        [Route("/ReportarAlertaCertificado/{strLlave}/{decIdEntidad}/{decIdUsuarioAnh}/{decFechaInicial}/{decFechaFinal}", "GET")]
        public class ReportarAlertaCertificado : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdUsuarioAnh { get; set; }
            public decimal decFechaInicial{ get; set; }
            public decimal decFechaFinal{ get; set; }

        }


        [Route("/ReportarCertificadosPivote/{strLlave}/{decIdEntidad}/{decIdTipoActividad}/{strCite}/{decFechaInicio}/{decFechaFin}/{decIdUsuario}", "GET")]
        public class ReportarCertificadosPivote : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdTipoActividad { get; set; }
            public string strCite { get; set; }
            public decimal decFechaInicio { get; set; }
            public decimal decFechaFin { get; set; }
            public decimal decIdUsuario { get; set; }
        }
        
        [Route("/GestionModificarDatos", "POST")]
        public class GestionModificarDatos : IReturn<EResultado>
        {
            public string strConsulta { get; set; }
        }
        #endregion

        #region Movimiento de volúmenes

        [Route("/ReportarDtepGasAlimento/{strLlave}/{decIdTipoReporte}/{decIdEntidad}/{decIdUnidadMedidaDestino}/{decFechaIni}/{decFechaFin}", "GET")]
        public class ReportarDtepGasAlimento : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdTipoReporte { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdUnidadMedidaDestino { get; set; }
            public decimal decFechaIni { get; set; }
            public decimal decFechaFin { get; set; }
        }

        [Route("/ReportarDtepProduccion/{strLlave}/{decIdTipoReporte}/{decIdEntidad}/{decIdUnidadMedidaDestino}/{decFechaIni}/{decFechaFin}", "GET")]
        public class ReportarDtepProduccion : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdTipoReporte { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdUnidadMedidaDestino { get; set; }
            public decimal decFechaIni { get; set; }
            public decimal decFechaFin { get; set; }
        }

        [Route("/ReportarDtepGasResidual/{strLlave}/{decIdTipoReporte}/{decIdEntidad}/{decIdUnidadMedidaDestino}/{decFechaIni}/{decFechaFin}", "GET")]
        public class ReportarDtepGasResidual : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdTipoReporte { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdUnidadMedidaDestino { get; set; }
            public decimal decFechaIni { get; set; }
            public decimal decFechaFin { get; set; }
        }

        [Route("/ListarTiposReporte/{strLlave}/{strDescripcion}", "GET")]
        public class ListarTiposReporte : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strDescripcion { get; set; }
        }

        #endregion

        #endregion
    }
}