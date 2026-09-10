using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnhAgenteServicios;
using AnhAgenteServicios.ServicioHydroListados;
using AnhHydroTalleresOpeGarrafasPresentacion.Parametros;
using AnhPersistenciaCore.Core;
using AnhPresentacionDTEP.Lib;
using AnhPresentacionDTEP.Parametros;

namespace AnhHydroTalleresOpeGarrafasPresentacion.Clases.Empadronamiento
{
    public class CHydroListados
    {
        #region Variables de Entorno

        private string _mensajeDeError = string.Empty;
        private string _mapPath;
        private IServicioHydroListados _servicio;
        private CLogs _logs;

        #endregion

        #region Metodos y Funciones

        public CHydroListados(string mapPath, string strIp)
        {
            _servicio = LocalizadorProxy.HydroListadosServicio();
            _mapPath = mapPath;
            _logs = new CLogs(_mapPath, strIp);
        }

        public List<O_ENTIDAD_CTY> obtenerEntidades()
        {
            try
            {
                List<O_ENTIDAD_CTY> listadoEntidades = _servicio.ListadoEntidades(1, 0, CParametrosHydro.strCredencialFuncionario, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {

                    _logs.Error(_mensajeDeError);
                }
                return listadoEntidades;
            }
            catch (Exception exp)
            {

                _logs.Error(exp);
                return null;
            }
        }

        public List<O_ENTIDAD_CTY> obtenerEntidades(decimal decIdEntidad)
        {
            try
            {
                List<O_ENTIDAD_CTY> listadoEntidades = _servicio.ListadoEntidades((decIdEntidad == 0 ? 1 : 2), decIdEntidad, CParametrosHydro.strCredencialFuncionario, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {

                    _logs.Error(_mensajeDeError);
                }
                return listadoEntidades;
            }
            catch (Exception exp)
            {

                _logs.Error(exp);
                return null;
            }
        }

        public O_DETALLE_ENTIDAD_CTY detalleEntidad(decimal decIdEntidad)
        {
            try
            {
                O_DETALLE_ENTIDAD_CTY entidad = _servicio.DetalleEntidad(decIdEntidad, CParametrosHydro.strCredencialFuncionario, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {

                    _logs.Error(_mensajeDeError);
                }
                return entidad;
            }
            catch (Exception exp)
            {

                _logs.Error(exp);
                return null;
            }
        }

        public List<O_DOCUMENTOS_ENTIDAD_CTY> obtenerDocumentosEntidad(decimal decIdEntidad)
        {
            try
            {
                List<O_DOCUMENTOS_ENTIDAD_CTY> listadoDocumentos = _servicio.DocumentosEntidad(decIdEntidad, CParametrosHydro.strCredencialFuncionario, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    _logs.Error(_mensajeDeError);
                }
                return listadoDocumentos;
            }
            catch (Exception exp)
            {
                _logs.Error(exp);
                return null;
            }
        }

        public byte[] obtenerDocumentoEntidadDigital(decimal decIdDocumento)
        {
            try
            {
                O_OBJETO_DIGITAL_CTY documentoDigital = _servicio.ObtenerDocumentoDigital(CParametrosHydro.strCredencialFuncionario, decIdDocumento, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    _logs.Error(_mensajeDeError);
                }
                if (documentoDigital != null)
                {
                    return documentoDigital.OBJETO;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception exp)
            {
                _logs.Error(exp);
                return null;
            }
        }

        public List<O_REPRESENTANTE_CTY> obtenerRepresentantes(decimal decIdEntidad)
        {
            try
            {
                List<O_REPRESENTANTE_CTY> listadoRepresentantes = _servicio.ListadoRepresentantes(decIdEntidad, CParametrosHydro.strCredencialFuncionario, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {

                    _logs.Error(_mensajeDeError);
                }
                return listadoRepresentantes;
            }
            catch (Exception exp)
            {

                _logs.Error(exp);
                return null;
            }
        }

        public O_DETALLE_REPRESENTANTE_CTY detalleRepresentante(decimal decIdRepresentante)
        {
            try
            {
                O_DETALLE_REPRESENTANTE_CTY representante = _servicio.DetalleRepresentante(decIdRepresentante, CParametrosHydro.strCredencialFuncionario, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {

                    _logs.Error(_mensajeDeError);
                }
                return representante;
            }
            catch (Exception exp)
            {

                _logs.Error(exp);
                return null;
            }
        }

        public List<O_DOCUMENTOS_REPRESENTANTE_CTY> obtenerDocumentosRepresentante(decimal decIdRepresentante)
        {
            try
            {
                List<O_DOCUMENTOS_REPRESENTANTE_CTY> listadoDocumentos = _servicio.DocumentosRepresentante(decIdRepresentante, CParametrosHydro.strCredencialFuncionario, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {

                    _logs.Error(_mensajeDeError);
                }
                return listadoDocumentos;
            }
            catch (Exception exp)
            {

                _logs.Error(exp);
                return null;
            }
        }

        public List<O_ACTIVIDAD_CTY> obtenerActividades(decimal decIdEntidad)
        {
            try
            {
                List<O_ACTIVIDAD_CTY> listadoActividades = _servicio.ListadoActividades(decIdEntidad, CParametrosHydro.strCredencialFuncionario, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {

                    _logs.Error(_mensajeDeError);
                }
                return listadoActividades;
            }
            catch (Exception exp)
            {

                _logs.Error(exp);
                return null;
            }
        }

        public byte[] obtener8001()
        {
            return null;
        }

        public byte[] obtenerResolucionInscripcion()
        {
            return null;
        }

        #region Listado Infraestructura

        public List<O_LISTA_PRODUCTOS_CTY> ListadoProductos(string strCodigo)
        {
            try
            {
                List<AnhAgenteServicios.ServicioHydroListados.O_PRODUCTOS_CTY> listadoProducto = _servicio.ListadoProducto(strCodigo, CParametrosHydro.strCredencialFuncionario, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {

                    _logs.Error(_mensajeDeError);
                }
                var listaProd = new List<O_LISTA_PRODUCTOS_CTY>();
                foreach (var item in listadoProducto)
                {
                    var objProducto = new O_LISTA_PRODUCTOS_CTY();
                    switch (item.NOMBRE)
                    {
                        case "BUTANO":
                            objProducto.ID_PRODUCTO = item.ID_PRODUCTO;
                            objProducto.CODIGO = item.CODIGO;
                            objProducto.NOMBRE = "GLP BUTANO";
                            objProducto.DESCRIPCION = item.DESCRIPCION;
                            objProducto.ID_PRODUCTO_PADRE = item.ID_PRODUCTO_PADRE;
                            break;
                        case "PROPANO":
                            objProducto.ID_PRODUCTO = item.ID_PRODUCTO;
                            objProducto.CODIGO = item.CODIGO;
                            objProducto.NOMBRE = "GLP PROPANO";
                            objProducto.DESCRIPCION = item.DESCRIPCION;
                            objProducto.ID_PRODUCTO_PADRE = item.ID_PRODUCTO_PADRE;
                            break;
                        default:
                            objProducto.ID_PRODUCTO = item.ID_PRODUCTO;
                            objProducto.CODIGO = item.CODIGO;
                            objProducto.NOMBRE = item.NOMBRE;
                            objProducto.DESCRIPCION = item.DESCRIPCION;
                            objProducto.ID_PRODUCTO_PADRE = item.ID_PRODUCTO_PADRE;
                            break;
                    }

                    listaProd.Add(objProducto);
                }
                return listaProd;
                //return listadoProducto;
            }
            catch (Exception exp)
            {

                _logs.Error(exp);
                return null;
            }
        }

        public class O_LISTA_PRODUCTOS_CTY
        {
            public decimal ID_PRODUCTO { get; set; }
            public string CODIGO { get; set; }
            public string NOMBRE { get; set; }
            public string DESCRIPCION { get; set; }
            public decimal? ID_PRODUCTO_PADRE { get; set; }
        }
        #endregion

        #endregion
    }
}