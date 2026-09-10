using AnhPersistenciaCore.Aplicacion.Listados;
using AnhPersistenciaCore.Core;
using AnhPersistenciaCore.Entidades;
using Newtonsoft.Json;
using ServiceStack.ServiceHost;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhServicioWebOctano.ServiciosWeb.Listado
{
    public class SvcListados : IService
    {
        #region Variables de Entorno

        private CPersistenciaListados gListado;

        public class EResultado
        {
            public decimal decCodigo { get; set; }
            public string strMensaje { get; set; }
            public object oResultado { get; set; }
        }

        #endregion

        #region Constructor de la Clase
        public SvcListados()
        {
            gListado = new CPersistenciaListados();
        }
        #endregion

        #region Métodos y Funciones
        /// <summary>
        /// Lista de cátalogos para el servicio web Externo.
        /// </summary>
        /// <param name="request">Objeto para obtener validaciones de calidad.</param>
        /// <returns>Objetos O_VALIDA_CALIDAD_CTY.</returns>
        public List<E_CATALOGO_CALIDAD> GET(ListarCatalogosCalidad request)
        {
            string strMensajeError = "";
            List<E_CATALOGO_CALIDAD> vObjValidaCalidad = null;
            vObjValidaCalidad = gListado.ListarCatalogosCalidad(request.Llave, ref strMensajeError);
            return vObjValidaCalidad;
        }
      
        #endregion

        #region Decalaracion de Servicios

        [Route("/ListarCatalogosCalidad/{Llave}", "GET")]
        public class ListarCatalogosCalidad : IReturn<O_CATALOGO_CALIDAD_CTY>
        {
            public string Llave { get; set; }
        }
        
        #endregion
    }
}