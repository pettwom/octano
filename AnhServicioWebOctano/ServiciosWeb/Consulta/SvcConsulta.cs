using AnhPersistenciaCore.Aplicacion.Consulta;
using AnhPersistenciaCore.Core;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhServicioWebOctano.ServiciosWeb.Consulta
{
    public class SvcConsulta : IService
    {
        #region Variables de Entorno

        private CPersistenciaConsulta gConsulta;

        public class EResultado
        {
            public decimal decCodigo { get; set; }
            public string strMensaje { get; set; }
            public object oResultado { get; set; }
        }

        #endregion

        #region Constructor de la Clase
        public SvcConsulta()
        {
            gConsulta = new CPersistenciaConsulta();
        }
        #endregion

        #region Métodos y Funciones

        /// <summary>
        /// Lista los tipos de operación.
        /// </summary>
        /// <param name="request">Objeto para realizar el listado de los tipos de operación.</param>
        /// <returns>Lista de objetos O_TIPOS_ACTIVIDAD_CTY.</returns>
        public List<O_RESULTADO_CTY> GET(ObtenerTipoOperacion request)
        {
            string strMensajeError = "";
            List<O_RESULTADO_CTY> vListProductos = null;
            vListProductos = gConsulta.ObtenerTipoOperacion(request.strLlave, request.strTipoOperacionPadre, request.strTipoOperacionHijo, ref strMensajeError);
            return vListProductos;
        }

        /// <summary>
        /// Obtiene la entidad a la que el usuario esta vinculado para OCTANO CALIDAD
        /// </summary>
        /// <param name="request">Objeto para realizar el listado de los tipos de operación.</param>
        /// <returns>Retorna el identificador de la entidad en un objeto O_CONSULTA_ENTIDAD_CTY</returns>
        public O_CONSULTA_ENTIDAD_CTY GET(ObtenerEntidadOctano request)
        {
            string strMensajeError = "";
            O_CONSULTA_ENTIDAD_CTY vEntidad = null;
            vEntidad = gConsulta.ObtenerEntidadOctano(request.strLlave, request.decIdUsuario, ref strMensajeError);
            return vEntidad;
        }

        #endregion

        #region Decalaracion de Servicios
        
        #region Movimiento de volúmenes
        [Route("/ObtenerTipoOperacion/{strLlave}/{strDescripcion}/{decIdCategoriaActividad}/{decIdTipoActividad}", "GET")]
        public class ObtenerTipoOperacion : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strTipoOperacionPadre { get; set; }
            public string strTipoOperacionHijo { get; set; }
        }
        #endregion

        #region Consulta Entidad Octano
        [Route("/ObtenerEntidadOctano/{strLlave}/{decIdUsuario}", "GET")]
        public class ObtenerEntidadOctano : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdUsuario { get; set; }
        }
        #endregion

        #endregion
    }
}