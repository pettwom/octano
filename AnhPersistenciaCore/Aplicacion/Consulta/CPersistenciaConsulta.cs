using AnhPersistenciaCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Librerias.Anh.Us;
using AnhPersistenciaCore.Entidades;
using System.Configuration;
using System.Globalization;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using OracleCommand = Oracle.ManagedDataAccess.Client.OracleCommand;
using OracleConnection = Oracle.ManagedDataAccess.Client.OracleConnection;
using OracleDataReader = Oracle.ManagedDataAccess.Client.OracleDataReader;
using OracleParameter = Oracle.ManagedDataAccess.Client.OracleParameter;
using Oracle.ManagedDataAccess.Types;

namespace AnhPersistenciaCore.Aplicacion.Consulta
{
    public class CPersistenciaConsulta
    {
        #region Variables

        private const string TipoMensajeError = "Repositorio";

        private readonly EPResultado _resultado = new EPResultado();

        private static readonly string ConString = "User Id=" + ConfigurationManager.AppSettings["Usuario"] + "; " +
                                          "password=" + ConfigurationManager.AppSettings["Password"] + "; " +
                                          "Data Source=" + ConfigurationManager.AppSettings["Servidor"] + ":" +
                                          ConfigurationManager.AppSettings["Puerto"] + "/" +
                                          ConfigurationManager.AppSettings["BaseDatos"] + "; " +
                                          "Pooling=true; Min Pool Size=1; Max Pool Size=80; " +
                                          "Incr Pool Size=1; Decr Pool Size=1; " +
                                          "Connection Lifetime=30; Connection Timeout=20; " +
                                          "Enlist=false;";

        #endregion

        /// <summary>
        /// Obtiene el id del tipo de reporte.
        /// </summary>
        /// <param name="strCredencial">Credencial de acceso al sistema.</param>
        /// <param name="strTipoOperacionPadre">Nombre del tipo de operacion padre.</param>
        /// <param name="strTipoOperacionHijo">Nombre del tipo de operacion hijo.</param>
        /// <param name="strMensajeError">Mensaje de error.</param>
        /// <returns>Retorna un objeto complejo O_RESULTADO_CTY con el codigo de error.</returns>
        public List<O_RESULTADO_CTY> ObtenerTipoOperacion(string strCredencial, string strTipoOperacionPadre,
                                                        string strTipoOperacionHijo, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                var resultado = new List<O_RESULTADO_CTY>();
                decimal decIdTipoOperacionHijo;

                #region Obtener el ID del tipo de operación

                try
                {
                    List<O_VOL_TIPOS_OPERACION_CTY> objListado =
                        ctx.PCAN_LISTADOS_P_LISTADO_TIPOS_OPERACION(strCredencial, "", "", "").ToList();

                    List<O_VOL_TIPOS_OPERACION_CTY> objListadoAuxiliar = (from tOp in objListado
                                                                          where
                                                                              tOp.NOMBRE.ToUpper().Contains(
                                                                                  strTipoOperacionPadre)
                                                                          select tOp).ToList();

                    decimal decIdTipoOperacionPadre = objListadoAuxiliar[0].ID_TIPO_OPERACION;

                    objListadoAuxiliar = (from tOp in objListado
                                          where
                                              tOp.NOMBRE.ToUpper().Contains(strTipoOperacionHijo) &&
                                              tOp.ID_TOPERACION_PADRE == decIdTipoOperacionPadre
                                          select tOp).ToList();
                    decIdTipoOperacionHijo = objListadoAuxiliar[0].ID_TIPO_OPERACION;
                }
                catch (Exception)
                {
                    decIdTipoOperacionHijo = 0;
                }

                #endregion

                var obj = new O_RESULTADO_CTY { ID_TABLA = decIdTipoOperacionHijo, MENSAJE_ERROR = "", RESULTADO = 1 };
                resultado.Add(obj);
                return resultado;
            }
        }

        public O_CONSULTA_ENTIDAD_CTY ObtenerEntidadOctano(string strCredencial, decimal decIdUsuario, ref string strMensajeError)
        {
            using (var ctx = new EntidadesOctano())
            {
                try
                {
                    List<O_CONSULTA_ENTIDAD_CTY> objListado =
                        ctx.PUSR_CONSULTAS_P_CONSULTA_ENTIDAD(strCredencial, decIdUsuario).ToList();
                    return objListado[0];
                }
                catch (Exception ex)
                {
                    strMensajeError =
                        CMensajeError.FormatearMensajeDeError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex,
                                                              TipoMensajeError);
                    return null;
                }
            }
        }
        
        public EPResultado ListadoPermisoCalidadIdUsuario(EListaControlesUsuario ob)
        {
            var objDatos = new List<O_LISTA_PERMISO_CALIDAD_CTY>();
            var dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_LISTADOS.P_LISTADO_PERMISO_CALIDAD";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, ob.decIdUsuario,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("2", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        #region lista acumulada de movimientos
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_LISTA_PERMISO_CALIDAD_CTY();

                                    reg.ID_GESTION_OCTANO = re.GetDecimal(0);
                                    reg.ID_USUARIO_ANH = re.GetDecimal(1);
                                    reg.NOMBRE_COMPLETO = re[2].ToString();
                                    reg.DIRECCION = re[3].ToString();
                                    reg.FECHA_FIN = re[4].ToString();
                                    reg.APLICACION = re[5].ToString();
                                    reg.ACTIVIDAD = re[6].ToString();
                                    reg.ID_ENTIDAD = re.GetDecimal(7);
                                    reg.ENTIDAD = re[8].ToString();

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        #endregion

                        if (objDatos.Count > 0)
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";
                            _resultado.oResultado = objDatos;
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros.";
                            _resultado.oResultado = null;
                        }

                        return _resultado;
                    }
                }
            }
            catch (Exception ex)
            {
                _resultado.intCodigo = -100;
                _resultado.strMensaje = ex.Message;
                _resultado.oResultado = null;
                return _resultado;
            }
        }
    }
}
