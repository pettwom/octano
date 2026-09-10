

using System.Diagnostics.Eventing.Reader;
using AnhAgenteServicios;
using AnhAgenteServicios.ServicioHydroListados;
using AnhPersistenciaCore.Core;
using AnhPersistenciaCore.Entidades;
using AnhPersistenciaCore.Entidades.Parametros.VolumenesCalidad;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Entidades;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionCalidad;
using AnhServicioWebOctano.ServiciosWeb.Gestion;
using DevExpress.Web;
using DevExpress.XtraCharts.Design;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraRichEdit.Import.Rtf;
using Librerias.Anh.Us;
using Newtonsoft.Json;
using ServiceStack.Common;
using ServiceStack.Common.Utils;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAdministrador;

namespace AnhPresentacionDTEP.Comun.UControl
{
    using System.Globalization;
    using System.Reflection;

    public partial class ucRegistroCertificadoCalidadCarburantes : System.Web.UI.UserControl
    {
        #region Parametros de entrada

        public byte[] archivo;
        public string Llave { get; set; }
        public decimal IdUsuario { get; set; }
        public decimal IdEntidad { get; set; }
        public string NumeroCorrelativoReporte { get; set; }
        public int TipoDeRegistro { get; set; }
        public string NumeroCorrelativo { get; set; }
        String[] vValorDatosGrilla;
        #endregion

        bool vTieneJustificacion = false;
        List<O_REPORTE_CALIDAD> vColReporteCalidad = null;
        public int count;
        public int count1;
        int vNumeroRegistro;
        public int vContadorModificacion = 0;
        string strAccion = "";
        public delegate void GuardarRegistroEventHandler(GuardarRegistroCommandEventArgs e);
        public event GuardarRegistroEventHandler GridSelectorChanged;
        JsonServiceClient clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
        JsonServiceClient clienteInfraestructura = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["UrlWSInfraestructura"]);
        readonly IServicioHydroListados _servicioHydroListado = LocalizadorProxy.ServicioHydroListados();
        JsonServiceClient _clientAdminHydro = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);
        JsonServiceClient client = new JsonServiceClient(CAppSettings.ServicioOctanoVolumenes);

        public class GuardarRegistroCommandEventArgs
        {
            public string NumeroCorrelativo { get; protected set; }
            public GuardarRegistroCommandEventArgs(string pNumeroCorrelativo)
            {
                NumeroCorrelativo = pNumeroCorrelativo;
            }
        }
        #region Metodos
        /// <summary>
        /// Limpia el formulario de declaración con volumen Cero.
        /// </summary>
        public void LimpiarFormularioDeclaracionVolumenCero()
        {
            deFechaImportacionVolCero.Text = "";
        }
        /// <summary>
        /// Limpia todos los datos registrados en el formulario, incluso los creados dentro de la grilla.
        /// </summary>
        public void LimpiarFormularioRegistroCalidadActual()
        {
            cite.Visible = false;
            TextBoxNumeroCite.Text = "";
            txtFechaInicial.Text = "";
            TextBoxLote.Text = "";
            cmbTag.Items.Clear();
            //cmbMarca.DataBind();
            cmbTag.SelectedIndex = -1;
            TextBoxVolumen.Text = "";
            cmbUnidadMedVol.SelectedIndex = -1;
            cmbMarca.SelectedIndex = -1;
            TextBoxVolumenMuestra.Text = "";
            TextBoxPrecio.Text = "";
            cmbMoneda.SelectedIndex = -1;
            txtResolucion.Text = "";
            //txtDestino.Text = "";
            //txtModalidadTransporte.Text = "";
            cmbModalidadTransporte.SelectedIndex = -1;
            txtEmpresaProveedora.Text = "";
            txtRutaInternacion.Text = "";
            txtTanqueOrigenExterno.Text = "";
            txtNroLoteVerf.Text = "";
            //tkblistaObs.Text = "";
            txtFechaMuIm.Text = "";
            txtFechaRA.Text = "";
            txtNombreProducto.Text = "";
            NumeroRegistro.Text = "";
            cmbNombreProducto.SelectedIndex = -1;
            EspecificacionMaxima.Text = "";
            EspecificacionMinima.Text = "";
            //otros                         
            LabelEntidad.Text = "";
            ValorNlgi.Text = "";
            ValorApi.Text = "";
            ClasificacionNLGI.Text = "";
            RegistroApi.Text = "";
            LabelIdPruebaCalidad.Text = "";
            LabelTipoAlerta.Text = "";
            AlmacenamientoAlerta.Text = "";
            NumeroRegistro.Text = "";
            grdCertificadoCalidad.DataSource = null;
            grdCertificadoCalidad.DataBind(); 
        }
        /// <summary>
        /// Inicializa los valores del conrol de usuario a la configuración predefinida en tiempo de diseño.
        /// </summary>
        public void InicializarControlPredefinido()
        {
            cite.Visible = false;
            TextBoxNumeroCite.Text = "";
            LabelMarca.Visible = false;
            cmbMarca.Visible = false;
            LabelVolumenMuestra.Visible = false;
            TextBoxVolumenMuestra.Visible = false;
            LabelPrecio.Visible = false;
            TextBoxPrecio.Visible = false;
            lblEmpresaProveedora.Visible = false;
            txtEmpresaProveedora.Visible = false;
            lblModalidadTransp.Visible = false;
            cmbModalidadTransporte.Visible = false;
            lblTanqueOrigenExterno.Visible = false;
            txtTanqueOrigenExterno.Visible = false;
            lblNroLoteVerf.Visible = false;
            txtNroLoteVerf.Visible = false;
            cmbMoneda.Visible = false;
            lblResolucion.Visible = false;
            txtResolucion.Visible = false;            
            lblFechaMuIm.Visible = false;
            txtFechaMuIm.Visible = false;
            txtFechaRA.Visible = false;
            lblNombreProducto.Visible = false;
            txtNombreProducto.Visible = false;
            cmbNombreProducto.Visible = false;            
            btnVisualiza.Visible = Convert.ToBoolean(Session[CVariablesSesion.IsSuperAdministrador]);


            grdCertificadoCalidad.Columns["decIdPruebaCalidad"].Visible = false;
            grdCertificadoCalidad.Columns["PruebaEnsayo"].Visible = true;
            grdCertificadoCalidad.Columns["MetodoASTM"].Visible = true;
            grdCertificadoCalidad.Columns["Unidad"].Visible = true;
            grdCertificadoCalidad.Columns["ValorReportado"].Visible = true;
            grdCertificadoCalidad.Columns["IsoNLGI"].Visible = true;
            grdCertificadoCalidad.Columns["EspecMinima"].Visible = true;
            grdCertificadoCalidad.Columns["EspecMaxima"].Visible = true;
            grdCertificadoCalidad.Columns["Justificacion"].Visible = true;
            grdCertificadoCalidad.Columns["Especificacion"].Visible = true;
            //variables
            count = 0;
            count1 = 0;

            panelRegistroNormal.Visible = true;
            panelVolumenCero.Visible = false;

            ckbxVolumenCero.Checked = false;

            Session[CParametrosCalidad.cColListadoPutoCustodio] = new List<O_PUNTO_CUSTODIO_CTY>();
        }
        /// <summary>
        /// Inicializa los parametros  para cargar la información en la grilla de acuerdo a los datos generados en el metodo.
        /// </summary>
        public void CargarParametroGrillaCalidad()
        {
            //parametros para configurar grilla
            if (Session[CParametrosCalidad.cObjValoresCargarGrilla] != null)
            {
                vValorDatosGrilla = Session[CParametrosCalidad.cObjValoresCargarGrilla].ToString().Split('|');
                //if (!IsPostBack && (vValorDatosGrilla[2] != null && vValorDatosGrilla[1] != null && vValorDatosGrilla[0] != null))
                if (Convert.ToDecimal(Session["conteo"]) >= 1
                    && (vValorDatosGrilla[2] != null && vValorDatosGrilla[1] != null
                        && vValorDatosGrilla[0] != null))
                {
                    CargaGrilla(
                        vValorDatosGrilla[1],
                        vValorDatosGrilla[2],
                        vValorDatosGrilla[3],
                        Convert.ToDecimal(vValorDatosGrilla[0]));
                    Session[CParametrosCalidad.cValoresMenu] = vValorDatosGrilla;
                    Session["nuevo"] = Convert.ToDecimal(Session["nuevo"]) + 1;
                }
                List<O_VALIDA_CALIDAD_CTY> listaCalidad = (List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad];
                var result = listaCalidad.FirstOrDefault(x => x.VALIDA_CALIDAD == "VOLUMEN");
                divPanelSwitch.Visible = (result != null || Convert.ToBoolean(Session[CVariablesSesion.IsSuperAdministrador])) && TipoDeRegistro == 1;

            }
            else if ((TipoDeRegistro == 2 || TipoDeRegistro == 3) && Convert.ToDecimal(Session["conteo"]) == 1)
            {
                divPanelSwitch.Visible = false;
                CargaGrilla("", "", "", 0);
            }

            //Opcion documento digital
            CargarOpcionAdjuntar();
        }
        /// <summary>
        /// valida los datos de acuerdo al indice (fila de la grilla)
        /// </summary>
        /// <param name="indice"> Fila de la grilla </param>
        /// <param name="strRegistroApi">Contiene datos del API</param>
        /// <param name="strClasificacionNLGI">Contiene dato de NLGI</param>
        /// <returns></returns>
        private bool ValidaApiNlgr(decimal indice, string strRegistroApi, string strClasificacionNLGI)
        {
            bool validacionCorrecto = false;
            string vValorRegex = "";
            if ((strRegistroApi != "" && indice == Convert.ToInt16(strRegistroApi)) || (strClasificacionNLGI != "" && indice == Convert.ToUInt16(strClasificacionNLGI)))
            {
                int vRegistroApi = 0;
                if (strRegistroApi != "")
                {
                    vRegistroApi = Convert.ToInt16(strRegistroApi);
                    vValorRegex = CParametrosCalidad.cExpresionRegularLetrasNumeros;
                }
                else if (strClasificacionNLGI != "")
                {
                    vRegistroApi = Convert.ToInt16(strClasificacionNLGI);
                    vValorRegex = CParametrosCalidad.cExpresionRegularLetrasNumerosSimbolo;
                }
                TextBox vObjValorReportado = (TextBox)grdCertificadoCalidad.FindRowCellTemplateControl(vRegistroApi, null, "txtValorReportado");
                if (VerificaExpresionRegular(vObjValorReportado.Text, vValorRegex))
                {
                    object vEspecificacionMinima = (grdCertificadoCalidad.GetRowValues(vRegistroApi, "strEspecMinima"));
                    object vEspecificacionMaxima = (grdCertificadoCalidad.GetRowValues(vRegistroApi, "strEspecMaxima"));
                    if (strRegistroApi != "")
                    {
                        if (vEspecificacionMinima != null && !VerificarValorApi(vObjValorReportado.Text, vEspecificacionMinima.ToString()))
                        {
                            alert.Visible = true;
                            idmsjError.Text = "EL VALOR API ES INCORRECTO.";
                            ValorApi.Text = "0";
                            vObjValorReportado.BorderColor = Color.Red;
                        }
                        else
                        {
                            alert.Visible = false;
                            vObjValorReportado.BorderColor = Color.Silver;
                            validacionCorrecto = true;
                        }
                    }
                    else if (strClasificacionNLGI != "")
                    {
                        if (vEspecificacionMinima != null && vEspecificacionMaxima != null &&
                            !VerificarClasificacionNlgi(vObjValorReportado.Text,
                                vEspecificacionMinima.ToString(), vEspecificacionMaxima.ToString()))
                        {
                            alert.Visible = true;
                            idmsjError.Text = "EL VALOR NLGI ES INCORRECTO.";
                            ValorNlgi.Text = "0";
                            vObjValorReportado.BorderColor = Color.Red;
                        }
                        else
                        {
                            alert.Visible = false;
                            vObjValorReportado.BorderColor = Color.Silver;
                            validacionCorrecto = true;
                        }
                    }
                }
                else
                {
                    alert.Visible = true;
                    idmsjError.Text = "CARACTER ESPECIAL INVÁLIDO.";
                }
            }
            return validacionCorrecto;
        }
        /// <summary>
        /// Valida el valor enviado para API,  considerando el valor de la especificación minima.
        /// </summary>
        /// <param name="pValorApi">Valor de API</param>
        /// <param name="pEspecificacionMinima">valor Minimo</param>
        /// <returns></returns>
        private bool VerificarValorApi(string pValorApi, string pEspecificacionMinima)
        {
            bool EsVerificacionCorrecta = false;
            decimal vCantidadApi = 0;
            long vCantidadEspecifMinima = 0;
            string StrCantidadApi;
            decimal vValorNumerico = 0;
            string cadenaInicial;
            string cadenaFinal;
            string vCadenaValorTotal = "";
            bool vResultadoVerificacion = false;
            String[] ValorMinimo = pEspecificacionMinima.Split('ó');
            if (ValorMinimo.Count() == 2)
            {
                if (ValorMinimo[0].Trim().Count() == ValorMinimo[1].Trim().Count())
                {
                    Array.Reverse(ValorMinimo);
                    vCadenaValorTotal = ValorMinimo[ValorMinimo.Count() - 1].Replace("-", "");
                }
                else
                {
                    if (ValorMinimo[0].Trim().Count() < ValorMinimo[1].Trim().Count())
                        vCadenaValorTotal = ValorMinimo[0].Trim() + 0;
                    else
                        vCadenaValorTotal = ValorMinimo[1].Trim() + 0;
                }
            }
            else if (ValorMinimo.Count() == 1)
            {
                if (ValorMinimo[0].Count() < 3)
                {
                    vCadenaValorTotal = ValorMinimo[0] + 0;
                }
                else
                {
                    vCadenaValorTotal = ValorMinimo[0];
                }
            }
            StrCantidadApi = "";
            for (int j = 0; j < vCadenaValorTotal.Trim().Count(); j++)
            {
                StrCantidadApi = StrCantidadApi + Convert.ToString(Encoding.ASCII.GetBytes(vCadenaValorTotal.Substring(j, 1))[0]);
            }
            vResultadoVerificacion = decimal.TryParse(StrCantidadApi, out vValorNumerico);
            if (vResultadoVerificacion)
                vCantidadEspecifMinima = Convert.ToInt64(StrCantidadApi);
            StrCantidadApi = "";
            if (pValorApi.Count() > 2 && pValorApi.Count() < 4)
            {
                cadenaInicial = pValorApi.Substring(0, 1);
                cadenaFinal = pValorApi.Substring(1, 1);
                StrCantidadApi = Convert.ToString(Encoding.ASCII.GetBytes(cadenaInicial)[0]) + Convert.ToString(Encoding.ASCII.GetBytes(cadenaFinal)[0] + Convert.ToString(Encoding.ASCII.GetBytes(pValorApi.Substring(2))[0]));
                vResultadoVerificacion = decimal.TryParse(StrCantidadApi, out vValorNumerico);
                if (vResultadoVerificacion)
                    vCantidadApi = Convert.ToInt64(StrCantidadApi);
            }
            else if (pValorApi.Count() == 2)
            {
                cadenaInicial = pValorApi.Substring(0, 1);
                cadenaFinal = pValorApi.Substring(1, 1);
                StrCantidadApi = Convert.ToString(Encoding.ASCII.GetBytes(cadenaInicial)[0]) + Convert.ToString(Encoding.ASCII.GetBytes(cadenaFinal)[0] + "48");
                vCantidadApi = Convert.ToInt64(StrCantidadApi);
            }

            if (vCantidadApi >= vCantidadEspecifMinima && vCantidadApi <= 909057)
            {
                ValorApi.Text = "";
                EsVerificacionCorrecta = true;
            }

            if (ValidaCalidad.Text != "" && ValidaCalidad.Text == "0")
            {
                //if (!EsVerificacionCorrecta)
                //    AlmacenamientoAlerta.Text = "1";
                //else
                //    AlmacenamientoAlerta.Text = "";
                //EsVerificacionCorrecta = true;
                if (!EsVerificacionCorrecta)
                {
                    AlmacenamientoAlerta.Text = "1";
                    EsVerificacionCorrecta = false;
                }
                else
                {
                    AlmacenamientoAlerta.Text = "";
                    EsVerificacionCorrecta = true;
                }

            }
            return EsVerificacionCorrecta;
        }
        /// <summary>
        /// Valida el valor para NLGI, considerando el valor de la especificación minima.
        /// </summary>
        /// <param name="pClasificacionNlgi">Valor de la Clasificación NLGI</param>
        /// <param name="pEspecificacionMinima">Especificación minima</param>
        /// <param name="pEspecificacionMaxima">Especificacion maxima</param>
        /// <returns></returns>
        private bool VerificarClasificacionNlgi(string pClasificacionNlgi, string pEspecificacionMinima, string pEspecificacionMaxima)
        {
            bool esVerificacionCorrecta = false;
            if (pEspecificacionMinima.Trim() == pEspecificacionMaxima.Trim())
            {
                String[] vValor = pEspecificacionMinima.Split('ó');
                for (int i = 0; i < vValor.Count(); i++)
                {
                    if (pClasificacionNlgi == vValor[i].Trim())
                    {
                        i = 99999;
                        esVerificacionCorrecta = true;
                        ValorNlgi.Text = "";
                    }
                }
            }

            if (ValidaCalidad.Text != "" && ValidaCalidad.Text == "0")
            {
                if (!esVerificacionCorrecta)
                    AlmacenamientoAlerta.Text = "1";
                else
                    AlmacenamientoAlerta.Text = "";
                esVerificacionCorrecta = true;
            }
            return esVerificacionCorrecta;
        }
        /// <summary>
        /// Envia al servicio los datos de Calidad registrado en el formulario.
        /// </summary>
        /// <param name="vColObjetoCalidad">Lista del objeto calidad</param>
        /// <param name="vObjObjetoCalidad">Objeto calidad</param>
        private void guardarObjCalidad(List<ObjetoCalidad> vColObjetoCalidad, ObjetoCalidad vObjObjetoCalidad)
        {
            idmsjErrorNumerico.Text = "";
            idmsjError.Text = "";
            // Realizar consumo por SERVICIO WEB.


            #region Llenado del objeto complejo

            var vObjPruebaCalidad = new PruebaCalidad();
            vObjPruebaCalidad.valor_lote = TextBoxLote.Text.Trim();
            vObjPruebaCalidad.id_entidad = Convert.ToInt32(Session[CVariablesSesion.IdEntidad]);

            vValorDatosGrilla = (String[])Session[CParametrosCalidad.cValoresMenu];
            vObjPruebaCalidad.id_producto = Convert.ToInt32(vValorDatosGrilla[0]);
            vObjPruebaCalidad.id_tabla_espec = Convert.ToInt16(vValorDatosGrilla[1]);
            vObjPruebaCalidad.id_usuario = Convert.ToInt32(Session[CVariablesSesion.UsuarioId]);
            vObjPruebaCalidad.op_debe = FormatHelper.ToDecimal(TextBoxVolumen.Text);


            vObjPruebaCalidad.ope_haber = 0;
            vObjPruebaCalidad.fecha_operacion = Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", Convert.ToDateTime(txtFechaInicial.Text)));
            if (Session[CParametrosCalidad.cValidacionCalidad] != null && txtFechaMuIm.Visible==true && ((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "FECHA_MUIM") != null)
            {
                vObjPruebaCalidad.fecha_muestra = Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", Convert.ToDateTime(txtFechaMuIm.Text)));
            }
            else
            {
                vObjPruebaCalidad.fecha_muestra = 0;
            }
            vObjPruebaCalidad.id_tipo_registro = 2; //1,2 y 3
            vObjPruebaCalidad.id_cantidad_padre = 0;
            vObjPruebaCalidad.id_unidad_medida = Convert.ToInt16(cmbUnidadMedVol.Value);
            vObjPruebaCalidad.id_entidad_origen = Convert.ToInt32(Session[CVariablesSesion.IdEntidad]);
            vObjPruebaCalidad.id_entidad_destino = pnlEntidadesRelacionados.Visible== false ? -99999 : Convert.ToInt32(cmbEnitdadDestino.Value);
            vObjPruebaCalidad.id_tipo_operacion = cmbTipoOperacion.Visible == true ? Convert.ToInt32(cmbTipoOperacion.Value) : 1; //??????????
            vObjPruebaCalidad.id_tipo_reporte = 3;
            vObjPruebaCalidad.id_volumen_datos = 0;
            vObjPruebaCalidad.id_moneda = Convert.ToInt16(cmbMoneda.Value);
            vObjPruebaCalidad.id_marca_producto = Convert.ToInt16(cmbMarca.Value);
            if (pnlProductosBase.Visible==true)
            {
                //var listaCustorio = new
                //{ Tanque_A = cmbBaseA.Text, Tanque_B = cmbBaseB.Text };
                //string jsonResult = JsonConvert.SerializeObject(listaCustorio);
                vObjPruebaCalidad.nombre_producto = cmbBaseA.Text + "|" + cmbBaseB.Text;           
            }
            else if (pnlTanquePrecinto.Visible==true)
            {
                vObjPruebaCalidad.nombre_producto = txtPlacaCisterna.Text.ToUpper() + "|" + txtNroPrecintos.Text.ToUpper();   
            }
            else if (cmbModalidadTransporte.Visible == true)
            {
                vObjPruebaCalidad.nombre_producto = cmbModalidadTransporte.Text;
            }
            vObjPruebaCalidad.ruta_internacion = txtRutaInternacion.Text!="" ? txtRutaInternacion.Text.ToUpper() : null;
            vObjPruebaCalidad.empresa_proveedora = txtEmpresaProveedora.Text != "" ? txtEmpresaProveedora.Text.ToUpper() : null;

            vObjPruebaCalidad.tanque_externo = txtTanqueOrigenExterno.Text != "" ? txtTanqueOrigenExterno.Text.ToUpper() : null;
            vObjPruebaCalidad.nro_lote_Verif = txtNroLoteVerf.Text != "" ? txtNroLoteVerf.Text.ToUpper() : null;
            if (Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]) == 0)
            {
                decimal idOrganigramaMod = Convert.ToDecimal(Session["idOrganigramaGet"]);

                if (idOrganigramaMod == DireccionesAnh.svc_obtener_idImportadores)
                {
                    vObjPruebaCalidad.nombre_producto = txtNombreProducto.Text;
                }
                else if (idOrganigramaMod == DireccionesAnh.svc_obtener_idRefinacion)
                {
                    vObjPruebaCalidad.nombre_producto = cmbNombreProducto.Text;
                }
                else if (idOrganigramaMod == DireccionesAnh.svc_obtener_idMayorista)
                {
                    vObjPruebaCalidad.nombre_producto = cmbNombreProducto.Text;
                }
            }
            else
            {
                if (!txtNombreProducto.Text.IsNullOrEmpty())
                {
                    vObjPruebaCalidad.nombre_producto = txtNombreProducto.Text;
                }
                else if (!cmbNombreProducto.Text.IsNullOrEmpty())
                {
                    vObjPruebaCalidad.nombre_producto = cmbNombreProducto.Text;
                }
            }

            if (TextBoxPrecio.Text != "")
                vObjPruebaCalidad.precio = FormatHelper.ToDecimal(TextBoxPrecio.Text);


            if (TextBoxVolumenMuestra.Text != "")
                vObjPruebaCalidad.volumen_muestra = FormatHelper.ToDecimal(TextBoxVolumenMuestra.Text);


            vObjPruebaCalidad.id_medio_transporte = 0;
            vObjPruebaCalidad.id_registro_padre = 0;
            vObjPruebaCalidad.id_tipo_muestra = 0;
            //vObjPruebaCalidad.fecha_muestra = 0;

            vObjPruebaCalidad.id_punto_custodio = Convert.ToInt32(cmbTag.Value);
            vObjPruebaCalidad.id_actividad = Convert.ToInt32(Session[CVariablesSesion.IdTipoActividad]);

            if (cmbTag.Value != null && Convert.ToString(Session[CVariablesSesion.TipoActividad]) != "IMPORTACION DE ACEITES Y/O LUBRICANTES")
            {
                var vPuntoCustodio =
                    ((List<O_PUNTO_CUSTODIO_CTY>)Session[CParametrosCalidad.cColListadoPutoCustodio]).Where(x => x.ID_PUNTO_CUSTODIO == Convert.ToDecimal(cmbTag.Value)).FirstOrDefault();
                if (vPuntoCustodio != null)
                    vObjPruebaCalidad.punto_custodio = vPuntoCustodio.PUNTO_CUSTODIO;
            }

            vObjPruebaCalidad.procedencia = "APP_REST_01";
            vObjPruebaCalidad.observaciones = "";
            vObjPruebaCalidad.pruebas_calidad = vColObjetoCalidad;
            vObjPruebaCalidad.valida_cerrado = Convert.ToInt16(ValidaCalidad.Text);
            if (!txtResolucion.Text.IsNullOrEmpty())
                vObjPruebaCalidad.observaciones = txtResolucion.Text.ToUpper();
            if (Session[CParametrosCalidad.cValidacionCalidad] != null && txtFechaRA.Visible == true && ((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "FECHA_RA") != null)
            {
                vObjPruebaCalidad.fecha_ra = Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", Convert.ToDateTime(txtFechaRA.Text)));                   
            }
            else
            {
                vObjPruebaCalidad.fecha_ra = 0;
            }            
            string vObjJasonEncabezado = null;
            O_REF_REG_REPORTE_PLANO_CTY objResultado = null;


            vObjJasonEncabezado = JsonConvert.SerializeObject(vObjPruebaCalidad);
            //try
            //{
            if (TipoDeRegistro == 1)
            {

              
                var vObjRegistraCalidadCarburantes = new RegistraCalidadCarburantes();
                vObjRegistraCalidadCarburantes.strEncabezado = vObjJasonEncabezado;
                vObjRegistraCalidadCarburantes.strLlave = Llave;
                vObjRegistraCalidadCarburantes.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
                vObjRegistraCalidadCarburantes.decAppFechaRegistro = Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", Convert.ToDateTime(DateTime.Now)));
                cite.Visible = false;
                objResultado = clienteJson.Post<O_REF_REG_REPORTE_PLANO_CTY>("/RegistraPruebasCalidad/?format=json", vObjRegistraCalidadCarburantes);
            }
            else
            {
                var vObjRegistraCalidadCarburantes = new ActualizaPruebasCalidad();
                vObjRegistraCalidadCarburantes.strEncabezado = vObjJasonEncabezado;
                vObjRegistraCalidadCarburantes.strLlave = Llave;
                vObjRegistraCalidadCarburantes.decAppIdUsuario =
                    Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
                vObjRegistraCalidadCarburantes.decAppFechaRegistro = Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", Convert.ToDateTime(DateTime.Now)));
                vObjRegistraCalidadCarburantes.strCiteGenerado = TextBoxNumeroCite.Text;
                objResultado = clienteJson.Post<O_REF_REG_REPORTE_PLANO_CTY>("/ActualizaPruebasCalidad/?format=json", vObjRegistraCalidadCarburantes);

                if (objResultado.MENSAJE_ERROR == "OK" && objResultado.RESULTADO == 0)
                {
                    if (pnlAdjuntaArchivo.Visible == true)
                    {
                        guardarDocumentoAjdunto();
                    }
                }
            }
            if (objResultado.MENSAJE_ERROR == "OK" && objResultado.RESULTADO == 0)
            {
                cite.Visible = true;
                TextBoxNumeroCite.Visible = true;
                TextBoxNumeroCite.Text = objResultado.CORRELATIVO_REGISTRO;
                alert.Visible = false;
                LimpiaVariablesSession();

                #region Registra alerta

                if (AlmacenamientoAlerta.Text == "1" && LabelTipoAlerta != null &&
                    LabelIdPruebaCalidad != null)
                {
                    var vObjRegistraAlertaCalidad = new RegistraAlertaCalidad();
                    vObjRegistraAlertaCalidad.strLlave = Llave;
                    vObjRegistraAlertaCalidad.decIdTipoAlerta = Convert.ToDecimal(LabelTipoAlerta.Text);
                    vObjRegistraAlertaCalidad.decIdPruebaCalidad =
                        Convert.ToDecimal(LabelIdPruebaCalidad.Text);
                    vObjRegistraAlertaCalidad.strCiteGenerado = objResultado.CORRELATIVO_REGISTRO;
                    vObjRegistraAlertaCalidad.strObservaciones = "";
                    vObjRegistraAlertaCalidad.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
                    vObjRegistraAlertaCalidad.decAppFechaRegistro = Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", DateTime.Now));
                    clienteJson.Post<AnhPresentacionDTEP.Entidades.O_RESULTADO_CTY>("/RegistraAlertaCalidad/?format=json", vObjRegistraAlertaCalidad);
                }

                /***************************************************************************************************************************/
             //   byte[] pdfBytes = upArchivo.FileBytes; 

              /**condicion*/
                if (TipoDeRegistro == 1 && Session[CVariablesSesion.DocumentoDig] != null && upArchivo.Visible==true)
                    {

                        // pnlAdjuntaArchivo.Visible = true;
                    
                        SvcRegistroDtep.RegistraDocumento vObjRegistraDoc = new SvcRegistroDtep.RegistraDocumento();

                        vObjRegistraDoc.strLlave = cParametrosHydro.strCredencial;
                        vObjRegistraDoc.decIdTipRespaldo = 1; //Obs.
                        vObjRegistraDoc.strCite = TextBoxNumeroCite.Text;
                        vObjRegistraDoc.byteDocumento = (byte[])Session[CVariablesSesion.DocumentoDig];
                        vObjRegistraDoc.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
                        vObjRegistraDoc.decAppFechaRegistro = Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", Convert.ToDateTime(DateTime.Now)));
                        vObjRegistraDoc.strObservacion = txtObservacion.Text.ToUpper();    
                        //vObjRegistraDoc.strObservacion = txtObservacion.Visible==false? tkblistaObs.Text : txtObservacion.Text;

                        var lstResultado = clienteJson.Post<List<O_RESULTADO_CTY>>("/RegistraDocumento/?format=json", vObjRegistraDoc);
                    }
                

               
               /* if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
                {
                    throw new Exception(lstResultado == null ? "No se registró el producto comercial" : lstResultado[0].MENSAJE_ERROR);
                }
                else
                {
                    //alert.Visible = true;
                    lblMsj.Text = "Se registro el documento exitosamente.<br>";
                    lblMsj.Visible = true;
                    pnlDocumento.Visible = false;
                }

                if (lstResultado[0].ID_TABLA != 0)
                {
                    Response.Redirect("~/Sitio/VolumenesCalidad/GestionAlertas/wfVeDocumento.aspx?idDocumento=" + lstResultado[0].ID_TABLA + "&idTipoRespaldo=" + 1, false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    return;
                }*/
                /***************************************************************************************************************************/

              //  LimpiarFormularioRegistroCalidadActual();
                limpiar();
                
                #endregion

                GridSelectorChanged(
                    new GuardarRegistroCommandEventArgs(objResultado.CORRELATIVO_REGISTRO));
            }
            else
            {
                alert.Visible = true;
                idmsjError.Text = objResultado.MENSAJE_ERROR;
                idmsjError.Visible = true;
            }
            #endregion
        }
        /// <summary>
        /// Configura para el cargado de adjuntos.
        /// </summary>
        private void CargarOpcionAdjuntar()
        {
            if (TipoDeRegistro == 2 || TipoDeRegistro == 3)
            {
                try
                {
                    var validaciones = Session[CParametrosCalidad.cValidacionCalidad] as List<O_VALIDA_CALIDAD_CTY>;

                    if (validaciones != null && validaciones.Any())
                    {
                        bool tieneSinObs = validaciones.Any(x => x.VALIDA_CALIDAD == "SIN_OBS");
                        bool tieneAdjuntaDocumento = validaciones.Any(x => x.VALIDA_CALIDAD == "ADJUNTA_DOCUMENTO_CTR");

                        pnlAdjuntaArchivo.Visible = tieneSinObs && tieneAdjuntaDocumento;
                    }
                    else
                    {
                        pnlAdjuntaArchivo.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }
        /// <summary>
        /// Inicializa valores de las sesiones.
        /// </summary>
        protected void LimpiaVariablesSession()
        {
            Session[CParametrosCalidad.cColListarPruebasCalidadTablaEspecificacion] = null;
            Session[CParametrosCalidad.cObjValoresCargarGrilla] = null;
        }
        /// <summary>
        /// Verifica la expresión regular de una cadena.
        /// </summary>
        /// <param name="pCadenaVerificar">Cadena a verificar</param>
        /// <param name="pExpesionRegular">Patron</param>
        /// <returns></returns>
        private bool VerificaExpresionRegular(string pCadenaVerificar, string pExpesionRegular)
        {
            var vRegex = new Regex(pExpesionRegular);
            var vMatch = vRegex.Match(pCadenaVerificar);
            return vMatch.Success;
        }
        /// <summary>
        /// Verifica que un numero o valor reportado se encuentre en el rango del minimo y maximo.
        /// </summary>
        /// <param name="decValorReportado">valor a ser verificado</param>
        /// <param name="strEspecificacionMinima">Especificacion minima</param>
        /// <param name="strEspecificacionMaxima">Especifiación maxima</param>
        /// <returns></returns>
        private bool VerificarValorNumerico(decimal decValorReportado, string strEspecificacionMinima, string strEspecificacionMaxima)
        {
            bool resp;

            decimal decEspecificacionMinima = !strEspecificacionMinima.IsNullOrEmpty() ? FormatHelper.ToDecimal(strEspecificacionMinima) : decimal.MinValue;
            decimal decEspecificacionMaxima = !strEspecificacionMaxima.IsNullOrEmpty() ? FormatHelper.ToDecimal(strEspecificacionMaxima) : decimal.MaxValue;

            if (decValorReportado >= decEspecificacionMinima && decValorReportado <= decEspecificacionMaxima)
            {
                resp = true;
            }
            else
            {
                resp = false;
            }
            return resp;
        }
        /// <summary>
        /// Carga los datos en la grilla primcipal del control.
        /// </summary>
        /// <param name="pTablaEspecifica">Tabla especifica</param>
        /// <param name="pProducto">Producto</param>
        /// <param name="pCarburanteLubricantes">Tipo (CARBURANTO O LUBRICANTE)</param>
        /// <param name="pIdProducto">Identificador del producto</param>        
        private void CargaGrilla(string pTablaEspecifica, string pProducto, string pCarburanteLubricantes, decimal pIdProducto)
        {
            List<E_TABLA_ESPECIFICA> vColFormularioEspecifico = null;
            Visible = true;

            LimpiaVariablesSession();
            Session[CParametrosCalidad.cColPruebasCalidadTablaEspecificacion] = null;
            alert.Visible = false;
            idmsjError.Text = "";
            idmsjErrorNumerico.Text = "";
            idmsMensajeMaxMin.Text = "";
            pnlTanquePrecinto.Visible = false;
            pnlEntidadesRelacionados.Visible = false;
            pnlProductosBase.Visible = false;

            LabelTag.Text = "TAG:";
            LabelFo.Text = "Fecha de Muestreo:";
            Label1.Text = "Volumen del Lote:";
            Label3.Text = "Archivo en formato PDF:";
            LabelLote.Text = "Nº Lote:";

            try
            {
                if (IdUsuario == 0)
                    IdUsuario = Convert.ToDecimal(IdUsuario);
                if (TipoDeRegistro == 2 || TipoDeRegistro == 3)
                {
                    cite.Visible = true;
                    TextBoxNumeroCite.Visible = true;
                    TextBoxNumeroCite.Text = (NumeroCorrelativoReporte.Replace("%20", " ")).Replace("_", "/");
                    try
                    {
                        vColReporteCalidad = clienteJson.Get<List<O_REPORTE_CALIDAD>>("/ReportarCalidad/" + Llave + "/" + NumeroCorrelativoReporte + "?format=json");
                        LabelEntidad.Text = Convert.ToString(vColReporteCalidad[0].ID_ENTIDAD);
                        Session[CVariablesSesion.IdEntidad] = Convert.ToDecimal(vColReporteCalidad[0].ID_ENTIDAD);
                        Session[CVariablesSesion.IdTipoActividad] = Convert.ToDecimal(vColReporteCalidad[0].ID_TIPO_ACTIVIDAD);
                        Session[CVariablesSesion.TipoActividad] = Convert.ToString(vColReporteCalidad[0].TIPO_ACTIVIDAD);
                        pCarburanteLubricantes = vColReporteCalidad[0].PRODUCTO_PADRE;
                        var FECHA_REG = vColReporteCalidad[0].APP_FECHA_REGISTRO;

                        var fecharegistro = DateTime.Parse(FECHA_REG.ToString());
                        var timeSpan = DateTime.Now - fecharegistro;
                        var objPerfil = _clientAdminHydro.Get<EResultadoLista>("/EReporteUsuariosPerfiles/" + CParametrosHydro.strCredencialHydroAdmin + "/" + Convert.ToDecimal(CAppSettings.idAplicacion) + "/" + IdUsuario + "?format=json");
                        if (objPerfil.decCodigo == 1)
                        {
                            var objPerfilAdm = objPerfil.oResultado.Where(x => x.NOMBRE_PERFIL.Contains("ADM")).ToList().Count();

                            if (timeSpan.TotalHours > 24 && objPerfilAdm < 0)
                            {
                                btnGuardarRegistroCalidad.Enabled = false;
                                btnGuardarRegistroCalidad.BackColor = Color.Red;

                                btnGuardarRegistroCalidad.Text = "NO PUEDE REALIZAR MODIFICACIONES";
                            }
                            else
                            {
                                btnGuardarRegistroCalidad.Enabled = true;
                                lblAlertaVolumenCero.Text = "";
                                btnGuardarRegistroCalidad.BackColor = Color.Green;
                                btnGuardarRegistroCalidad.Text = "REGISTRAR.";

                            }
                        }
                        else
                        {
                            //A Implementar
                        }
                    }
                    catch (Exception ex)
                    {
                        alert.Visible = true;
                        idmsjError.Text = ex.Message == "Not Found" ? "EL SERVICIO WEB NO SE ENCUENTRA DISPONIBLE." : ex.Message;
                        return;
                    }
                    if (vColReporteCalidad != null)
                    {
                        pTablaEspecifica = Convert.ToString(vColReporteCalidad[0].ID_TABLA_ESPEC);
                        pProducto = vColReporteCalidad[0].PRODUCTO;
                        pIdProducto = Convert.ToDecimal(vColReporteCalidad[0].ID_PRODUCTO);
                        Session[CParametrosCalidad.cValoresMenu] = new string[] { Convert.ToString(vColReporteCalidad[0].ID_PRODUCTO), pTablaEspecifica };

                    }
                }
                if (pTablaEspecifica != "")
                {
                    List<O_UM_CANT_CAL_CTY> vColUnidadesMedida = null;
                    try
                    {
                        decimal vFechaActual;
                        //decimal vFechaImportacion;
                        if (txtFechaInicial.Text == "")
                        {
                            vFechaActual = Convert.ToDecimal(DateTime.Now.ToString("yyyyMMddHHmmss"));
                            txtFechaInicial.Date = DateTime.Today;
                        }
                        else
                            vFechaActual = Convert.ToDecimal(Convert.ToDateTime(txtFechaInicial.Text).ToString("yyyyMMddHHmmss"));

                        //vFechaImportacion = Convert.ToDecimal(Convert.ToDateTime(txtFechaMuIm.Text).ToString("yyyyMMddHHmmss"));

                        decimal gg = Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]);

                        //vColFormularioEspecifico = TipoDeRegistro == 1 ? clienteJson.Get<List<E_TABLA_ESPECIFICA>>("/ListarPruebasCalidadTablaEspecificacion/" + Llave + "/" + pTablaEspecifica + "/" + Session[CVariablesSesion.IdEntidad] + "/" + vFechaActual + "/0?format=json") :
                        //    clienteJson.Get<List<E_TABLA_ESPECIFICA>>("/ListarPruebasCalidadTablaEspecificacion/" + Llave + "/" + pTablaEspecifica + "/" + Convert.ToInt16(Session[CVariablesSesion.IdEntidad]) + "/" + Convert.ToDateTime(vColReporteCalidad[0].FECHA_OPERACION).ToString("yyyyMMddHHmmss") + "/" + NumeroCorrelativoReporte + "?format=json");
                        //sin arbol
                        vColFormularioEspecifico = TipoDeRegistro == 1 ?
                            clienteJson.Get<List<E_TABLA_ESPECIFICA>>("/ListarPruebasCalidadTablaEspecificacion/" + Llave + "/" + pTablaEspecifica + "/" + Session[CVariablesSesion.IdEntidad] + "/" + vFechaActual + "/0?format=json") :
                            clienteJson.Get<List<E_TABLA_ESPECIFICA>>("/ListarPruebasCalidadTablaEspecificacion/" + Llave + "/" + pTablaEspecifica + "/" + Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]) + "/" + Convert.ToDateTime(vColReporteCalidad[0].FECHA_OPERACION).ToString("yyyyMMddHHmmss") + "/" + NumeroCorrelativoReporte + "?format=json");
                        //con arbol
                        /*vColFormularioEspecifico = TipoDeRegistro == 1 ? clienteJson.Get<List<E_TABLA_ESPECIFICA>>("/ListarPruebasCalidadTablaEspecificacion/" + Llave + "/" + pTablaEspecifica + "/" + IdEntidad + "/" + vFechaActual + "/0?format=json") :
                            clienteJson.Get<List<E_TABLA_ESPECIFICA>>("/ListarPruebasCalidadTablaEspecificacion/" + Llave + "/" + pTablaEspecifica + "/" + IdEntidad + "/" + Convert.ToDateTime(vColReporteCalidad[0].FECHA_OPERACION).ToString("yyyyMMddHHmmss") + "/" + NumeroCorrelativoReporte + "?format=json");
                        */
                        Session[CParametrosCalidad.cColListarPruebasCalidadTablaEspecificacion] = vColFormularioEspecifico;
                        string mensajehydro = "";

                        if (Session[CParametrosCalidad.cColListadoActividades] == null && TipoDeRegistro == 1)
                            Session[CParametrosCalidad.cColListadoActividades] = _servicioHydroListado.ListadoActividades(Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]), CParametrosHydro.strCredencialHydroAdmin, ref mensajehydro);
                        else if (TipoDeRegistro != 1)
                            Session[CParametrosCalidad.cColListadoActividades] = _servicioHydroListado.ListadoActividades(Convert.ToDecimal(LabelEntidad.Text), CParametrosHydro.strCredencialHydroAdmin, ref mensajehydro);

                        Session[CParametrosCalidad.cValidacionCalidad] = null; //alc

                        if (Session[CParametrosCalidad.cValidacionCalidad] == null && Session[CParametrosCalidad.cColListadoActividades] != null)
                            Session[CParametrosCalidad.cValidacionCalidad] = clienteJson.Get<List<O_VALIDA_CALIDAD_CTY>>("/ObtenerValidacionCalidad/" + Llave + "/" + Convert.ToDecimal(Session[CVariablesSesion.IdTipoActividad])/*((List<O_ACTIVIDAD_CTY>)Session[CParametrosCalidad.cColListadoActividades])[0].TIPO_ACTIVIDAD_ID*/ + "/" + 0 + "?format=json");

                        List<O_PUNTO_CUSTODIO_CTY> PTC = new List<O_PUNTO_CUSTODIO_CTY>();
                        if (Session[CParametrosCalidad.cColListadoPutoCustodio] != null)
                        {
                            PTC = (List<O_PUNTO_CUSTODIO_CTY>)Session[CParametrosCalidad.cColListadoPutoCustodio];
                        }


                        if (PTC.Count == 0 && TipoDeRegistro == 1 && (Session[CParametrosCalidad.cValidacionCalidad] != null && ((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "PTC") != null))
                            Session[CParametrosCalidad.cColListadoPutoCustodio] = clienteInfraestructura.Get<List<O_PUNTO_CUSTODIO_CTY>>("/ListadoPuntoCustodioOctCal/" + CParametrosHydro.StrCredencialInfraestructura + "/" + Session[CVariablesSesion.IdEntidad] + "/" + Convert.ToDecimal(Session[CVariablesSesion.IdTipoActividad])/*((List<O_ACTIVIDAD_CTY>)Session[CParametrosCalidad.cColListadoActividades])[0].TIPO_ACTIVIDAD_ID */+ "?format=json");
                        else if (TipoDeRegistro != 1)
                            Session[CParametrosCalidad.cColListadoPutoCustodio] = clienteInfraestructura.Get<List<O_PUNTO_CUSTODIO_CTY>>("/ListadoPuntoCustodioOctCal/" + CParametrosHydro.StrCredencialInfraestructura + "/" + Session[CVariablesSesion.IdEntidad] + "/" + Convert.ToDecimal(Session[CVariablesSesion.IdTipoActividad])/*((List<O_ACTIVIDAD_CTY>)Session[CParametrosCalidad.cColListadoActividades])[0].TIPO_ACTIVIDAD_ID*/ + "?format=json");

                        if (Session[CParametrosCalidad.cValidacionCalidad] != null)
                        {
                            bool precio;
                            bool volumen_muestra;
                            bool resolucion;

                            if (((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "PRECIO") != null)
                            {
                                LabelPrecio.Visible = true;
                                TextBoxPrecio.Visible = true;
                                precio = true;
                                TextBoxPrecioL.Enabled = true;
                                TextBoxPrecioT.Enabled = true;
                                cmbMoneda.Visible = true;
                                if (vColReporteCalidad != null && vColReporteCalidad[0].IMPORTE != null)
                                    TextBoxPrecio.Text = Convert.ToString(vColReporteCalidad[0].IMPORTE);

                                #region Cargado del combo de la moneda

                                if (Session["ColListaMoneda"] == null)
                                    Session["ColListaMoneda"] = clienteJson.Get<List<O_LISTADO_MONEDA_CTY>>("ListarMonedas/" + Llave + "?format=json");

                                cmbMoneda.DataSource = (List<O_LISTADO_MONEDA_CTY>)Session["ColListaMoneda"];
                                cmbMoneda.TextField = "CODIGO";
                                cmbMoneda.ValueField = "ID_MONEDA";
                                cmbMoneda.DataBind();

                                #endregion
                            }
                            else
                            {
                                LabelPrecio.Visible = false;
                                TextBoxPrecio.Visible = false;
                                precio = false;
                                cmbMoneda.Visible = false;
                            }
                            if (Session[CParametrosCalidad.cValidacionCalidad] != null && ((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "VOLUMEN_MUESTRA") != null)
                            {
                                LabelVolumenMuestra.Visible = true;
                                TextBoxVolumenMuestra.Visible = true;
                                volumen_muestra = true;
                                LabelVolumenMuestraL.Enabled = true;
                                LabelVolumenMuestraT.Enabled = true;
                                if (vColReporteCalidad != null && vColReporteCalidad[0].VOLUMEN_OP_DEBE_MUESTRA != null)
                                    TextBoxVolumenMuestra.Text = Convert.ToString(vColReporteCalidad[0].VOLUMEN_OP_DEBE_MUESTRA);
                            }
                            else
                            {
                                LabelVolumenMuestra.Visible = false;
                                TextBoxVolumenMuestra.Visible = false;
                                volumen_muestra = false;
                            }
                            if (Session[CParametrosCalidad.cValidacionCalidad] != null && ((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "RESOLUCION") != null)
                            {
                                lblResolucion.Visible = true;
                                lblResolucion.Text = "N° Resolucion de Autorizacion:";
                                txtResolucion.Visible = true;
                                resolucion = true;
                                if (vColReporteCalidad != null && vColReporteCalidad[0].OBSERVACIONES != null && vColReporteCalidad[0].OBSERVACIONES != "SIN_OBSERVACION")
                                    txtResolucion.Text = Convert.ToString(vColReporteCalidad[0].OBSERVACIONES);
                            }
                            else
                            {
                                lblResolucion.Text = "Resolución:";
                                txtResolucion.Visible = false;
                                resolucion = false;
                            }

                            #region Implementado en fecha 19/09/2023 PSLs

                            if (Session[CParametrosCalidad.cValidacionCalidad] != null && ((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "TIPO_OPERACION") != null)
                            {
                                lblTipoOperacion.Visible = true;
                                cmbTipoOperacion.Visible = true;

                                var objTipoOperacion = client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
                                new
                                {
                                    decFiltro = 4,
                                    strParametro1 = Session[CVariablesSesion.IdTipoActividad].ToString(),
                                    strParametro2 = "",
                                    strParametro3 = ""
                                });
                                if (objTipoOperacion.IntCodigo == 1)
                                {
                                    var lstTipoOperacion = objTipoOperacion.OResultado;
                                    cmbTipoOperacion.DataSource = lstTipoOperacion;
                                    cmbTipoOperacion.TextField = "NOMBRE";
                                    cmbTipoOperacion.ValueField = "ID_TIPO_OPERACION";
                                    cmbTipoOperacion.DataBind();
                                    cmbTipoOperacion.SelectedIndex = objTipoOperacion.OResultado.IndexOf(objTipoOperacion.OResultado.FirstOrDefault(w => w.NOMBRE.Contains("INTERNO")));

                                }
                                else
                                {
                                    cmbTipoOperacion.DataSource = null;
                                    cmbTipoOperacion.DataBind();
                                }
                            }
                            else
                            {
                                lblTipoOperacion.Visible = false;
                                cmbTipoOperacion.Visible = false;
                            }

                            #endregion

                            #region Implementado en fecha 30/04/2026 Importacion de carburantes

                            if (Session[CParametrosCalidad.cValidacionCalidad] != null && ((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "ENTIDADES_RELACIONADOS") != null)
                            {
                                lblEmpresaProveedora.Visible = true;
                                txtEmpresaProveedora.Visible = true;
                                lblModalidadTransp.Visible = true;
                                cmbModalidadTransporte.Visible = true;
                                var modalidades = new List<object>
                                {
                                    new { COD_TRANSP = "DUCTO" },
                                    new { COD_TRANSP = "CISTERNA" },
                                    new { COD_TRANSP = "BARCAZA" },
                                    new { COD_TRANSP = "FERROCARRIL" },
                                    new { COD_TRANSP = "OTROS MEDIOS DE TRANSPORTE" },
                                    new { COD_TRANSP = "DUCTOS DE OPERACION INTERNA" },
                                    new { COD_TRANSP = "TIPO MEDIO DE TRANSPORTE NO IDENTIFICADO" }
                                };

                                cmbModalidadTransporte.DataSource = modalidades;
                                cmbModalidadTransporte.DataBind();

                                pnlEntidadesRelacionados.Visible = true;
                                var objEntidades = client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
                                    new
                                    {
                                        strCredencial = CParametrosHydro.strCredencialFuncionarioOctVol,
                                        decFiltro = 10,
                                        strParametro1 = "",
                                        strParametro2 = Session[CVariablesSesion.IdTipoActividad].ToString(),
                                        strParametro3 = ""
                                    });
                                if (objEntidades.IntCodigo == 1)
                                {
                                    cmbEnitdadDestino.DataSource = objEntidades.OResultado;
                                    cmbEnitdadDestino.TextField = "NOMBRE";
                                    cmbEnitdadDestino.ValueField = "ID_IDENTIFICADOR";
                                    cmbEnitdadDestino.DataBind();
                                    if (vColReporteCalidad != null)
                                    {
                                        cmbEnitdadDestino.SelectedIndex = objEntidades.OResultado.IndexOf(objEntidades.OResultado.FirstOrDefault(w => w.ID_IDENTIFICADOR == Convert.ToDecimal(vColReporteCalidad[0].ID_ENTIDAD_DESTINO)));
                                    }
                                }
                                if (vColReporteCalidad != null)
                                {
                                    cmbModalidadTransporte.Value = cmbModalidadTransporte.Visible == true ? vColReporteCalidad[0].NOMBRE_PRODUCTO.ToString() : "";
                                    txtEmpresaProveedora.Text = vColReporteCalidad[0].EMPRESA_PROVEEDORA.ToString();
                                    txtRutaInternacion.Text = vColReporteCalidad[0].RUTA_INTERNACION.ToString();
                                }
                            }
                            else
                            {
                                pnlEntidadesRelacionados.Visible = false;
                            }

                            #endregion

                            if (precio == true || volumen_muestra == true)
                            { divDinamic.Visible = true; }
                            else { divDinamic.Visible = false; }

                            divDinamicImp.Visible = resolucion == true;
                        }
                        else
                        {
                            LabelPrecio.Visible = false;
                            TextBoxPrecio.Visible = false;
                            divDinamic.Visible = false;
                            divDinamicImp.Visible = false;
                            divDinamicNomProd.Visible = false;
                            cmbMoneda.Visible = false;
                            LabelVolumenMuestra.Visible = false;
                            TextBoxVolumenMuestra.Visible = false;
                        }

                        var idEnt = Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]);
                        if (idEnt == 11 || idEnt == 12 || idEnt == 19969)
                        {
                            upArchivo.Visible = false;
                            Label3.Visible = false;
                        }
                        else
                        {
                            upArchivo.Visible = true;
                            Label3.Visible = true;
                        }


                        if (pCarburanteLubricantes != "CARBURANTES" && (pCarburanteLubricantes != null || pCarburanteLubricantes == "LUBRICANTES"))
                        {
                            decimal decIdDireccion = Convert.ToDecimal(Session[CVariablesSesion.IdTipoActividad]); //  ObtenerIdDireccion();



                            if (Session[CParametrosCalidad.cValidacionCalidad] != null &&
                                ((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "MARCA_PRODUCTO") != null)
                            {
                                if (Session[CParametrosCalidad.cColListadoMarcaLubricantes] == null)
                                {
                                    var objResultado =
                                        clienteJson.Get<List<O_LISTADO_MARCA_LUB_CTY>>("ListarMarcasLubricantes/" +
                                                                                       cParametrosHydro.strCredencial +
                                                                                       "?format=json");
                                    if (objResultado != null)
                                        Session[CParametrosCalidad.cColListadoMarcaLubricantes] = objResultado;
                                }
                                List<O_LISTADO_MARCA_LUB_CTY> listaMarca =
                                    (List<O_LISTADO_MARCA_LUB_CTY>)
                                        Session[CParametrosCalidad.cColListadoMarcaLubricantes];
                                cmbMarca.DataSource = listaMarca;
                                cmbMarca.TextField = "CODIGO";
                                cmbMarca.ValueField = "ID_MARCA_PRODUCTO";
                                cmbMarca.DataBind();
                                if (vColReporteCalidad != null)
                                {
                                    cmbMarca.SelectedIndex =
                                        listaMarca.IndexOf(
                                            listaMarca.FirstOrDefault(
                                                w =>
                                                w.ID_MARCA_PRODUCTO
                                                == Convert.ToDecimal(vColReporteCalidad[0].ID_MARCA_PRODUCTO)));
                                }
                                LabelMarca.Visible = true;
                                cmbMarca.Visible = true;
                            }
                            else
                            {
                                LabelMarca.Visible = false;
                                cmbMarca.Visible = false;
                            }

                            if (Session[CParametrosCalidad.cValidacionCalidad] != null && ((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "NOMBRE_PRODUCTO") != null)
                            {
                                decimal idOrganigrama = vColReporteCalidad != null ? Convert.ToDecimal(vColReporteCalidad[0].ID_ORGANIGRAMA) : 0;
                                Session["idOrganigramaGet"] = idOrganigrama;

                                if (decIdDireccion == DireccionesAnh.svc_obtener_idImportadores ||
                                    idOrganigrama == DireccionesAnh.svc_obtener_idImportadores)
                                {
                                    cmbNombreProducto.Visible = false;
                                    lblNombreProducto.Visible = true;
                                    txtNombreProducto.Visible = true;

                                    divDinamicNomProd.Visible = true;
                                }
                                else if (decIdDireccion == DireccionesAnh.svc_obtener_idRefinacion ||
                                         idOrganigrama == DireccionesAnh.svc_obtener_idRefinacion)
                                {
                                    //if (Session[CParametrosCalidad.cColListadoNombreProductos] == null)
                                    //{
                                    decimal idEntidad = Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]);
                                    var objResultadoP = clienteJson.Get<List<O_LISTA_NOMBRE_PRODUCTO_CTY>>("ListarNombreProductos/" + cParametrosHydro.strCredencial + "/" + idEntidad + "/" + pTablaEspecifica + "?format=json");
                                    if (objResultadoP != null)
                                        Session[CParametrosCalidad.cColListadoNombreProductos] = objResultadoP;
                                    //}
                                    cmbNombreProducto.Items.Clear();

                                    if (objResultadoP.Count > 0)
                                    {
                                        cmbNombreProducto.DataSource = (List<O_LISTA_NOMBRE_PRODUCTO_CTY>)Session[CParametrosCalidad.cColListadoNombreProductos];
                                        cmbNombreProducto.TextField = "NOMBRE_PRODUCTO";
                                        cmbNombreProducto.ValueField = "ID_NOMBRE_PRODUCTO";
                                    }
                                    else
                                    {
                                        cmbNombreProducto.DataSource = new List<O_LISTA_NOMBRE_PRODUCTO_CTY>();
                                    }
                                    cmbNombreProducto.DataBind();

                                    cmbNombreProducto.Visible = true;
                                    lblNombreProducto.Visible = true;
                                    txtNombreProducto.Visible = false;

                                    divDinamicNomProd.Visible = true;
                                }
                            }
                            else
                            {
                                cmbNombreProducto.Visible = false;
                                lblNombreProducto.Visible = false;
                                txtNombreProducto.Visible = false;

                                divDinamicNomProd.Visible = true;
                            }
                        }

                        if (ValidaCalidad.Text == "")
                        {
                            if (Session[CParametrosCalidad.cValidacionCalidad] != null && ((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "PRUEBA_CERRADA") != null)
                                ValidaCalidad.Text = "1";
                            else
                                ValidaCalidad.Text = "0";


                        }

                        if (Session[CParametrosCalidad.cColListadoUnidadesMedida] == null)
                            Session[CParametrosCalidad.cColListadoUnidadesMedida] = clienteJson.Get<List<O_UM_CANT_CAL_CTY>>("/ListarUnidadMedidaPorUsuario/" + Llave + "/" + IdUsuario + "?format=json");

                    }
                    catch (Exception ex)
                    {
                        alert.Visible = true;
                        idmsjError.Text = ex.Message == "Not Found" ? "EL SERVICIO WEB NO SE ENCUENTRA DISPONIBLE." : ex.Message;
                        return;
                    }
                    TextBoxDescripcionProducto.Text = pProducto;
                    if (TipoDeRegistro == 3)
                        btnEliminarRegistroCalidad.Visible = true;
                    else if (TipoDeRegistro == 1 || TipoDeRegistro == 2)
                        btnGuardarRegistroCalidad.Visible = true;

                    // Carga la grilla.
                    if (vColFormularioEspecifico != null)
                    {
                        grdCertificadoCalidad.DataSource = vColFormularioEspecifico;
                        grdCertificadoCalidad.DataBind();
                    }
                    //vColTanques = (List<O_PUNTO_CUSTODIO_CTY>)Session[CParametrosCalidad.cColListadoPutoCustodio];
                    //if (vColTanques != null && vColTanques.Any())
                    if (((List<O_PUNTO_CUSTODIO_CTY>)Session[CParametrosCalidad.cColListadoPutoCustodio]).Count > 0)
                    {
                        List<O_PUNTO_CUSTODIO_CTY> listaPuntos = (List<O_PUNTO_CUSTODIO_CTY>)Session[CParametrosCalidad.cColListadoPutoCustodio];
                        cmbTag.DataSource = listaPuntos;
                        cmbTag.TextField = "CODIGO";
                        cmbTag.ValueField = "ID_PUNTO_CUSTODIO";
                        cmbTag.DataBind();
                        if (vColReporteCalidad != null)
                        {
                            cmbTag.SelectedIndex =
                                listaPuntos.IndexOf(
                                    listaPuntos.FirstOrDefault(
                                        w => w.ID_PUNTO_CUSTODIO == vColReporteCalidad[1].ID_PUNTO_CUSTODIO));
                        }

                        #region Implementado en fecha 02/12/2025, NI-DB-UGIAB 0165/2025 C.B 4839796
                        List<decimal> listadoProductos = new List<decimal> { 358, 360, 361, 362, 383 };
                        List<decimal> listadoGasolinas = new List<decimal> { 358, 383 };
                        if (listadoProductos.Contains(pIdProducto) && Session[CParametrosCalidad.cValidacionCalidad] != null && ((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "PRODUCTOS_BASE") != null)
                        {
                            LabelTag.Text = "Placa Cisterna:";
                            LabelFo.Text = "Fecha de Muestreo Prod. Final:";
                            Label1.Text = "Vol. de Muestra Prod. Final:";
                            Label3.Text = "Archivo en formato PDF de Prod. Final:";
                            LabelLote.Text = "Nº Certificado:";
                            if (listadoGasolinas.Contains(pIdProducto))
                            {
                                lblBaseA.Text = "Nª Tanque GB:";
                                lblBaseB.Text = "Nª Tanque EA:";
                                txtObservacion.Text = "EESS: " + Environment.NewLine + "N° DE LOTE Y CERTIFICADO DE GB: " + Environment.NewLine + "N° DE LOTE Y CERTIFICADO DE EA: ";
                            }
                            else
                            {
                                lblBaseA.Text = "Nª Tanque DB:";
                                lblBaseB.Text = "Nª Tanque BD:";
                                txtObservacion.Text = "EESS: " + Environment.NewLine + "N° DE LOTE Y CERTIFICADO DE DB: " + Environment.NewLine + "N° DE LOTE Y CERTIFICADO DE BD: ";
                            }

                            pnlProductosBase.Visible = true;
                            cmbBaseA.DataSource = listaPuntos;
                            cmbBaseA.TextField = "CODIGO";
                            cmbBaseA.ValueField = "ID_PUNTO_CUSTODIO";
                            cmbBaseA.DataBind();

                            cmbBaseB.DataSource = listaPuntos;
                            cmbBaseB.TextField = "CODIGO";
                            cmbBaseB.ValueField = "ID_PUNTO_CUSTODIO";
                            cmbBaseB.DataBind();

                            if (vColReporteCalidad != null)
                            {
                                string valorCompleto = vColReporteCalidad[1].NOMBRE_PRODUCTO; // "5642UAO|6055DLP"                                
                                string[] valores = valorCompleto.Split('|');

                                if (valores.Length == 2)
                                {
                                    string valorTanqueA = valores[0]; // "5642UAO"
                                    cmbBaseA.SelectedIndex = listaPuntos.IndexOf(listaPuntos.FirstOrDefault(w => w.CODIGO == valorTanqueA));
                                    string valorTanqueB = valores[1]; // "6055DLP"
                                    cmbBaseB.SelectedIndex = listaPuntos.IndexOf(listaPuntos.FirstOrDefault(w => w.CODIGO == valorTanqueB));
                                }
                            }
                            //tkblistaObs.DataSource = listaPuntos;
                            //tkblistaObs.DataBind();
                        }
                        else if (Session[CParametrosCalidad.cValidacionCalidad] != null && ((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "NRO_TK_PRECINTOS") != null)
                        {
                            pnlTanquePrecinto.Visible = true;
                            lblModalidadTransp.Visible = false;
                            cmbModalidadTransporte.Visible = false;

                            lblEmpresaProveedora.Text = "Nombre de la Empresa Fabricante:";
                            LabelFo.Text = "Fecha de Muestreo:";
                            LabelLote.Text = "N° de Certificado:";
                            Label1.Text = "Vol. de Muestra:";
                            LabelTag.Text = "N° de Tanque:";
                            lbldestino.Text = "Planta Destino del Producto:";
                            //lblRutaInternacion.Text = "N° de Lote:";
                            lblRutaInternacion.Visible = false;
                            txtRutaInternacion.Visible = false;
                            //divDinamicImp.Visible = true;
                            lblNroLoteVerf.Visible = true;
                            lblNroLoteVerf.Text = "N° de Lote:";
                            txtNroLoteVerf.Visible = true;
                            if (vColReporteCalidad != null && vColReporteCalidad[0].NRO_LOTE_VERIF != null)
                                txtNroLoteVerf.Text = vColReporteCalidad[0].NRO_LOTE_VERIF.ToString();
                            if (vColReporteCalidad != null && vColReporteCalidad.Count > 1)
                            {
                                string valorCompleto = vColReporteCalidad[1].NOMBRE_PRODUCTO ?? "";

                                string[] valores = valorCompleto.Split('|');

                                txtPlacaCisterna.Text = valores.Length > 0
                                    ? valores[0]
                                    : "";

                                txtNroPrecintos.Text = valores.Length > 1
                                    ? valores[1]
                                    : "";
                            }
                            else
                            {
                                txtPlacaCisterna.Text = "";
                                txtNroPrecintos.Text = "";
                            }
                        }
                        else if (Session[CParametrosCalidad.cValidacionCalidad] != null && ((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "FECHA_RA") != null)
                        {
                            LabelFo.Text = "Fecha del Certificado:";
                            LabelLote.Text = "N° Lote del Prod. Internado:";
                            Label1.Text = "Vol. Lote del Prod. Internado:";

                            lblFechaMuIm.Visible = true;
                            txtFechaRA.Visible = true;
                            lblFechaMuIm.Text = "Vigencia de la R.A:";
                            txtFechaRA.Value = null;
                            if (vColReporteCalidad != null && vColReporteCalidad[0].OBSERVACIONES != null && vColReporteCalidad[0].OBSERVACIONES != "SIN_OBSERVACION")
                                if (vColReporteCalidad[0].FECHA_RA != null)
                                {
                                    DateTime fechaAuxiliar;
                                    string datoDeBase = vColReporteCalidad[0].FECHA_RA.ToString();
                                    if (DateTime.TryParse(datoDeBase, out fechaAuxiliar))
                                    {
                                        txtFechaRA.Date = fechaAuxiliar.Date;
                                    }
                                    else
                                    {
                                        txtFechaRA.Value = null;
                                    }
                                }
                        }
                        else if (Convert.ToInt32(Session[CVariablesSesion.IdTipoActividad]) == 5 && (TextBoxDescripcionProducto.Text.Contains("BIODIESEL") || TextBoxDescripcionProducto.Text.Contains("ETANOL ANHIDRO") || TextBoxDescripcionProducto.Text.Contains("(IyA)")))
                        {
                            LabelFo.Text = "Fecha de Muestreo:";
                            LabelLote.Text = "N° de Certificado:";
                            LabelTag.Text = "N° de Tanque:";
                            Label1.Text = "Vol. Muestra:";
                            lblTipoOperacion.Visible = false;
                            cmbTipoOperacion.Visible = false;
                        }
                        else
                        {
                            pnlTanquePrecinto.Visible = false;
                            pnlProductosBase.Visible = false;
                            LabelTag.Text = "TAG:";
                            LabelFo.Text = "Fecha de Muestreo:";
                            Label1.Text = "Volumen del Lote:";
                            Label3.Text = "Archivo en formato PDF:";
                            LabelLote.Text = "Nº Lote:";
                            txtObservacion.Text = "";
                        }

                        #endregion
                    }
                    else if (TipoDeRegistro == 1 && (Convert.ToString(Session[CVariablesSesion.TipoActividad]) != "IMPORTACION DE ACEITES Y/O LUBRICANTES")) // ((List<O_ACTIVIDAD_CTY>)Session[CParametrosCalidad.cColListadoActividades]).FirstOrDefault(x => x.TIPO_ACTIVIDAD == "IMPORTACION DE ACEITES Y/O LUBRICANTES") == null)
                    {
                        alert.Visible = true;
                        idmsjError.Text = "LA ENTIDAD NO TIENE PUNTO DE CUSTODIO.";
                    }
                    else
                    {
                        alert.Visible = false;
                    }

                    #region NRO_TK_PRECINTOS — standalone (corre incluso sin PTC)
                    if (Session[CParametrosCalidad.cValidacionCalidad] != null && ((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "NRO_TK_PRECINTOS") != null)
                    {
                        pnlTanquePrecinto.Visible = true;
                        lblModalidadTransp.Visible = false;
                        cmbModalidadTransporte.Visible = false;

                        lblEmpresaProveedora.Text = "Nombre de la Empresa Fabricante:";
                        LabelFo.Text = "Fecha de Muestreo:";
                        LabelLote.Text = "N° de Certificado:";
                        Label1.Text = "Vol. de Muestra:";
                        LabelTag.Text = "N° de Tanque:";
                        lbldestino.Text = "Planta Destino del Producto";
                        //lblRutaInternacion.Text = "N° de Lote:";
                        txtRutaInternacion.Visible = false;
                        //divDinamicImp.Visible = true;
                        lblNroLoteVerf.Visible = true;
                        txtNroLoteVerf.Visible = true;
                        if (vColReporteCalidad != null && vColReporteCalidad[0].NRO_LOTE_VERIF != null)
                            txtNroLoteVerf.Text = vColReporteCalidad[0].NRO_LOTE_VERIF.ToString();
                    }
                    #endregion

                    //if (((List<O_ACTIVIDAD_CTY>)Session[CParametrosCalidad.cColListadoActividades]).FirstOrDefault(x => x.TIPO_ACTIVIDAD == "IMPORTACION DE ACEITES Y/O LUBRICANTES") != null )
                    if (Convert.ToString(Session[CVariablesSesion.TipoActividad]) == "IMPORTACION DE ACEITES Y/O LUBRICANTES")
                    {
                        cmbTag.Visible = false;
                        LabelTag.Visible = false;
                    }
                    else
                    {
                        cmbTag.Visible = true;
                        LabelTag.Visible = true;
                    }

                    vColUnidadesMedida = (List<O_UM_CANT_CAL_CTY>)Session[CParametrosCalidad.cColListadoUnidadesMedida];
                    if (vColUnidadesMedida != null && vColUnidadesMedida.Any())
                    {
                        if (pProducto.Contains("GAS LICUADO DE PETROLEO"))
                        {
                            var pairs = clienteJson.Get<List<O_VALIDA_CALIDAD_CTY>>("/ObtenerValidacionCalidad/" + cParametrosHydro.strCredencial + "/" + ((List<O_ACTIVIDAD_CTY>)Session[CParametrosCalidad.cColListadoActividades])[0].TIPO_ACTIVIDAD_ID + "/" + 0 + "?format=json");
                            List<string> ccc = new List<string>();
                            foreach (var pair in pairs)
                            {
                                if (pair.VALIDA_CALIDAD.ToString().Contains("GLP"))
                                {
                                    string[] valores = pair.VALIDA_CALIDAD.Split(Convert.ToChar("_"));

                                    for (int i = 0; i < valores.Length; i++)
                                    {
                                        ccc.Add(valores[1]);
                                    }
                                }
                            }

                            var lll = (from l in vColUnidadesMedida where ccc.Contains(l.CODIGO) select l).ToList();
                            vColUnidadesMedida = lll.Count > 0 ? lll : vColUnidadesMedida;
                            cmbUnidadMedVol.DataSource = vColUnidadesMedida;

                        }
                        else
                        {
                            vColUnidadesMedida = vColUnidadesMedida.Where(l => !l.CODIGO.Contains("Tm")).ToList();
                            cmbUnidadMedVol.DataSource = vColUnidadesMedida;
                        }

                        cmbUnidadMedVol.TextField = "CODIGO";
                        cmbUnidadMedVol.ValueField = "ID_UNIDAD_MEDIDA";
                        cmbUnidadMedVol.DataBind();
                        if (vColReporteCalidad != null)
                        {
                            cmbUnidadMedVol.SelectedIndex = vColUnidadesMedida.IndexOf(vColUnidadesMedida.FirstOrDefault(w => w.ID_UNIDAD_MEDIDA == vColReporteCalidad[0].UNIDAD_MEDIDA_VOL));
                        }
                    }
                    else
                    {
                        alert.Visible = true;
                        idmsjError.Text = "NO SE PUDO CARGAR LAS UNIDADES DE MEDIDA.";
                    }
                }
                else
                {
                    alert.Visible = true;
                    if (vColReporteCalidad != null) idmsjErrorNumerico.Text = vColReporteCalidad[0].OBSERVACIONES;
                }

                #region Verificación — muestra campos para actividad 16 + productos específicos

                string _pProdNorm = pProducto != null ? pProducto.ToUpperInvariant()
                    .Replace('Á', 'A').Replace('É', 'E').Replace('Í', 'I').Replace('Ó', 'O').Replace('Ú', 'U')
                    .Replace('À', 'A').Replace('È', 'E').Replace('Ì', 'I').Replace('Ò', 'O').Replace('Ù', 'U')
                    .Replace('Ü', 'U').Replace('Ñ', 'N') : "";
                bool _esVrf = _pProdNorm.Contains("DIESEL OIL (VERIFICACION)") || _pProdNorm.Contains("GASOLINA ESPECIAL (VERIFICACION)");

                if (Convert.ToInt32(Session[CVariablesSesion.IdTipoActividad]) == 16)
                {
                    if (_esVrf)
                    {
                        lblFechaMuIm.Visible = true;
                        lblFechaMuIm.Text = "Fecha de toma de muestra:";
                        txtFechaMuIm.Visible = true;
                        txtFechaRA.Visible = false;
                        lblNroLoteVerf.Visible = false;
                        txtNroLoteVerf.Visible = false;
                        divDinamicImp.Visible = true;
                        lblTanqueOrigenExterno.Visible = true;
                        txtTanqueOrigenExterno.Visible = true;
                        lblRutaInternacion.Visible = true;
                        txtRutaInternacion.Visible = true;
                    }
                    else
                    {
                        lblNroLoteVerf.Visible = true;
                        lblNroLoteVerf.Text = "N° Lote de Verificacion:";
                        txtNroLoteVerf.Visible = true;
                    }
      
                    if (vColReporteCalidad != null)
                    {
                        if (vColReporteCalidad[0].FECHA_MUESTRA != null)
                            txtFechaMuIm.Date = vColReporteCalidad[0].FECHA_MUESTRA.Value.Date;
                        txtTanqueOrigenExterno.Text = vColReporteCalidad[0].TK_ORIG_EXTERNO != null ? vColReporteCalidad[0].TK_ORIG_EXTERNO.ToString() : "";
                        txtNroLoteVerf.Text = vColReporteCalidad[0].NRO_LOTE_VERIF != null ? vColReporteCalidad[0].NRO_LOTE_VERIF.ToString() : "";
                    }
                }                

                #endregion

                //if (Convert.ToDecimal(Session["conteo"]) > 2) return;
                //if (IsPostBack) return;
                /*if (count1 == 0)
                    grdCertificadoCalidad.Columns[grdCertificadoCalidad.VisibleColumns.Count()].Visible = false;*/
                //grdCertificadoCalidad.Columns["Justificacion"].Visible = false;

                if (!vTieneJustificacion)
                {
                    TextBoxJustificacion.Visible = false;
                    LabelJustificacion.Visible = false;
                    /*if (count1 == 0)
                        grdCertificadoCalidad.Columns[grdCertificadoCalidad.VisibleColumns.Count()].Visible = false;
                    else
                        grdCertificadoCalidad.Columns[grdCertificadoCalidad.VisibleColumns.Count() - 1].Visible = false;*/
                    grdCertificadoCalidad.Columns["Justificacion"].Visible = false;
                }
                else
                {
                    TextBoxJustificacion.Visible = true;
                    LabelJustificacion.Visible = true;
                    grdCertificadoCalidad.Columns["Justificacion"].Visible = true;
                }


                if (Session[CParametrosCalidad.cValidacionCalidad] != null
                    && ((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(
                        x => x.VALIDA_CALIDAD == "SIN_PARAMETRO") != null)
                {
                    grdCertificadoCalidad.Columns["EspecMinima"].Visible = false;
                    grdCertificadoCalidad.Columns["EspecMaxima"].Visible = false;
                    grdCertificadoCalidad.Columns["Justificacion"].Visible = false;
                    grdCertificadoCalidad.Columns["Especificacion"].Visible = false;
                }
                else
                {
                    //grdCertificadoCalidad.Columns[7].Visible = true;
                    //grdCertificadoCalidad.Columns[8].Visible = true;
                    //grdCertificadoCalidad.Columns[10].Visible = true;
                    //grdCertificadoCalidad.Columns[9].Visible = false;
                    grdCertificadoCalidad.Columns["EspecMinima"].Visible = true;
                    grdCertificadoCalidad.Columns["EspecMaxima"].Visible = true;
                    grdCertificadoCalidad.Columns["Justificacion"].Visible = false;
                    grdCertificadoCalidad.Columns["Especificacion"].Visible = false;
                }

                if (count == 0)
                    grdCertificadoCalidad.Columns["IsoNLGI"].Visible = false;
                //grdCertificadoCalidad.Columns[6].Visible = false;
            }
            catch (Exception e)
            {
                alert.Visible = true;
                idmsjError.Text = e.Message;
                strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CCalidadLibreria.ValidacionAdministradorHydro(e, Session[CVariablesSesion.UsuarioId], strAccion);
            }
        }
        /// <summary>
        /// Envia los documentos adjuntos la servicio correspondiente.
        /// </summary>
        public void guardarDocumentoAjdunto()
        {
            if (pnlAdjuntaArchivo.Visible == true)
            {
                if (Session[CVariablesSesion.DocumentoDig] != null)
                {
                    SvcRegistroDtep.RegistraDocumento vObjRegistraDoc = new SvcRegistroDtep.RegistraDocumento();

                    vObjRegistraDoc.strLlave = cParametrosHydro.strCredencial;
                    vObjRegistraDoc.decIdTipRespaldo = TipoDeRegistro; //2 mod, 3Elim
                    vObjRegistraDoc.strCite = TextBoxNumeroCite.Text;
                    vObjRegistraDoc.byteDocumento = (byte[])Session[CVariablesSesion.DocumentoDig];
                    vObjRegistraDoc.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
                    vObjRegistraDoc.decAppFechaRegistro = Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", Convert.ToDateTime(DateTime.Now)));
                    vObjRegistraDoc.strObservacion = txtObservacion.Text.ToUpper();
                    //vObjRegistraDoc.strObservacion = txtObservacion.Visible == false ? tkblistaObs.Text.ToUpper() : txtObservacion.Text.ToUpper();                    

                    var lstResultado = clienteJson.Post<List<AnhPresentacionDTEP.Entidades.O_RESULTADO_CTY>>("/RegistraDocumento/?format=json", vObjRegistraDoc);

                    if (lstResultado == null || lstResultado[0].ID_TABLA < 0)
                    {
                        throw new Exception(lstResultado == null ? "ERROR DOCUMENTO" : lstResultado[0].MENSAJE_ERROR);
                    }
                }
                else
                {
                    idmsjError.Text = "DEBE ADJUNTAR EL ARCHIVO";
                    idmsjError.BackColor = Color.OrangeRed;
                    idmsjError.Visible = true;
                }
            }
        }
        /// <summary>
        /// Obtiene el identificador de la dirección.
        /// </summary>
        /// <returns>Identificador</returns>
        public decimal ObtenerIdDireccion()
        {
            decimal decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);

            RetornaIdDireccion vObjRetornaIdDireccion = new RetornaIdDireccion();
            vObjRetornaIdDireccion.decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
            var clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);
            var lstDirecciones = clienteJson.Post<List<AnhPersistenciaCore.Core.O_RESULTADO_NUMBER_CTY>>("/RetornaIdDireccion/?format=json", vObjRetornaIdDireccion);

            decimal decIdDireccion;
            if (lstDirecciones.Count == 1)
            {
                decIdDireccion = lstDirecciones[0].RESULTADO;
            }
            else
            {
                decIdDireccion = 0;
            }
            return decIdDireccion;
        }

        /// <summary>
        /// Bindea los combos MetodoASTM, IsoNlgi y Unidad en una fila de la grilla.
        /// Centraliza la lógica repetida en HtmlRowCreated, CustomCallback y limpiar().
        /// </summary>
        /// <param name="index">Índice visible de la fila en la grilla.</param>
        /// <param name="colFormularioEspecifico">Lista de especificaciones del formulario.</param>
        private void BindearCombosGrilla(int index, List<E_TABLA_ESPECIFICA> colFormularioEspecifico)
        {
            ASPxComboBox vObjMetodoASTM = (ASPxComboBox)grdCertificadoCalidad.FindRowCellTemplateControl(index, null, "cmbMetodoASTM");
            ASPxComboBox vObjUnidad = (ASPxComboBox)grdCertificadoCalidad.FindRowCellTemplateControl(index, null, "cmbUnidad");
            ASPxComboBox vObjIsoNlgi = (ASPxComboBox)grdCertificadoCalidad.FindRowCellTemplateControl(index, null, "cmbIsoNlgi");

            var especifico = colFormularioEspecifico[index];

            if (especifico != null)
            {
                // MetodoASTM
                vObjMetodoASTM.DataSource = especifico.listMetodoAstm;
                vObjMetodoASTM.TextField = "strCampo";
                vObjMetodoASTM.ValueField = "intValor";
                vObjMetodoASTM.DataBind();
                if (especifico.listMetodoAstm.Count == 1)
                    vObjMetodoASTM.SelectedIndex = 0;

                // IsoNlgi
                if (vObjIsoNlgi != null)
                {
                    if (especifico.listRangosMultiples != null)
                    {
                        vObjIsoNlgi.Visible = true;
                        Session[CParametrosCalidad.cColPruebasCalidadTablaEspecificacion] = especifico;
                        vObjIsoNlgi.DataSource = especifico.listRangosMultiples;
                        vObjIsoNlgi.TextField = "strCampo";
                        vObjIsoNlgi.ValueField = "intValor";
                        vObjIsoNlgi.DataBind();
                        count++;
                        NumeroRegistro.Text = Convert.ToString(index);
                        if (especifico.listRangosMultiples.Count == 1)
                            vObjMetodoASTM.SelectedIndex = 0;
                    }
                    else
                        vObjIsoNlgi.Visible = false;
                }

                // Unidad
                vObjUnidad.DataSource = especifico.listUnidadMedida;
                vObjUnidad.TextField = "strCampo";
                vObjUnidad.ValueField = "intValor";
                vObjUnidad.DataBind();
                if (especifico.listUnidadMedida.Count == 1)
                    vObjUnidad.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Valida la condición cruzada entre Índice de Cetano (133) y Número de Cetano (134).
        /// Extraída de la validación inline en btnGuardarRegistroCalidad_Click.
        /// </summary>
        /// <returns>1 = válido, 2 = requiere atención, 3 = ambos nulos (error).</returns>
        private int ValidarIndiceCetano(decimal idPruebaCal, TextBox vObjValorReportado,
            decimal valoridPruebaAnterior, decimal valoridPruebaPosterior,
            TextBox vObjValorReportadoAnt, TextBox vObjValorReportadoPost,
            ASPxComboBox vObjMetodoASTM)
        {
            int varRes = 1;

            if (idPruebaCal == 133 && vObjValorReportado.Text.IsNullOrEmpty())
            {
                if (valoridPruebaPosterior == 134 && vObjValorReportadoPost.Text.IsNullOrEmpty())
                {
                    alert.Visible = true;
                    vObjValorReportado.BorderColor = Color.Red;
                    vObjValorReportado.BorderStyle = BorderStyle.Groove;
                    vObjValorReportadoPost.BorderColor = Color.Red;
                    vObjValorReportadoPost.BorderStyle = BorderStyle.Groove;
                    idmsjError.Text = "POR FAVOR VERIFIQUE LAS OBSERVACIONES, INGRESAR UN VALOR EN INDICE DE CETANO Y/O NUMERO CETANO";
                    varRes = 3;
                }
                else
                {
                    varRes = 2;
                }
            }
            else if (idPruebaCal == 134 && vObjValorReportado.Text.IsNullOrEmpty())
            {
                if (valoridPruebaAnterior == 133 && vObjValorReportadoAnt.Text.IsNullOrEmpty())
                {
                    if (vObjMetodoASTM.Value == null)
                    {
                        alert.Visible = true;
                        vObjMetodoASTM.BorderColor = Color.Red;
                        vObjMetodoASTM.BorderStyle = BorderStyle.Groove;
                    }
                    alert.Visible = true;
                    vObjValorReportado.BorderColor = Color.Red;
                    vObjValorReportado.BorderStyle = BorderStyle.Groove;
                    vObjValorReportadoAnt.BorderColor = Color.Red;
                    vObjValorReportadoAnt.BorderStyle = BorderStyle.Groove;
                    idmsjError.Text = "POR FAVOR VERIFIQUE LAS OBSERVACIONES, INGRESAR UN VALOR EN INDICE DE CETANO Y/O NUMERO CETANO";
                    varRes = 3;
                }
                else
                {
                    varRes = 2;
                }
            }
            else if (idPruebaCal != 133 || idPruebaCal != 134)
            {
                alert.Visible = true;
                vObjValorReportado.BorderColor = Color.Red;
                vObjValorReportado.BorderStyle = BorderStyle.Groove;
                idmsjError.Text = "POR FAVOR VERIFIQUE LAS OBSERVACIONES DE LOS VALORES REPORTADOS. ";
                varRes = 2;
            }
            else
            {
                varRes = 1;
            }

            return varRes;
        }

        /// <summary>
        /// Extrae el valor de especificación (temperatura) según la unidad °C o °F
        /// a partir de un string compuesto tipo "100°C/212°F".
        /// </summary>
        /// <param name="valor">Especificación compuesta (ej. "100°C/212°F" o "100°C").</param>
        /// <param name="unidad">Unidad deseada ("°C" o "°F").</param>
        /// <param name="esMaxima">Indica si es especificación máxima (no usado en la lógica actual,预留 para extensión).</param>
        /// <returns>El valor numérico como string para la unidad solicitada.</returns>
        private string ObtenerEspecificacionPorUnidad(string valor, string unidad, bool esMaxima)
        {
            if (string.IsNullOrEmpty(valor))
                return valor;

            string tempC = null;
            string tempF = null;

            string[] items = valor.Split('/');
            if (items.Length > 1)
            {
                tempC = items[0].Substring(0, items[0].Trim().Length - 2);
                tempF = items[1].Substring(0, items[1].Trim().Length - 2);
                return unidad == "°C" ? tempC : tempF;
            }
            else
            {
                if (valor.Contains("°C"))
                {
                    tempC = items[0].Substring(0, items[0].Trim().Length - 2);
                }
                else if (valor.Contains("°F"))
                {
                    tempF = items[0].Substring(0, items[0].Trim().Length - 2);
                }

                if (!string.IsNullOrEmpty(tempC) || !string.IsNullOrEmpty(tempF))
                {
                    return unidad == "°C" ? tempC : tempF;
                }
            }

            return valor;
        }

        #endregion

        #region Eventos
        /// <summary>
        /// Carga el valor mínimo y máximo en la grilla
        /// </summary>
        /// <param name="sender">Objeto sobre el que se ejecuta el evento</param>
        /// <param name="e">Argumento del evento</param>
        protected void gvImports_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {
            if (NumeroRegistro.Text != "")
            {
                vNumeroRegistro = Convert.ToInt16(NumeroRegistro.Text);
                if (e.DataColumn.Name == "EspecMaxima" && e.VisibleIndex == vNumeroRegistro)
                    e.Cell.Text = EspecificacionMaxima.Text;

                if (e.DataColumn.Name == "EspecMinima" && e.VisibleIndex == vNumeroRegistro)
                    e.Cell.Text = EspecificacionMinima.Text;
            }
        }
        /// <summary>
        /// actualizar los minimos y maximos de acuerdo al item seleccionado.
        /// </summary>
        /// <param name="sender">Objeto del evento</param>
        /// <param name="e">Argumento del evento</param>
        protected void VobjIsoNlgi_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (NumeroRegistro.Text != "")
            {
                vNumeroRegistro = Convert.ToInt16(NumeroRegistro.Text); ;
                ASPxComboBox vObjIsoNlgi = (ASPxComboBox)grdCertificadoCalidad.FindRowCellTemplateControl(vNumeroRegistro, null, "cmbIsoNlgi");
                var vObjFormularioEspecifico = (E_TABLA_ESPECIFICA)this.Session[CParametrosCalidad.cColPruebasCalidadTablaEspecificacion];
                for (int i = 0; i < vObjFormularioEspecifico.listRangosMultiples.Count(); i++)
                {
                    if (vObjFormularioEspecifico.listRangosMultiples[i].strCampo == vObjIsoNlgi.Text && vObjFormularioEspecifico.listRangosMultiples[i].intValor == Convert.ToDecimal(vObjIsoNlgi.Value))
                    {
                        EspecificacionMinima.Text = vObjFormularioEspecifico.listEspecMinimaRango[i].strCampo;
                        EspecificacionMaxima.Text = vObjFormularioEspecifico.listEspecMaximaRango[i].strCampo;
                        i = 99999;
                    }
                }
            }
        }
        /// <summary>
        /// Carga la valores iniciales de la pagina.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[CVariablesSesion.UsuarioId] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Session["conteo"] = Convert.ToDecimal(Session["conteo"]) + 1;
                //superadministrador
                btnVisualiza.Visible = Convert.ToBoolean(Session[CVariablesSesion.IsSuperAdministrador]);
                btnCancelarRegistroCalidad.Visible = TipoDeRegistro == 1;
                if (Convert.ToDecimal(Session["conteo"]) == 1)
                {
                    CargarParametroGrillaCalidad();
                }
               

            }
        }

        
                /// <summary>
        /// Configura los controles y plantillas incorporadas en la grilla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCertificadoCalidad_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.GridViewRowType.Data) return;

            if (Session[CVariablesSesion.UsuarioId] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                //if (!IsPostBack)
                //{
                try
                {
                    var vColFormularioEspecifico = (List<E_TABLA_ESPECIFICA>)this.Session[CParametrosCalidad.cColListarPruebasCalidadTablaEspecificacion];
                    int dato = Convert.ToInt32(grdCertificadoCalidad.GetRowValues(e.VisibleIndex, "decIdPruebaCalidad"));
                    string PruebaEnsayo = Convert.ToString(grdCertificadoCalidad.GetRowValues(e.VisibleIndex, "strDescripcion"));
                    PruebaEnsayo = !string.IsNullOrEmpty(PruebaEnsayo) ? PruebaEnsayo.Trim().ToUpper() : null;
                    if (dato == 0)
                        return;
                    object ObjEspecAlfanumerico = (grdCertificadoCalidad.GetRowValues(e.VisibleIndex, "strEspecAlfanumerico"));
                    ASPxComboBox vObjIsoNlgi = (ASPxComboBox)grdCertificadoCalidad.FindRowCellTemplateControl(e.VisibleIndex, null, "cmbIsoNlgi");
                    ASPxComboBox vObjMetodoASTM = (ASPxComboBox)grdCertificadoCalidad.FindRowCellTemplateControl(e.VisibleIndex, null, "cmbMetodoASTM");
                    ASPxComboBox vObjUnidad = (ASPxComboBox)grdCertificadoCalidad.FindRowCellTemplateControl(e.VisibleIndex, null, "cmbUnidad");

                    //TextBox alfanum = (TextBox)grdCertificadoCalidad.FindRowCellTemplateControl(e.VisibleIndex, null, "txtValorReportado");
                    //alfanum.Text = Convert.ToString(ObjEspecAlfanumerico);

                    if (PruebaEnsayo.ToUpper() == "API")
                    {
                        //ASPxTextBox vObjValorReportado = (ASPxTextBox)grdCertificadoCalidad.FindRowCellTemplateControl(e.VisibleIndex, null, "txtValorReportado");
                        //vObjValorReportado.AutoPostBack = true;


                        RegistroApi.Text = Convert.ToString(e.VisibleIndex);
                        if (vColFormularioEspecifico[e.VisibleIndex] != null)
                            LabelIdPruebaCalidad.Text = Convert.ToString(vColFormularioEspecifico[e.VisibleIndex].decIdPruebaCalidad);
                        LabelTipoAlerta.Text = "2";
                    }

                    if (PruebaEnsayo.ToUpper() == "CLASIFICACIÓN NLGI")
                    {
                        //ASPxTextBox vObjValorReportado = (ASPxTextBox)grdCertificadoCalidad.FindRowCellTemplateControl(e.VisibleIndex, null, "txtValorReportado");
                        //vObjValorReportado.AutoPostBack = true;
                        ClasificacionNLGI.Text = Convert.ToString(e.VisibleIndex);
                        if (vColFormularioEspecifico[e.VisibleIndex] != null)
                            LabelIdPruebaCalidad.Text = Convert.ToString(vColFormularioEspecifico[e.VisibleIndex].decIdPruebaCalidad);
                        LabelTipoAlerta.Text = "4";
                    }

                    BindearCombosGrilla(e.VisibleIndex, vColFormularioEspecifico);

                    if (ObjEspecAlfanumerico != null)
                        count1++;

                    vContadorModificacion++;
                    // Cargado de la información para realizar la modificación. Peter
                    //if (((TipoDeRegistro == 2 || TipoDeRegistro == 3) && vContadorModificacion == vColReporteCalidad.Count * 2) || ((TipoDeRegistro == 2 || TipoDeRegistro == 3) && vContadorModificacion == ((vColReporteCalidad.Count * 2) - 2)))

                    if (TipoDeRegistro == 2 || TipoDeRegistro == 3)
                    {
                        //ASPxTextBox vObjValorReportado = null;
                        TextBox vObjValorReportado = null;
                        if (e.VisibleIndex == 0)
                        {

                            if (vColReporteCalidad != null && vColReporteCalidad[0].ID_REGISTRO_CALIDAD != -99999)
                            {
                                int j = 0;
                                TextBoxLote.Text = vColReporteCalidad[0].VALOR_LOTE;
                                //TODO: IMPORTANTE PARA PRODUCCION (VOLUMEN)
                                /*TextBoxVolumen.Text = CParametrosHydro.idEntornoAplicacion != "PRO"
                                                          ? vColReporteCalidad[0].VOLUMEN_OP_DEBE.ToString()
                                                                .Replace(".", ",")
                                                          : vColReporteCalidad[0].VOLUMEN_OP_DEBE.ToString()
                                                                .Replace(",", ".");*/

                                TextBoxVolumen.Text = Convert.ToString(vColReporteCalidad[0].VOLUMEN_OP_DEBE);
                                txtFechaInicial.Text =
                                    Convert.ToString(
                                        (Convert.ToDateTime(vColReporteCalidad[0].FECHA_OPERACION).ToShortDateString()));
                                txtFechaMuIm.Text =
                                    Convert.ToString(
                                        (Convert.ToDateTime(vColReporteCalidad[0].FECHA_MUESTRA).ToShortDateString()));
                                decimal idOrganigrama = vColReporteCalidad != null
                                                            ? Convert.ToDecimal(vColReporteCalidad[0].ID_ORGANIGRAMA)
                                                            : 0;

                                if (idOrganigrama == DireccionesAnh.svc_obtener_idImportadores)
                                {
                                    txtNombreProducto.Text = vColReporteCalidad[0].NOMBRE_PRODUCTO;
                                }
                                else if (idOrganigrama == DireccionesAnh.svc_obtener_idRefinacion)
                                {
                                    cmbNombreProducto.Text = vColReporteCalidad[0].NOMBRE_PRODUCTO;
                                }
                            }
                            else
                            {
                                alert.Visible = true;
                                TextBoxNumeroCite.Text = vColReporteCalidad[0].CITE_DOCUMENTO;
                                TextBoxDescripcionProducto.Text = Request.QueryString["DescripcionProducto"];
                                idmsjError.Text = vColReporteCalidad[0].OBSERVACIONES;
                            }
                        }
                        //for (int i = 0; i < vColReporteCalidad.Count; i++)
                        //{
                        //if (vColReporteCalidad[i].ID_PRUEBA_CALIDAD == 0)
                        //    i++;
                        vObjUnidad = (ASPxComboBox)grdCertificadoCalidad.FindRowCellTemplateControl(e.VisibleIndex, null, "cmbUnidad");
                        vObjMetodoASTM = (ASPxComboBox)grdCertificadoCalidad.FindRowCellTemplateControl(e.VisibleIndex, null, "cmbMetodoASTM");
                        //vObjValorReportado = (ASPxTextBox)grdCertificadoCalidad.FindRowCellTemplateControl(j, null, "txtValorReportado");
                        vObjValorReportado = (TextBox)grdCertificadoCalidad.FindRowCellTemplateControl(e.VisibleIndex, null, "txtValorReportado");
                        //string vObjPrueba = Convert.ToString(grdCertificadoCalidad.GetRowValues(e.VisibleIndex, "strDescripcion"));
                        decimal decIdPruebaCalidad =
                            Convert.ToDecimal(grdCertificadoCalidad.GetRowValues(e.VisibleIndex, "decIdPruebaCalidad"));


                        var objColReporteCalidad =
                            vColReporteCalidad.FirstOrDefault(w => w.ID_PRUEBA_CALIDAD == decIdPruebaCalidad);

                        if (objColReporteCalidad != null)
                        {
                            if (vObjValorReportado != null && vObjMetodoASTM != null && vObjUnidad != null)
                            {
                                vObjValorReportado.Text = objColReporteCalidad.VALOR_ALFANUMERICO;
                                vObjMetodoASTM.Value = objColReporteCalidad.ID_METODO_ASTM;
                                vObjMetodoASTM.Text = objColReporteCalidad.CODIGO_ASTM;
                                vObjUnidad.Value = objColReporteCalidad.ID_PRUEBA_UNIDAD;
                                vObjUnidad.Text = objColReporteCalidad.CODIGO_UNIDAD;
                            }
                        }

                    }

                }

                catch (Exception ex)
                {
                    alert.Visible = true;
                    idmsjError.Text = ex.Message;
                }
                //}
            }
        }

        /// <summary>
        /// Evento que validad los datos del formulario antes de ser en viado al servicio.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardarRegistroCalidad_Click(object sender, EventArgs e)
        {

            if (Session[CVariablesSesion.UsuarioId] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                try
                {
                    //  archivo = upArchivo.FileBytes;

                    //                    string script = @"<script type='text/javascript'>
                    //                        NombreFuncionJavascript();
                    //                  </script>";

                    //                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, false);

                    cite.Visible = false;
                    alert.Visible = false;
                    decimal vValorNumerico = 0;
                    bool vResultadoVerificacion = false;
                    idmsjErrorNumerico.Text = "";
                    idmsMensajeMaxMin.Text = "";
                    idmsjError.Text = "POR FAVOR VERIFIQUE LAS OBSERVACIONES DE LOS VALORES REPORTADOS.";
                    alert.Visible = false;
                    //idmsjError.Visible = false;
                    TextBoxLote.BorderColor = Color.Silver;
                    TextBoxVolumen.BorderColor = Color.Silver;
                    txtFechaInicial.BorderColor = Color.Silver;
                    txtFechaMuIm.BorderColor = Color.Silver;
                    txtFechaRA.BorderColor = Color.Silver;
                    cmbTag.BorderColor = Color.Silver;
                    cmbUnidadMedVol.BorderColor = Color.Silver;

                    decimal esNlgi = 0;
                    #region Validaciones de datos
                    if (ValorApi.Text == "0")
                    {
                        alert.Visible = true;
                        idmsjError.Text = "EL VALOR API ES INCORRECTO.";
                    }
                    if (ValorNlgi.Text == "0")
                    {
                        alert.Visible = true;
                        idmsjError.Text = "EL VALOR NLGI ES INCORRECTO.";
                    }

                    if (txtFechaInicial.Text == "")
                    {
                        alert.Visible = true;
                        txtFechaInicial.BorderColor = Color.Red;
                        txtFechaInicial.BorderStyle = BorderStyle.Groove;
                    }
                    if (Session[CVariablesSesion.DocumentoDig] != null)
                    {
                        alert.Visible = false;
                    }
                    var idEntidad = Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]);
                    if (TipoDeRegistro == 1 && Session[CVariablesSesion.DocumentoDig] == null && upArchivo.Visible==true)
                    {

                       // pnlAdjuntaArchivo.Visible = true;
                         alert.Visible = true;
                        idmsjError.Text = "Debe seleccionar el documento a grabar.<br>";
                    }
                    

                    if (Session[CParametrosCalidad.cValidacionCalidad] != null && txtFechaMuIm.Visible==true && (((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "FECHA_MUIM") != null))
                    {
                        if (txtFechaMuIm.Text == "")
                        {
                            alert.Visible = true;
                            txtFechaMuIm.BorderColor = Color.Red;
                            txtFechaMuIm.BorderStyle = BorderStyle.Groove;
                        }
                    }
                    if (Session[CParametrosCalidad.cValidacionCalidad] != null && txtFechaRA.Visible==true &&(((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "FECHA_RA") != null))
                    {
                        if (txtFechaRA.Text == "")
                        {
                            alert.Visible = true;
                            txtFechaRA.BorderColor = Color.Red;
                            txtFechaRA.BorderStyle = BorderStyle.Groove;
                        }
                    }
                    if (Session[CParametrosCalidad.cValidacionCalidad] != null && (((List<O_VALIDA_CALIDAD_CTY>)Session[CParametrosCalidad.cValidacionCalidad]).FirstOrDefault(x => x.VALIDA_CALIDAD == "ENTIDADES_RELACIONADOS") != null))
                    {                        
                        if (cmbEnitdadDestino.Value == null && cmbEnitdadDestino.Visible)
                        {
                            alert.Visible = true;
                            cmbEnitdadDestino.BorderColor = Color.Red;
                            cmbEnitdadDestino.BorderStyle = BorderStyle.Groove;
                        }
                    }

                    if (cmbTag.Value == null && cmbTag.Visible)
                    {
                        alert.Visible = true;
                        cmbTag.BorderColor = Color.Red;
                        cmbTag.BorderStyle = BorderStyle.Groove;
                    }
                    if (cmbUnidadMedVol.Value == null)
                    {
                        alert.Visible = true;
                        cmbUnidadMedVol.BorderColor = Color.Red;
                        cmbUnidadMedVol.BorderStyle = BorderStyle.Groove;
                    }


                    ASPxComboBox vObjIsoNlgi = null;
                    ASPxComboBox vObjMetodoASTM = null;
                    ASPxComboBox vObjUnidad = null;
                    //ASPxTextBox vObjValorReportado = null;
                    TextBox vObjValorReportado = null;
                    TextBox vObjValorReportadoAnt = null;
                    TextBox vObjValorReportadoPost = null;
                    ASPxTextBox vObjJustificacion = null;
                    object vEspecificacionMinima = null;
                    object vEspecificacionMaxima = null;
                    ObjetoCalidad vObjObjetoCalidad = null;
                    List<ObjetoCalidad> vColObjetoCalidad = new List<ObjetoCalidad>();
                    for (int i = 0; i < grdCertificadoCalidad.VisibleRowCount; i++)
                    {
                        vObjObjetoCalidad = new ObjetoCalidad();
                        vObjIsoNlgi = (ASPxComboBox)grdCertificadoCalidad.FindRowCellTemplateControl(i, null, "cmbIsoNlgi");
                        vObjMetodoASTM = (ASPxComboBox)grdCertificadoCalidad.FindRowCellTemplateControl(i, null, "cmbMetodoASTM");
                        vObjUnidad = (ASPxComboBox)grdCertificadoCalidad.FindRowCellTemplateControl(i, null, "cmbUnidad");
                        vObjValorReportado = (TextBox)grdCertificadoCalidad.FindRowCellTemplateControl(i, null, "txtValorReportado");
                        vObjJustificacion = (ASPxTextBox)grdCertificadoCalidad.FindRowCellTemplateControl(i, null, "txtJustificacion");
                        decimal id_prueba_cal = Convert.ToDecimal(grdCertificadoCalidad.GetRowValues(i, "decIdPruebaCalidad"));
                        string PruebaEnsayo = Convert.ToString(grdCertificadoCalidad.GetRowValues(i, "strDescripcion"));
                        if (PruebaEnsayo.ToUpper() == "API")
                        {
                            ValidaApiNlgr(i, RegistroApi.Text, "");
                        }
                        if (PruebaEnsayo.ToUpper() == "CLASIFICACIÓN NLGI")
                        {
                            ValidaApiNlgr(i, "", ClasificacionNLGI.Text);
                        }
                        decimal valoridPruebaAnterior = 0;
                        decimal valoridPruebaPosterior = 0;
                        if (i > 0 && i < grdCertificadoCalidad.VisibleRowCount - 1) //A.L.C.
                        {
                            vObjValorReportadoAnt = (TextBox)grdCertificadoCalidad.FindRowCellTemplateControl(i - 1, null, "txtValorReportado");
                            vObjValorReportadoPost = (TextBox)grdCertificadoCalidad.FindRowCellTemplateControl(i + 1, null, "txtValorReportado");
                            valoridPruebaAnterior = Convert.ToDecimal(grdCertificadoCalidad.GetRowValues(i - 1, "decIdPruebaCalidad"));
                            valoridPruebaPosterior = Convert.ToDecimal(grdCertificadoCalidad.GetRowValues(i + 1, "decIdPruebaCalidad"));
                        }
                        if (vObjUnidad != null) // vObjMetodoASTM != null && vObjValorReportado != null)
                        {
                            vObjMetodoASTM.BorderStyle = BorderStyle.Groove;
                            vObjUnidad.BorderStyle = BorderStyle.Groove;
                            if (vObjIsoNlgi != null)
                            {
                                vObjIsoNlgi.BorderStyle = BorderStyle.Groove;
                                vObjIsoNlgi.BorderColor = Color.Silver;
                            }
                            vObjUnidad.BorderColor = Color.Silver;
                            vObjValorReportado.BorderColor = Color.Silver;//descomentado para prueba

                            vEspecificacionMinima = (grdCertificadoCalidad.GetRowValues(i, "strEspecMinima"));
                            vEspecificacionMaxima = (grdCertificadoCalidad.GetRowValues(i, "strEspecMaxima"));


                            if (vObjIsoNlgi != null && vObjIsoNlgi.Value != null)
                            {
                                vObjObjetoCalidad.valor_referencial = Convert.ToInt16(vObjIsoNlgi.Value);
                                vEspecificacionMinima = EspecificacionMinima.Text;
                                vEspecificacionMaxima = EspecificacionMaxima.Text;
                            }
                            if (vObjIsoNlgi!=null && NumeroRegistro.Text != "" && (EspecificacionMaxima.Text == "" || EspecificacionMinima.Text == ""))
                            {
                                alert.Visible = true;
                                vObjIsoNlgi.BorderColor = Color.Red;
                                vObjIsoNlgi.BorderStyle = BorderStyle.Groove;
                            }

                            if (vObjUnidad.Value == null)
                            {
                                alert.Visible = true;
                                vObjUnidad.BorderColor = Color.Red;
                                vObjUnidad.BorderStyle = BorderStyle.Groove;
                            }
                            else
                                vObjObjetoCalidad.id_unidad_medida = Convert.ToInt16(vObjUnidad.Value);

                            if (vObjMetodoASTM.Value == null)
                            {
                                if (id_prueba_cal == 133)
                                {
                                    //no mando alerta
                                }
                                else
                                {
                                    alert.Visible = true;
                                    vObjMetodoASTM.BorderColor = Color.Red;
                                    vObjMetodoASTM.BorderStyle = BorderStyle.Groove;
                                }

                            }
                            else
                                vObjObjetoCalidad.id_metodo_astm = Convert.ToInt16(vObjMetodoASTM.Value);

                            decimal varRes = 1;

                            if (vObjValorReportado.Text == string.Empty)
                            {
                                varRes = ValidarIndiceCetano(id_prueba_cal, vObjValorReportado,
                                    valoridPruebaAnterior, valoridPruebaPosterior,
                                    vObjValorReportadoAnt, vObjValorReportadoPost,
                                    vObjMetodoASTM);
                            }

                            if (varRes == 1)
                            {
                                if (vEspecificacionMaxima != null || vEspecificacionMinima != null)
                                {
                                    if (vEspecificacionMaxima != null)
                                    {
                                        vEspecificacionMaxima = ObtenerEspecificacionPorUnidad(
                                            vEspecificacionMaxima.ToString(), vObjUnidad.Text, true);
                                    }

                                    if (vEspecificacionMinima != null)
                                    {
                                        vEspecificacionMinima = ObtenerEspecificacionPorUnidad(
                                            vEspecificacionMinima.ToString(), vObjUnidad.Text, false);
                                    }

                                    bool vResultadoVerificacionAux=false;
                                    decimal vResultadoReportado=0;


                                    vResultadoVerificacion = vEspecificacionMaxima != null
                                        ? decimal.TryParse(vEspecificacionMaxima.ToString(), out vValorNumerico)
                                        : decimal.TryParse(vEspecificacionMinima.ToString(), out vValorNumerico);
                                    if (vResultadoVerificacion)
                                    {
                                        if (vObjValorReportado.Text.Contains(">"))
                                        {
                                            vResultadoVerificacionAux = decimal.TryParse(vObjValorReportado.Text.Replace(">", "").Trim() , out vValorNumerico);
                                            if (vResultadoVerificacionAux)
                                            {
                                                vResultadoReportado = Convert.ToDecimal(vObjValorReportado.Text.Replace(">", "").Trim()) + Convert.ToDecimal(0.1);    
                                            }
                                            else
                                            {
                                                alert.Visible = true;
                                                idmsjErrorNumerico.Text = "DEBE INGRESAR UN VALOR NUMÉRICO EN EL VALOR REPORTADO.";
                                                vObjValorReportado.BorderColor = Color.Red;
                                                vObjValorReportado.BorderStyle = BorderStyle.Groove;
                                                vObjValorReportado.BorderWidth = 1; 
                                            }
                                        }
                                        else
                                        {
                                            if (vObjValorReportado.Text.Contains("<"))
                                            {
                                                vResultadoVerificacionAux =
                                                    decimal.TryParse(vObjValorReportado.Text.Replace("<", "").Trim(),
                                                        out vValorNumerico);
                                                if (vResultadoVerificacionAux)
                                                {
                                                    vResultadoReportado =
                                                        Convert.ToDecimal(
                                                            vObjValorReportado.Text.Replace("<", "").Trim()) -
                                                        Convert.ToDecimal(0.1);
                                                }
                                                else
                                                {
                                                    alert.Visible = true;
                                                    idmsjErrorNumerico.Text =
                                                        "DEBE INGRESAR UN VALOR NUMÉRICO EN EL VALOR REPORTADO.";
                                                    vObjValorReportado.BorderColor = Color.Red;
                                                    vObjValorReportado.BorderStyle = BorderStyle.Groove;
                                                    vObjValorReportado.BorderWidth = 1;
                                                }
                                            }
                                            else
                                            {
                                                vResultadoVerificacion = decimal.TryParse(vObjValorReportado.Text.Replace(">", "").Replace("<", ""), out vValorNumerico);
                                                if (!vResultadoVerificacion)
                                                {
                                                    alert.Visible = true;
                                                    idmsjErrorNumerico.Text = "DEBE INGRESAR UN VALOR NUMÉRICO EN EL VALOR REPORTADO.";
                                                    vObjValorReportado.BorderColor = Color.Red;
                                                    vObjValorReportado.BorderStyle = BorderStyle.Groove;
                                                    vObjValorReportado.BorderWidth = 1;
                                                }
                                                else
                                                {
                                                    
                                                }
                                            }

                                            if (Session[CParametrosCalidad.cValidacionCalidad] != null && (((List<O_VALIDA_CALIDAD_CTY>)
                                                            Session[CParametrosCalidad.cValidacionCalidad])
                                                            .FirstOrDefault(x => x.VALIDA_CALIDAD == "PRUEBA_CERRADA") !=
                                                         null))
                                            {
                                                // Verificar valores máximos y mínimos de los valores numéricos.

                                                string strEspecificacionMinima = vEspecificacionMinima != null ? vEspecificacionMinima.ToString() : null;
                                                string strEspecificacionMaxima = vEspecificacionMaxima != null ? vEspecificacionMaxima.ToString() : null;

                                                if (
                                                    !VerificarValorNumerico(Convert.ToDecimal(vResultadoReportado/*vObjValorReportado.Text.Replace(".", ",")*/),
                                                        strEspecificacionMinima, strEspecificacionMaxima))
                                                {
                                                    idmsMensajeMaxMin.Text = "VERIFICAR VALORES MÁXIMOS Y/O MÍNIMOS PERMITIDOS";
                                                    alert.Visible = true;
                                                    vObjValorReportado.BorderColor = Color.Red;
                                                    vObjValorReportado.BorderStyle = BorderStyle.Groove;
                                                }
                                                else
                                                {
                                                    vObjValorReportado.BorderColor = Color.Silver;
                                                }
                                            }
                                        }
                                        
                                        
                                    }
                                }
                                else
                                {
                                    vObjValorReportado.BorderColor = Color.Silver;
                                }
                                if (VerificaExpresionRegular(vObjValorReportado.Text, CParametrosCalidad.cAlfanumericoBasicoConTildesConEspacios))
                                {
                                    //TODO: IMPORTANTE CAMBIAR PARA PRODUCCION (VALOR REPORTADO)
                                    //vObjObjetoCalidad.valor_alfanumerico = vObjValorReportado.Text.Replace(",", ".").Trim();
                                    ////vObjObjetoCalidad.valor_alfanumerico = vObjValorReportado.Text.Replace(".", ",").Trim();

                                    /*vObjObjetoCalidad.valor_alfanumerico = CParametrosHydro.idEntornoAplicacion != "PRO" ? vObjValorReportado.Text.Replace(".", ",").Trim() : vObjValorReportado.Text.Replace(",", ".").Trim();*/
                                    vObjObjetoCalidad.valor_alfanumerico = vObjValorReportado.Text.Trim();
                                }
                                else
                                {
                                    alert.Visible = true;
                                    idmsjError.Text = "CARACTER ESPECIAL INVÁLIDO.";
                                    vObjValorReportado.BorderColor = Color.Red;
                                    vObjValorReportado.BorderStyle = BorderStyle.Groove;
                                }
                            }
                            if (!vObjObjetoCalidad.valor_alfanumerico.IsNullOrEmpty())
                            {
                                vObjObjetoCalidad.justificacion = vObjJustificacion != null ? vObjJustificacion.Text : "";
                                vObjObjetoCalidad.id_prueba_unidad = Convert.ToDecimal(grdCertificadoCalidad.GetRowValues(i, "decIdPruebaCalidad"));
                                vObjObjetoCalidad.id_prueba_calidad = Convert.ToDecimal(grdCertificadoCalidad.GetRowValues(i, "decIdPruebaCalidad"));
                                vColObjetoCalidad.Add(vObjObjetoCalidad);
                            }
                        }
                    }

                    #endregion


                    if (divDinamicImp.Visible == true && txtResolucion.Text == "")
                    {
                        alert.Visible = true;
                        idmsjError.Text = "INGRESE EL NUMERO DE RESOLUCIÓN.";
                        txtResolucion.BorderColor = Color.Red;
                        txtResolucion.BorderStyle = BorderStyle.Groove;
                    }


                    if ((TipoDeRegistro == 2 || TipoDeRegistro == 3) && pnlAdjuntaArchivo.Visible == true)
                    {
                        if (Session[CVariablesSesion.DocumentoDig] == null)
                        {
                            if (alert.Visible == true)
                            {
                                idmsjError.Text = idmsjError.Text + "<br/> NO HAY ARCHIVO SELECCIONADO.";
                            }
                            else
                            {
                                alert.Visible = true;
                                idmsjError.Text = "NO HAY ARCHIVO SELECCIONADO.";
                            }
                        }
                    }

                   
                   
                    if (!alert.Visible)
                    {

                        /*qui hacer datagrid*/

                        /*************************************************************************************************************************/
                        ASPxComboBox v3 = null;
                        //ASPxTextBox vObjValorReportado = null;
                        TextBox v4 = null;
                        // v5 = null;
                        string v7 = null;
                        ASPxComboBox v2 = null;
                        //v1 = (TextBox)grdCertificadoCalidad.FindRowCellTemplateControl(0, null, "txtValorReportado");// grdCertificadoCalidad.VisibleRowCount;

                        DataTable dt = new DataTable();

                        // DataColumn decIdPruebaCalidad = dt.Columns.Add("decIdPruebaCalidad", typeof(string)); //ok
                        DataColumn numero = dt.Columns.Add("N°", typeof(string)); //ok
                        DataColumn strDescripcion = dt.Columns.Add("Pruba/Ensayo", typeof(string)); //ok
                        //DataColumn codigo = dt.Columns.Add("cmbIsoNlgi", typeof(string)); //no
                        DataColumn nombre = dt.Columns.Add("Método ASTM", typeof(string)); //no
                        DataColumn cmbUnidad = dt.Columns.Add("Unidad", typeof(string)); //ok
                        DataColumn txtValorReportado = dt.Columns.Add("Valor Reportado", typeof(string)); //ok 
                        
                        for (int i = 1; i <= grdCertificadoCalidad.VisibleRowCount; i++)
                        {
                            v7 = Convert.ToString(grdCertificadoCalidad.GetRowValues(i - 1, "strDescripcion"));  //(ok)
                            // v1 = Convert.ToString(grdCertificadoCalidad.GetRowValues(i-1, "cmbIsoNlgi"));
                            v2 = (ASPxComboBox)grdCertificadoCalidad.FindRowCellTemplateControl(i-1, null, "cmbMetodoASTM");
                            v3 = (ASPxComboBox)grdCertificadoCalidad.FindRowCellTemplateControl(i-1, null, "cmbUnidad");   //(ok)
                            v4 = (TextBox)grdCertificadoCalidad.FindRowCellTemplateControl(i-1, null, "txtValorReportado");

                            //decimal v6 = Convert.ToDecimal(grdCertificadoCalidad.GetRowValues(i-1, "decIdPruebaCalidad")); //(ok)
                            dt.Rows.Add(i,v7,v2.Text ,v3.Text, v4.Text);
                        }

                        //dt.Rows.Add(1, "Jonathan Orozco", "Monterrey");
                        //dt.Rows.Add(2, "Jesus Corona", "México");
                        //dt.Rows.Add(3, "Cirilo Zaucedo", "Tijuana");
                        //dt.Rows.Add(4, "Humberto Suazo", "Chile");

                      
                        ASPxGridView1.DataSource = dt;
                        ASPxGridView1.DataBind();
                        
                        /**********************************************************************************************************************/


                        ppMensajeAlerta.ShowOnPageLoad = true;

                        Session.Add("List_vColObjetoCalidad", vColObjetoCalidad);
                        Session.Add("List_vObjObjetoCalidad", vObjObjetoCalidad);

                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }
        /// <summary>
        /// Guarda el objeto calidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnContinuar_OnClick(object sender, EventArgs e)
        {
            List<ObjetoCalidad> vColObjetoCalidad = (List<ObjetoCalidad>)Session["List_vColObjetoCalidad"];
            ObjetoCalidad vObjObjetoCalidad = (ObjetoCalidad)Session["List_vObjObjetoCalidad"];

           
            guardarObjCalidad(vColObjetoCalidad, vObjObjetoCalidad); 
            ppMensajeAlerta.ShowOnPageLoad = false;
        
            Session.Remove("List_vColObjetoCalidad");
            Session.Remove("List_vObjObjetoCalidad");

            #region Traza consulta de informacion                      
            CLogTraza.Informacion("RESULTADO: " + base.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + " ENTRADA: " + JsonConvert.SerializeObject(vColObjetoCalidad), CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]), HttpContext.Current.Request.UserHostAddress, null);
            #endregion
        }
        /// <summary>
        /// Cancela el y limpia el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelarRegistroCalidad_Click(object sender, EventArgs e)
        {
          
        }
        /// <summary>
        /// Elimina registro o certificado de calidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEliminarRegistroCalidad_Click(object sender, EventArgs e)
        {
            if (Session[CVariablesSesion.UsuarioId] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                var vObjEliminaRegistroCalidad = new EliminaRegistroCalidad();
                vObjEliminaRegistroCalidad.strLlave = Llave;
                try
                {
                    vObjEliminaRegistroCalidad.decAppFechaRegistro = Convert.ToInt64(String.Format("{0:yyyyMMddhhmmss}", Convert.ToDateTime(DateTime.Now))); ;
                    vObjEliminaRegistroCalidad.strCiteGenerado = TextBoxNumeroCite.Text;
                    vObjEliminaRegistroCalidad.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]);
                    var objResultado = clienteJson.Post<AnhPresentacionDTEP.Entidades.O_RESULTADO_CTY>("/EliminaRegistroCalidad/?format=json", vObjEliminaRegistroCalidad);
                    if (objResultado.MENSAJE_ERROR == "OK" && objResultado.RESULTADO == 0 && objResultado.ID_TABLA > 0)
                    {
                        alert.Visible = false;
                        GridSelectorChanged(new GuardarRegistroCommandEventArgs(TextBoxNumeroCite.Text));
                        guardarDocumentoAjdunto();
                    }
                    else
                    {
                        alert.Visible = true;
                        idmsjError.Text = objResultado.MENSAJE_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                    alert.Visible = true;
                    idmsjError.Text = "NO SE PUDO ELIMINAR LA PRUEBA DE CALIDAD.";
                }
            }
        }
        /// <summary>
        /// Permite crear la numeracion de las filas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCertificadoCalidad_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name == "Numero")
                e.DisplayText = (e.VisibleRowIndex + 1).ToString();
        }
        /// <summary>
        /// Actualiza valores en la grilla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCertificadoCalidad_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (Session[CVariablesSesion.UsuarioId] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                vValorDatosGrilla = (String[])Session[CParametrosCalidad.cValoresMenu];


                if (TipoDeRegistro == 1)
                    CargaGrilla(vValorDatosGrilla[1], vValorDatosGrilla[2], vValorDatosGrilla[3], Convert.ToDecimal(vValorDatosGrilla[0]));
                try
                {
                    #region Carga combos

                    var vColFormularioEspecifico = (List<E_TABLA_ESPECIFICA>)Session[CParametrosCalidad.cColListarPruebasCalidadTablaEspecificacion];
                    for (int item = 0; item < vColFormularioEspecifico.Count; item++)
                    {
                        int dato = Convert.ToInt32(grdCertificadoCalidad.GetRowValues(item, "decIdPruebaCalidad"));
                        string PruebaEnsayo = Convert.ToString(grdCertificadoCalidad.GetRowValues(item, "strDescripcion"));
                        PruebaEnsayo = !string.IsNullOrEmpty(PruebaEnsayo) ? PruebaEnsayo.Trim().ToUpper() : null;
                        if (dato == 0)
                            return;
                        object ObjEspecAlfanumerico = (grdCertificadoCalidad.GetRowValues(item, "strEspecAlfanumerico"));
                        if (PruebaEnsayo.ToUpper() == "API")
                        {
                            TextBox vObjValorReportado = (TextBox)grdCertificadoCalidad.FindRowCellTemplateControl(item, null, "txtValorReportado");
                            vObjValorReportado.AutoPostBack = true;
                            RegistroApi.Text = Convert.ToString(item);
                            if (vColFormularioEspecifico[item] != null)
                                LabelIdPruebaCalidad.Text = Convert.ToString(vColFormularioEspecifico[item].decIdPruebaCalidad);
                            LabelTipoAlerta.Text = "2";
                        }

                        if (PruebaEnsayo.ToUpper() == "CLASIFICACIÓN NLGI")
                        {
                            ASPxTextBox vObjValorReportado = (ASPxTextBox)grdCertificadoCalidad.FindRowCellTemplateControl(item, null, "txtValorReportado");
                            vObjValorReportado.AutoPostBack = true;
                            ClasificacionNLGI.Text = Convert.ToString(item);
                            if (vColFormularioEspecifico[item] != null)
                                LabelIdPruebaCalidad.Text = Convert.ToString(vColFormularioEspecifico[item].decIdPruebaCalidad);
                            LabelTipoAlerta.Text = "4";
                        }

                        BindearCombosGrilla(item, vColFormularioEspecifico);

                        if (ObjEspecAlfanumerico != null)
                            count1++;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    alert.Visible = true;
                    idmsjError.Text = ex.Message;
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }
        /// <summary>
        /// Carga el archivo adjunto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void upArchivo_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            archivo = upArchivo.FileBytes;
            if (archivo != null)
            {
                lblOkArchivo.Text = "SE HA SELECCIONADO EL ARCHIVO: " + upArchivo.FileName + "</br> SI DESEA, PUEDE CAMBIAR SU ARCHIVO.";
                lblOkArchivo.Visible = true;
                Session[CVariablesSesion.DocumentoDig] = archivo;

            }
            else
            {
                lblNoArchivo.Visible = false;
            }
        }
        /// <summary>
        /// Muestra columna de maximos y minimos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnVisualiza_Click(object sender, EventArgs e)
        {
            if (Session[CVariablesSesion.UsuarioId] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                try
                {



                    if (btnVisualiza.Text.ToUpper().Contains("VISUALIZAR"))
                    {
                        grdCertificadoCalidad.Columns["EspecMinima"].Visible = true;
                        grdCertificadoCalidad.Columns["EspecMaxima"].Visible = true;
                        grdCertificadoCalidad.Columns["Justificacion"].Visible = true;
                        //grdCertificadoCalidad.Columns["Especificacion"].Visible = false;
                        btnVisualiza.Text = "Ocultar Parametros";
                    }
                    else
                    {
                        grdCertificadoCalidad.Columns["EspecMinima"].Visible = false;
                        grdCertificadoCalidad.Columns["EspecMaxima"].Visible = false;
                        grdCertificadoCalidad.Columns["Justificacion"].Visible = false;
                        //grdCertificadoCalidad.Columns["Especificacion"].Visible = false;
                        btnVisualiza.Text = "Visualizar Parametros";
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }
        /// <summary>
        /// Muestra oculta el formulario de registro o el forlulario de volumen cero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbxVolumenCero_CheckedChanged(object sender, EventArgs e)
        {
            if (Session[CVariablesSesion.UsuarioId] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                try
                {
                    if (ckbxVolumenCero.Checked)
                    {
                        panelRegistroNormal.Visible = false;
                        panelVolumenCero.Visible = true;
                        //this.LimpiarFormularioRegistroCalidadActual();
                    }
                    else
                    {
                        panelRegistroNormal.Visible = true;
                        panelVolumenCero.Visible = false;
                        //LimpiarFormularioDeclaracionVolumenCero();
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }
        /// <summary>
        /// Registra y genera el cite para volumen cero.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegistrarVolumenCero_Click(object sender, EventArgs e)
        {
            if (Session[CVariablesSesion.UsuarioId] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                try
                {
                    divAlertaVolCero.Visible = false;
                    cite.Visible = false;
                    DateTime fecha = new DateTime();
                    if (DateTime.TryParse(deFechaImportacionVolCero.Text, out fecha))
                    {
                        var objResultado = clienteJson.Post<O_REF_REG_REPORTE_PLANO_CTY>(
                            "/GestionVolumen/?format=json",
                            new
                                {
                                    strLlave = cParametrosHydro.strCredencial,
                                    decIdPruebaCalidad = 0, //solo en caso de eliminacion
                                    decIdEntidad = Convert.ToDecimal(Session[CVariablesSesion.IdEntidad]),
                                    decIdTipoActividad = Convert.ToDecimal(Session[CVariablesSesion.IdTipoActividad]),
                                    decFechaImportacion =
                                        Convert.ToInt64(
                                            String.Format(
                                                "{0:yyyyMMddhhmmss}",
                                                Convert.ToDateTime(deFechaImportacionVolCero.Date))),
                                    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.UsuarioId]),
                                    decAccion = 1 //1=registro/3=elimina
                                });


                        if (objResultado.MENSAJE_ERROR.Contains("OK") && objResultado.RESULTADO > 0)
                        {
                            divAlertaVolCero.Visible = false;
                            lblAlertaVolumenCero.Text = "";
                            lblAlertaVolumenCero.Visible = true;
                            cite.Visible = true;
                            TextBoxNumeroCite.Visible = true;
                            TextBoxNumeroCite.Text = objResultado.CORRELATIVO_REGISTRO;
                            divAlertaVolCero.Visible = false;
                            this.LimpiarFormularioDeclaracionVolumenCero();
                            //LimpiaVariablesSession();
                        }
                        else
                        {
                            divAlertaVolCero.Visible = true;
                            lblAlertaVolumenCero.Text = objResultado.MENSAJE_ERROR;
                            lblAlertaVolumenCero.Visible = true;
                        }
                    }
                    else
                    {
                        divAlertaVolCero.Visible = true;
                        lblAlertaVolumenCero.Text = "FECHA DE IMPORTACIÓN/OPERACIÓN NO VÁLIDA";
                    }
                }
                catch (Exception ex)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }
        protected void btnCancelarAlerta_OnClick(object sender, EventArgs e)
        {
            ppMensajeAlerta.ShowOnPageLoad = false;

            lblNoArchivo.Visible = false;
            lblOkArchivo.Visible = true;
        }
        #endregion

        #region Declaración de objetos

        public class O_TABLA_ESPEC_FORMULARIO_CITY
        {
            public decimal ID_PRUEBA_CALIDAD { get; set; }
            public decimal ID_PRUEBA_CALIDAD_PADRE { get; set; }
            public string DESCRIPCION { get; set; }
            public string ESPEC_MINIMA { get; set; }
            public string ESPEC_MAXIMA { get; set; }
            public string UNIDAD_MEDIDA { get; set; }
            public string METODO_ASTM { get; set; }
            public string RANGOS_MULTIPLES { get; set; }
        }

        public class E_TABLA_ESPECIFICA
        {
            public decimal decIdPruebaCalidad { get; set; }
            public decimal decIdPruebaCalidadPadre { get; set; }
            public string strDescripcion { get; set; }
            public List<E_CAMPO_VALOR> listMetodoAstm { get; set; }
            public List<E_CAMPO_VALOR> listUnidadMedida { get; set; }
            public List<E_CAMPO_VALOR> listRangosMultiples { get; set; }
            public string strEspecMinima { get; set; }
            public string strEspecMaxima { get; set; }
            public string strEspecAlfanumerico { get; set; }
            public List<E_CAMPO_VALOR> listEspecMinimaRango { get; set; }
            public List<E_CAMPO_VALOR> listEspecMaximaRango { get; set; }
        }
        public class E_CAMPO_VALOR
        {
            public decimal intValor { get; set; }
            public string strCampo { get; set; }
        }

        public class O_PUNTO_CUSTODIO_CTY
        {
            public decimal ID_PUNTO_CUSTODIO { get; set; }
            public decimal PRODUCTO_ID { get; set; }
            public string PRODUCTO { get; set; }
            public string CODIGO { get; set; }
            public string PUNTO_CUSTODIO { get; set; }
        }

        public class O_UM_CANT_CAL_CTY
        {
            public string DESCRIPCION { get; set; }
            public string NOMBRE { get; set; }
            public string CODIGO { get; set; }
            public decimal ID_UNIDAD_MEDIDA { get; set; }
        }

        public class O_RESULTADO_NUMBER_CTY
        {
            public decimal RESULTADO { get; set; }
        }

        public class EliminaRegistroCalidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strCiteGenerado { get; set; }
            public decimal decAppFechaRegistro { get; set; }
            public decimal decAppIdUsuario { get; set; }
        }

        public class RegistraAlertaCalidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public decimal decIdTipoAlerta { get; set; }
            public decimal decIdPruebaCalidad { get; set; }
            public string strCiteGenerado { get; set; }
            public string strObservaciones { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAppFechaRegistro { get; set; }
        }

        #region Registro de carburantes

        public class RegistraCalidadCarburantes : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strEncabezado { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAppFechaRegistro { get; set; }
        }

        public class ActualizaPruebasCalidad : IReturn<EResultado>
        {
            public string strLlave { get; set; }
            public string strCiteGenerado { get; set; }
            public string strEncabezado { get; set; }
            public decimal decAppIdUsuario { get; set; }
            public decimal decAppFechaRegistro { get; set; }
        }

        public class O_REF_REG_REPORTE_PLANO_CTY
        {
            public string CORRELATIVO_REGISTRO { get; set; }
            public string MENSAJE_ERROR { get; set; }
            public decimal RESULTADO { get; set; }
        }
        public partial class O_RESULTADO_CTY
        {
            public decimal ID_TABLA { get; set; }
            public string MENSAJE_ERROR { get; set; }
            public Nullable<decimal> RESULTADO { get; set; }
        }

        public class EResultado
        {
            public decimal decCodigo { get; set; }
            public string strMensaje { get; set; }
            public Object oResultado { get; set; }
        }

        #endregion

        #endregion

        public class RetornaIdDireccion : IReturn<EResultado>
        {
            public decimal decIdUsuario { get; set; }
        }
        public void limpiar()
        {
            Session[CVariablesSesion.DocumentoDig] = null;
            lblOkArchivo.Text = "";
            lblNoArchivo.Text = "";
            txtObservacion.Text = "";


            if (Session[CVariablesSesion.UsuarioId] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                vValorDatosGrilla = (String[])Session[CParametrosCalidad.cValoresMenu];


                if (TipoDeRegistro == 1)
                    CargaGrilla(vValorDatosGrilla[1], vValorDatosGrilla[2], vValorDatosGrilla[3], Convert.ToDecimal(vValorDatosGrilla[0]));
                try
                {
                    #region Carga combos

                    var vColFormularioEspecifico = (List<E_TABLA_ESPECIFICA>)Session[CParametrosCalidad.cColListarPruebasCalidadTablaEspecificacion];
                    for (int item = 0; item < vColFormularioEspecifico.Count; item++)
                    {
                        int dato = Convert.ToInt32(grdCertificadoCalidad.GetRowValues(item, "decIdPruebaCalidad"));
                        string PruebaEnsayo = Convert.ToString(grdCertificadoCalidad.GetRowValues(item, "strDescripcion"));
                        PruebaEnsayo = !string.IsNullOrEmpty(PruebaEnsayo) ? PruebaEnsayo.Trim().ToUpper() : null;
                        if (dato == 0)
                            return;
                        object ObjEspecAlfanumerico = (grdCertificadoCalidad.GetRowValues(item, "strEspecAlfanumerico"));
                        if (PruebaEnsayo.ToUpper() == "API")
                        {
                            TextBox vObjValorReportado = (TextBox)grdCertificadoCalidad.FindRowCellTemplateControl(item, null, "txtValorReportado");
                            vObjValorReportado.AutoPostBack = true;
                            RegistroApi.Text = Convert.ToString(item);
                            if (vColFormularioEspecifico[item] != null)
                                LabelIdPruebaCalidad.Text = Convert.ToString(vColFormularioEspecifico[item].decIdPruebaCalidad);
                            LabelTipoAlerta.Text = "2";
                        }

                        if (PruebaEnsayo.ToUpper() == "CLASIFICACIÓN NLGI")
                        {
                            ASPxTextBox vObjValorReportado = (ASPxTextBox)grdCertificadoCalidad.FindRowCellTemplateControl(item, null, "txtValorReportado");
                            vObjValorReportado.AutoPostBack = true;
                            ClasificacionNLGI.Text = Convert.ToString(item);
                            if (vColFormularioEspecifico[item] != null)
                                LabelIdPruebaCalidad.Text = Convert.ToString(vColFormularioEspecifico[item].decIdPruebaCalidad);
                            LabelTipoAlerta.Text = "4";
                        }

                        BindearCombosGrilla(item, vColFormularioEspecifico);

                        if (ObjEspecAlfanumerico != null)
                            count1++;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    alert.Visible = true;
                    idmsjError.Text = ex.Message;
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(ex, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LimpiarFormularioRegistroCalidadActual();
            limpiar();
        }

        public class EResultadoTipoOperacion
        {
            public int IntCodigo { get; set; }
            public string StrMensaje { get; set; }
            public List<O_TIPOS_OPE_ACT_CTY> OResultado { get; set; }
        }

        public partial class O_TIPOS_OPE_ACT_CTY
        {
            public decimal ID_TIPO_OPERACION { get; set; }
            public string CODIGO { get; set; }
            public string NOMBRE { get; set; }
            public decimal ID_TIPO_OPERACION_PADRE { get; set; }
            public decimal ID_TIPO_ACTIVIDAD { get; set; }
            public decimal ORDEN { get; set; }
            public Nullable<decimal> FACTOR_CALCULO { get; set; }
            public Nullable<decimal> LIMITE_MINIMO { get; set; }
            public Nullable<decimal> LIMITE_MAXIMO { get; set; }
            public string CODIGO_REPORTE_VARIABLE { get; set; }
            public Nullable<double> ID_REPORTE_VARIABLE { get; set; }
            public string BINARIO_ESTADO { get; set; }
            public string STRTIPO { get; set; }
            public decimal AUD_ESTADO { get; set; }
        }

    }
}
