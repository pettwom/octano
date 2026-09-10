using AnhPersistenciaCore.Aplicacion.Gestion;
using AnhPersistenciaCore.Core;
using AnhPersistenciaCore.Entidades;
using Newtonsoft.Json;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhServicioWebOctano.ServiciosWeb.Gestion
{
    public class SvcRegistroDtep : IService
    {
        #region Variables de Entorno

        private CPersistenciaGestion gRegistro;

        public class EResultado
        {
            public decimal decCodigo { get; set; }
            public string strMensaje { get; set; }
            public object oResultado { get; set; }
        }

        #endregion

        #region Constructor de la Clase
        public SvcRegistroDtep()
        {
            gRegistro = new CPersistenciaGestion();
        }
        #endregion

        #region Métodos y Funciones

        /// <summary>
        /// Método que realiza la inserción de la prueba de calidad para carburantes (Servicio Externo).
        /// </summary>
        /// <param name="request">Objeto para el registro de la prueba de calidad.</param>
        /// <returns>Retorna un objeto complejo O_REF_REG_REPORTE_PLANO_CTY con el codigo de error.</returns>
        public List<O_REF_REG_REPORTE_PLANO_CTY> POST(RegistrarPruebaCalidad request)
        {
            string strMensajeError = "";
            //List<O_REF_REG_REPORTE_PLANO_CTY> oResultado = gRegistro.InsertarPruebaCalidad(request.Llave, request.Version, request.CodigoProyecto, request.DatosReporte, request.IdOperador, request.EstadoRegistro, request.ObjetoLote, ref strMensajeError);
            List<O_REF_REG_REPORTE_PLANO_CTY> oResultado = gRegistro.RegistroPruebaCalidadExt(request.Llave, request.Version, request.DatosReporte, request.IdOperador, request.EstadoRegistro, request.ObjetoLote, ref strMensajeError);
            if (!string.IsNullOrEmpty(strMensajeError))
            {
                oResultado[0].RESULTADO = -1;
                oResultado[0].MENSAJE_ERROR = strMensajeError;
            }
            return oResultado;
        }
       
        #endregion

        #region Declaracion de Servicios

        [Route("/RegistrarPruebaCalidad", "POST")]
        public class RegistrarPruebaCalidad : IReturn<O_REF_REG_REPORTE_PLANO_CTY>//IReturn<EResultado>
        {
            public string Llave { get; set; }
            public decimal Version { get; set; }
            public ObjetoLote ObjetoLote { get; set; }
            //public decimal CodigoProyecto { get; set; }

            #region Valores auxiliares del sercicio externo

            public string DatosReporte { get; set; }
            public decimal IdOperador { get; set; }
            public decimal EstadoRegistro { get; set; }

            #endregion
        }
       
        #endregion

        #region Método(s) privado(s)

        /// <summary>
        /// Método que devuelve la respuesta del error en el caso de que existiese.
        /// </summary>
        /// <param name="vResultado">Objeto de tipo resultado.</param>
        /// <param name="pMensaje">Mensaje del Error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        private List<O_RESULTADO_CTY> RespuetaError(List<O_RESULTADO_CTY> vResultado, string pMensaje)
        {
            O_RESULTADO_CTY vObjResultado = new O_RESULTADO_CTY();
            vObjResultado.RESULTADO = -1;
            vObjResultado.MENSAJE_ERROR = pMensaje;
            vResultado = new List<O_RESULTADO_CTY>();
            vResultado.Add(vObjResultado);
            return vResultado;
        }

        #endregion
    }
}