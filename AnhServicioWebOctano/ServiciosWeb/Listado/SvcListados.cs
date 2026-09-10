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

        #region Certificados de calidad

        /// <summary>
        /// Método que obtiene los productos de especificación.
        /// </summary>
        /// <param name="request">Objeto para obtener los productos de especificación.</param>
        /// <returns>Lista de objetos O_PRODUCTO_ESPEC_CTY.</returns>
        public List<O_PRODUCTO_ESPEC_CTY> GET(ListarProductoEspecificacion request)
        {
            string strMensajeError = "";
            List<O_PRODUCTO_ESPEC_CTY> vListPruebasCalidad = null;
            vListPruebasCalidad = gListado.ListarProductoEspecificacion(request.strLlave, request.decIdEstadoTipoPrueba,request.decIdProductoPadre, ref strMensajeError);
            return vListPruebasCalidad;
        }

        /// <summary>
        /// Método que obtiene los productos de especificación de calidad por el tipo.
        /// </summary>
        /// <param name="request">Objeto para obtener los productos de especificación de calidad por el tipo.</param>
        /// <returns>Lista de objetos O_PROD_ESPEC_PACL_CTY.</returns>
        public List<O_PROD_ESPEC_PACL_CTY> GET(ListarGruposProductoEspecificacionTipoCalidad request)
        {
            string strMensajeError = "";
            List<O_PROD_ESPEC_PACL_CTY> vListPruebasCalidad = null;
            vListPruebasCalidad = gListado.ListarGruposProductoEspecificacionPACL(request.strLlave, request.decIdEntidad, request.decIdTipoActividad, request.decIdUsuario, ref strMensajeError);
            return vListPruebasCalidad;
        }

        /// <summary>
        /// Método que obtiene los productos de especificación de calidad.
        /// </summary>
        /// <param name="request">Objeto para obtener los productos de especificación de calidad.</param>
        /// <returns>Lista de objetos O_PROD_CAL_CTY.</returns>
        public List<O_PROD_CAL_CTY> GET(ListarGruposProductoEspecificacionCalidad request)
        {
            string strMensajeError = "";
            List<O_PROD_CAL_CTY> vListPruebasCalidad = null;
            vListPruebasCalidad = gListado.ListarGruposProductoEspecificacionCalidad(request.strLlave,  request.decIdUsuario, request.decIdTipoActividad, request.decIdProductoPadre, request.strAplicacionOrigen, ref strMensajeError);
            return vListPruebasCalidad;
        }

        /// <summary>
        /// Método que obtiene los tanques por entidad.
        /// </summary>
        /// <param name="request">Objeto para obtener los tanques por entidad.</param>
        /// <returns>Lista de objetos O_LISTA_TANQUE_ENTIDAD_CTY.</returns>
        public List<O_LISTA_TANQUE_ENTIDAD_CTY> GET(ListarTanquePorEntidad request)
        {
            string strMensajeError = "";
            List<O_LISTA_TANQUE_ENTIDAD_CTY> vListPruebasCalidad = null;
            vListPruebasCalidad = gListado.ListarTanquePorEntidad(request.strLlave, request.decIdEntidad, ref strMensajeError);
            return vListPruebasCalidad;
        }

        /// <summary>
        /// Método que obtiene las unidades de medida por usuario.
        /// </summary>
        /// <param name="request">Objeto para obtener la tabla de especificación de calidad.</param>
        /// <returns>Lista de objetos O_UM_CANT_CAL_CTY.</returns>
        public List<O_UM_CANT_CAL_CTY> GET(ListarUnidadMedidaPorUsuario request)
        {
            string strMensajeError = "";
            List<O_UM_CANT_CAL_CTY> vListPruebasCalidad = null;
            vListPruebasCalidad = gListado.ListarUnidadMedidaPorUsuario(request.strLlave, request.decIdUsuario, ref strMensajeError);
            return vListPruebasCalidad;
        }

        /// <summary>
        /// Método que obtiene la tabla de especificación de calidad.
        /// </summary>
        /// <param name="request">Objeto para obtener la tabla de especificación de calidad.</param>
        /// <returns>Lista de objetos E_TABLA_ESPECIFICA.</returns>
        public List<E_TABLA_ESPECIFICA> GET(ListarPruebasCalidadTablaEspecificacion request)
        {
            string strMensajeError = "";
            string strCite = request.strCite.Replace('_', '/');
            List<E_TABLA_ESPECIFICA> vListPruebasCalidad = null;
            vListPruebasCalidad = gListado.ListarPruebasCalidadTablaEspecificacion(request.strLlave, request.decIdTablaEspecificacion, request.decIdEntidad, request.decFecha, strCite , ref strMensajeError);
            return vListPruebasCalidad;
        }

        /// <summary>
        /// Método que obtiene el tipo de prueba de calidad.
        /// </summary>
        /// <param name="request">Objeto para obtener el tipo de prueba.</param>
        /// <returns>Lista de objetos O_VOL_TIPOS_OPERACION_CTY.</returns>
        public List<O_ESTADO_TIPO_PRUEBA_CTY> GET(ObtenerTipoPrueba request)
        {
            string strMensajeError = "";
            List<O_ESTADO_TIPO_PRUEBA_CTY> vListPruebasCalidad = null;
            vListPruebasCalidad = gListado.ListarVolumenTipoOperacion(request.strLlave,request.decIdUsuario, ref strMensajeError);
            return vListPruebasCalidad;
        }
        
        /// <summary>
        /// Método que verifica las validaciones de calidad.
        /// </summary>
        /// <param name="request">Objeto para obtener validaciones de calidad.</param>
        /// <returns>Lista de O_VALIDA_CALIDAD_CTY.</returns>
        public List<O_VALIDA_CALIDAD_CTY> GET(ObtenerValidacionCalidad request)
        {
            string strMensajeError = "";
            List<O_VALIDA_CALIDAD_CTY> vObjValidaCalidad = null;
            vObjValidaCalidad = gListado.ObtenerValidacionCalidad(request.strLlave, request.decIdTipoActividad, request.decIdUsuario, ref strMensajeError);
            return vObjValidaCalidad;
        }

        /// <summary>
        /// Método que verifica las validaciones de calidad.
        /// </summary>
        /// <param name="request">Objeto para obtener validaciones de calidad.</param>
        /// <returns>Objetos O_VALIDA_CALIDAD_CTY.</returns>
        public List<O_CERTIFICADOS_ALERTAS_CTY> GET(ListarCertificadosConAlertas request)
        {
            string strMensajeError = "";
            List<O_CERTIFICADOS_ALERTAS_CTY> vObjValidaCalidad = null;
            vObjValidaCalidad = gListado.ListarCertificadosConAlertas(request.strLlave, request.decIdUsuario, request.strFechaInicial, request.strFechaFinal, ref strMensajeError);
            return vObjValidaCalidad;
        }

        /// <summary>
        /// Método que verifica las validaciones de calidad.
        /// </summary>
        /// <param name="request">Objeto para obtener validaciones de calidad.</param>
        /// <returns>Objetos O_VALIDA_CALIDAD_CTY.</returns>
        public List<O_CERTIFICADO_ALERTAS_CTY> GET(ListarCertificadoConAlertas request)
        {
            string strMensajeError = "";
            List<O_CERTIFICADO_ALERTAS_CTY> vObjValidaCalidad = null;
            vObjValidaCalidad = gListado.ListarCertificadoConAlertas(request.decIdRegistroCalPrincipal, ref strMensajeError);
            return vObjValidaCalidad;
        }

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

        /// <summary>
        /// Lista de cátalogos para el servicio web Externo.
        /// </summary>
        /// <param name="request">Objeto para obtener validaciones de calidad.</param>
        /// <returns>Objetos O_VALIDA_CALIDAD_CTY.</returns>
        public List<O_LISTADO_MARCA_LUB_CTY> GET(ListarMarcasLubricantes request)
        {
            string strMensajeError = "";
            List<O_LISTADO_MARCA_LUB_CTY> vObjMarcaLubricante = null;
            vObjMarcaLubricante = gListado.ListarMarcasLubricantes(request.strLlave, ref strMensajeError);
            return vObjMarcaLubricante;
        }

        /// <summary>
        /// Lista de Tipos de Modena y Cambio
        /// </summary>
        /// <param name="request">Objeto para obtener lista de monedas y cambio</param>
        /// <returns>Objetos O_LISTADO_MONEDA_CTY</returns>
        public List<O_LISTADO_MONEDA_CTY> GET(ListarMonedas request)
        {
            string strMensajeError = "";
            List<O_LISTADO_MONEDA_CTY> vObjMonedas = null;
            vObjMonedas = gListado.ListarMonedas(request.strLlave, ref strMensajeError);
            return vObjMonedas;
        }


        /// <summary>
        /// Lista de Nombres Productos
        /// </summary>
        /// <param name="request">Objeto para obtener lista de nombres_productos</param>
        /// <returns>Objetos O_LISTA_NOMBRE_PRODUCTO_CTY</returns>
        public List<O_LISTA_NOMBRE_PRODUCTO_CTY> GET(ListarNombreProductos request)
        {
            string strMensajeError = "";
            List<O_LISTA_NOMBRE_PRODUCTO_CTY> vObjNombreProductos = null;
            vObjNombreProductos = gListado.ListarNombreProductos(request.strLlave, request.decIdEntidad, request.decIdTablaEspec, ref strMensajeError);
            return vObjNombreProductos;
        }

        /// <summary>
        /// Lista de Dependientes
        /// </summary>
        /// <param name="request">Objeto para obtener lista de entidades dependientes y tablas especificas</param>
        /// <returns>Objetos O_LISTA_DEPENDIENTES_CTY</returns>
        public List<O_LISTA_DEPENDIENTES_CTY> GET(ListarDependientes request)
        {
            string strMensajeError = "";
            List<O_LISTA_DEPENDIENTES_CTY> vObjNombreProductos = null;
            vObjNombreProductos = gListado.ListarDependientes(request.strLlave, request.decIdUsuario, ref strMensajeError);
            return vObjNombreProductos;
        }

        /// <summary>
        /// Lista de Dependientes
        /// </summary>
        /// <param name="request">Objeto para obtener lista de entidades dependientes y tablas especificas</param>
        /// <returns>Objetos O_LISTA_DOCUMENTOS_CTY</returns>
        public List<O_LISTA_DOCUMENTOS_CTY> GET(ListarDocumentos request)
        {
            string strMensajeError = "";
            List<O_LISTA_DOCUMENTOS_CTY> vObjListadoDocumentos = null;
            vObjListadoDocumentos = gListado.ListarDocumentos(request.strLlave, request.strCite, request.decIdTipoRespaldo, ref strMensajeError);
            return vObjListadoDocumentos;
        }

        public List<O_LISTA_DOCUMENTOS_CTY> GET(ListarDocumentosDetalle request)
        {
            string strMensajeError = "";
            List<O_LISTA_DOCUMENTOS_CTY> vObjListadoDocumentos = null;
            vObjListadoDocumentos = gListado.ListarDocumentosDetalle(request.strLlave,  request.decIdDocumento, ref strMensajeError);
            return vObjListadoDocumentos;
        }

        /// <summary>
        /// Lista de Ampliaciones programadas
        /// </summary>
        /// <param name="request">Objeto para obtener lista ampliaciones por entidad, actividad y fecha</param>
        /// <returns>listado de O_LISTA_AMPLIACIONES_CTY</returns>
        public List<O_LISTA_AMPLIACIONES_CTY> GET(ListarAmpliaciones request)
        {
            string strMensajeError = "";
            List<O_LISTA_AMPLIACIONES_CTY> vObjListadoDocumentos = null;
            vObjListadoDocumentos = gListado.ListarAmpliaciones(request.strLlave, request.decIdEntidad, request.decIdTipoActividad, request.decIdAmpliacion, ref strMensajeError);
            return vObjListadoDocumentos;
        }

        /// <summary>
        /// Lista de Entidades habilitadas para registro de octano calidad (para arbol de amplicaciones)
        /// </summary>
        /// <param name="request">Objeto para obtener las entidades, actividades habilitadas para OCT-CCC</param>
        /// <returns>llistado de O_LISTA_ARBOL_ENTIDADES_CTY</returns>
        public List<O_LISTA_ARBOL_ENTIDADES_CTY> GET(ListarArbolEntidades request)
        {
            string strMensajeError = "";
            List<O_LISTA_ARBOL_ENTIDADES_CTY> vObjListado = null;
            vObjListado = gListado.ListarArbolEntidades(request.strLlave, request.decIdEntidad, request.decIdTipoActividad, request.decIdUsuario, request.decIdTipo, ref strMensajeError);
            return vObjListado;
        }

        /// <summary>
        /// Lista de Propietarios de certificados de calidad
        /// </summary>
        /// <param name="request">Objeto para obtener el cite, actividad e identificador de la relacion de entidades con certificados de calidad</param>
        /// <returns>listado de O_LISTA_PROPIETARIOS_CTY</returns>
        public List<O_LISTA_PROPIETARIOS_CTY> GET(ListarPropietarios request)
        {
            string strMensajeError = "";
            List<O_LISTA_PROPIETARIOS_CTY> vObjListado = null;
            vObjListado = gListado.ListarPropietarios(request.strLlave, request.decFechaInicial, request.decFechaFinal, request.decIdEntidad, request.decIdTipoActividad, request.decIdUsuario, ref strMensajeError);
            return vObjListado;
        }

        /// <summary>
        /// Lista Entidades de todos los Propietarios de certificados de calidad
        /// </summary>
        /// <param name="request">Objeto para obtener la entidades y actividades de los certificados de calidad</param>
        /// <returns>listado de O_LISTA_ENTIDAD_PROP_CTY</returns>
        public List<O_LISTA_ENTIDAD_PROP_CTY> GET(ListarEntidadesPropietarios request)
        {
            string strMensajeError = "";
            List<O_LISTA_ENTIDAD_PROP_CTY> vObjListado = null;
            vObjListado = gListado.ListarEntidadesPropietarios(request.strLlave, request.decIdUsuario, ref strMensajeError);
            return vObjListado;
        }


        #endregion        
        
        #region Movimiento de volúmenes

        /// <summary>
        /// Listado general de unidades de medida.
        /// </summary>
        /// <param name="request">Objeto para realizar el listado de la unidades de medida.</param>
        /// <returns>Lista de objetos O_UNIDADES_MEDIDA_GRAL_CTY.</returns>
        public List<O_UNIDADES_MEDIDA_GRAL_CTY> GET(ListarUnidadesMedidaGeneral request)
        {
            string strMensajeError = "";
            List<O_UNIDADES_MEDIDA_GRAL_CTY> vListPruebasCalidad = null;
            vListPruebasCalidad = gListado.ListarUnidadesMedidaGeneral(request.strLlave, ref strMensajeError);
            return vListPruebasCalidad;
        }

        /// <summary>
        /// Lista la datos de TPAR_PRODUCTOS.
        /// </summary>
        /// <param name="request">Objeto para realizar el listado de los productos.</param>
        /// <returns>Lista de objetos O_PRODUCTOS_CTY.</returns>
        public List<O_PRODUCTOS_CTY> GET(ListarProductos request)
        {
            string strMensajeError = "";
            List<O_PRODUCTOS_CTY> vListProductos = null;
            vListProductos = gListado.ListarProductos(request.strLlave, request.strCodigo, ref strMensajeError);
            return vListProductos;
        }

        /// <summary>
        /// Lista la datos de TPAR_PRODUCTOS.
        /// </summary>
        /// <param name="request">Objeto para realizar el listado de los productos.</param>
        /// <returns>Lista de objetos O_PRODUCTOS_CTY.</returns>
        public List<O_LISTA_CAMPO_ENTIDAD_CTY> GET(ListarCamposPorEntidad request)
        {
            string strMensajeError = "";
            List<O_LISTA_CAMPO_ENTIDAD_CTY> vListProductos = null;
            vListProductos = gListado.ListarCamposPorEntidad(request.strLlave, request.decIdCamposEntidad,request.decIdCampo,request.decIdEntidad, ref strMensajeError);
            return vListProductos;
        }
        
        /// <summary>
        /// Lista la datos de TPAR_PRODUCTOS.
        /// </summary>
        /// <param name="request">Objeto para realizar el listado de los productos.</param>
        /// <returns>Lista de objetos O_PRODUCTOS_CTY.</returns>
        public List<O_LISTA_PRODE_CTY> GET(ListarProde request)
        {
            string strMensajeError = "";
            List<O_LISTA_PRODE_CTY> vListProductos = null;
            vListProductos = gListado.ListarProde(request.strLlave, request.decIdCampo, request.decIdEntidad, request.decIdTipoReporte, request.decIdProducto, request.decFechaProde, ref strMensajeError);
            return vListProductos;
        }

        /// <summary>
        /// Lista los tipos de actividad.
        /// </summary>
        /// <param name="request">Objeto para realizar el listado de los tipos de actividad.</param>
        /// <returns>Lista de objetos O_TIPOS_ACTIVIDAD_CTY.</returns>
        public List<O_TIPOS_ACTIVIDAD_CTY> GET(ListarTiposActividad request)
        {
            string strMensajeError = "";
            List<O_TIPOS_ACTIVIDAD_CTY> vListProductos = null;
            vListProductos = gListado.ListarTiposActividad(request.strLlave, request.strDescripcion, request.decIdCategoriaActividad, request.decIdTipoActividad, ref strMensajeError);
            return vListProductos;
        }

        /// <summary>
        /// Obtener fecha del último reporte.
        /// </summary>
        /// <param name="request">Objeto para obtener la fecha del último reporte.</param>
        /// <returns>Lista de objetos O_TIPOS_ACTIVIDAD_CTY.</returns>
        public List<O_FECHA_ULTIMO_REPORTE_CTY> GET(ObtenerFechaUltimoReporte request)
        {
            string strMensajeError = "";
            List<O_FECHA_ULTIMO_REPORTE_CTY> vListProductos = null;
            vListProductos = gListado.ObtenerFechaUltimoReporte(request.strLlave, request.decIdCampo, request.decIdEntidad, request.decIdTipoReporte,request.decIdTipoReporte,request.decFechaOperacion, ref strMensajeError);
            return vListProductos;
        }
        
        #endregion

        #region Parametricas
        /// <summary>
        /// Método que obtiene los productos de especificación.
        /// </summary>
        /// <param name="request">Objeto para obtener los productos de especificación.</param>
        /// <returns>Lista de objetos O_PRODUCTO_ESPEC_CTY.</returns>
        public List<O_LISTA_PARAMETRICA_CTY> GET(ListarParametricas request)
        {
            string strMensajeError = "";
            List<O_LISTA_PARAMETRICA_CTY> vListParametricas = null;
            vListParametricas = gListado.ListarParametricas(request.strLlave, request.strTipo, ref strMensajeError);
            return vListParametricas;
        }
        #endregion

        #endregion

        #region Decalaracion de Servicios

        #region Certificados de calidad

        [Route("/ListarProductoEspecificacion/{strLlave}/{decIdEstadoTipoPrueba}/{decIdProductoPadre}", "GET")]
        public class ListarProductoEspecificacion : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdEstadoTipoPrueba { get; set; }
            public decimal decIdProductoPadre { get; set; }
        }

        [Route("/ListarGruposProductoEspecificacionTipoCalidad/{strLlave}/{decIdEntidad}/{decIdTipoActividad}/{decIdUsuario}", "GET")]
        public class ListarGruposProductoEspecificacionTipoCalidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            //public decimal decIdEstadoTipoPrueba { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdTipoActividad { get; set; }
            public decimal decIdUsuario { get; set; }
        }

        [Route("/ListarGruposProductoEspecificacionCalidad/{strLlave}/{decIdUsuario}/{decIdTipoActividad}/{decIdProductoPadre}/{strAplicacionOrigen}", "GET")]
        public class ListarGruposProductoEspecificacionCalidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdUsuario { get; set; }
            public decimal decIdTipoActividad { get; set; }
            public decimal decIdProductoPadre { get; set; }
            public string strAplicacionOrigen { get; set; }
        }

        [Route("/ListarTanquePorEntidad/{strLlave}/{decIdEntidad}", "GET")]
        public class ListarTanquePorEntidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdEntidad { get; set; }
        }

        [Route("/ListarUnidadMedidaPorUsuario/{strLlave}/{decIdUsuario}", "GET")]
        public class ListarUnidadMedidaPorUsuario : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdUsuario { get; set; }
        }

        [Route("/ListarPruebasCalidadTablaEspecificacion/{strLlave}/{decIdTablaEspecificacion}/{decIdEntidad}/{decFecha}/{strCite}", "GET")]
        public class ListarPruebasCalidadTablaEspecificacion : IReturn<List<E_TABLA_ESPECIFICA>>
        {
            public string strLlave { get; set; }
            public decimal decIdTablaEspecificacion { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decFecha { get; set; }
            public string strCite { get; set; }
        }

        [Route("/ObtenerTipoPrueba/{strLlave}/{decIdUsuario}", "GET")]
        public class ObtenerTipoPrueba : IReturn<List<O_VOL_TIPOS_OPERACION_CTY>>
        {
            public string strLlave { get; set; }
            public decimal decIdUsuario { get; set; }
        }

        [Route("/ObtenerValidacionCalidad/{strLlave}/{decIdTipoActividad}/{decIdUsuario}", "GET")]
        public class ObtenerValidacionCalidad : IReturn<O_VALIDA_CALIDAD_CTY>
        {
            public string strLlave { get; set; }
            public decimal decIdTipoActividad { get; set; }
            public decimal decIdUsuario { get; set; }
        }

        [Route("/ListarCertificadosConAlertas/{strLlave}/{decIdUsuario}/{strFechaInicial}/{strFechaFinal}", "GET")]
        public class ListarCertificadosConAlertas : IReturn<O_VALIDA_CALIDAD_CTY>
        {
            public string strLlave { get; set; }
            public decimal decIdUsuario { get; set; }
            public string strFechaInicial { get; set; }
            public string strFechaFinal { get; set; }
        }

        [Route("/ListarCertificadoConAlertas/{decIdRegistroCalPrincipal}", "GET")]
        public class ListarCertificadoConAlertas : IReturn<O_VALIDA_CALIDAD_CTY>
        {            
            public decimal decIdRegistroCalPrincipal { get; set; }         
        }

        [Route("/ListarCatalogosCalidad/{Llave}", "GET")]
        public class ListarCatalogosCalidad : IReturn<O_CATALOGO_CALIDAD_CTY>
        {
            public string Llave { get; set; }
        }
        
        [Route("/ListarMarcasLubricantes/{strLlave}/", "GET")]
        public class ListarMarcasLubricantes : IReturn<EResultado>
        {
            public string strLlave { get; set; }
        }

        [Route("/ListarMonedas/{strLlave}/", "GET")]
        public class ListarMonedas : IReturn<EResultado>
        {
            public string strLlave { get; set; }
        }

        [Route("/ListarNombreProductos/{strLlave}/{decIdEntidad}/{decIdTablaEspec}/", "GET")]
        public class ListarNombreProductos : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdTablaEspec { get; set; }
        }

        [Route("/ListarDependientes/{strLlave}/{decIdUsuario}/", "GET")]
        public class ListarDependientes : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdUsuario { get; set; }
        }

        [Route("/ListarDocumentos/{strLlave}/{strCite}/{decIdTipoRespaldo}/", "GET")]
        public class ListarDocumentos : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strCite { get; set; }
            public decimal decIdTipoRespaldo { get; set; }
        }
        
        [Route("/ListarDocumentosDetalle/{strLlave}/{decIdDocumento}/", "GET")]
        public class ListarDocumentosDetalle : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdDocumento { get; set; }
        }

        [Route("/ListarAmpliaciones/{strLlave}/{decIdEntidad}/{decIdTipoActividad}/{decIdAmpliacion}/", "GET")]
        public class ListarAmpliaciones : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdTipoActividad { get; set; }
            public decimal decIdAmpliacion { get; set; }
        }

        [Route("/ListarArbolEntidades/{strLlave}/{decIdEntidad}/{decIdTipoActividad}/{decIdUsuario}/{decIdTipo}/", "GET")]
        public class ListarArbolEntidades : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdTipoActividad { get; set; }
            public decimal decIdUsuario { get; set; }
            public decimal decIdTipo { get; set; }
        }

        [Route("/ListarPropietarios/{strLlave}/{decFechaInicial}/{decFechaFinal}/{decIdEntidad}/{decIdTipoActividad}/{decIdUsuario}/", "GET")]
        public class ListarPropietarios : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decFechaInicial { get; set; }
            public decimal decFechaFinal { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdTipoActividad { get; set; }
            public decimal decIdUsuario { get; set; }
        }


        [Route("/ListarEntidadesPropietarios/{strLlave}/{decIdUsuario}/", "GET")]
        public class ListarEntidadesPropietarios : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdUsuario { get; set; }
        }

        #endregion       

        #region Movimiento de volúmenes

        [Route("/ListarUnidadesMedidaGeneral/{strLlave}/", "GET")]
        public class ListarUnidadesMedidaGeneral : IReturn<EResultado>
        {
            public string strLlave { get; set; }
        }

        [Route("/ListarProductos/{strLlave}/{strCodigo}", "GET")]
        public class ListarProductos : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strCodigo { get; set; }
        }

        [Route("/ListarCamposPorEntidad/{strLlave}/{decIdCamposEntidad}/{decIdCampo}/{decIdEntidad}", "GET")]
        public class ListarCamposPorEntidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdCamposEntidad { get; set; }
            public decimal decIdCampo { get; set; }
            public decimal decIdEntidad { get; set; }            
        }

        [Route("/ListarProde/{strLlave}/{decIdCampo}/{decIdEntidad}/{decIdTipoReporte}/{decIdProducto}/{decFechaProde}", "GET")]
        public class ListarProde : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdCampo { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdTipoReporte { get; set; }
            public decimal decIdProducto { get; set; }
            public decimal decFechaProde { get; set; }            
        }

        [Route("/ListarTiposActividad/{strLlave}/{strDescripcion}/{decIdCategoriaActividad}/{decIdTipoActividad}", "GET")]
        public class ListarTiposActividad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strDescripcion { get; set; }
            public decimal decIdCategoriaActividad { get; set; }
            public decimal decIdTipoActividad { get; set; }
        }

        [Route("/ObtenerFechaUltimoReporte/{strLlave}/{decIdCampo}/{decIdEntidad}/{decIdTipoReporte}/{decIdTipoOperacion}/{decFechaOperacion}", "GET")]
        public class ObtenerFechaUltimoReporte : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdCampo { get; set; }
            public decimal decIdEntidad { get; set; }
            public decimal decIdTipoReporte { get; set; }
            public decimal decIdTipoOperacion { get; set; }
            public decimal decFechaOperacion { get; set; }            
        }
        
        
        #endregion

        #region Paramtericas
        [Route("/ListarParametricas/{strLlave}/{strTipo}", "GET")]
        public class ListarParametricas : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strTipo { get; set; }
        }
        #endregion
        
        #endregion
    }
}