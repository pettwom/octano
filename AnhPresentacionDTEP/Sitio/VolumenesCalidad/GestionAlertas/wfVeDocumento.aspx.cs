using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhPersistenciaCore.Core;
using AnhPresentacionDTEP.Clases.VolumenesCalidad;
using AnhPresentacionDTEP.Parametros;
using AnhPresentacionDTEP.Parametros.VolumenesCalidad;
using DevExpress.XtraRichEdit.Import.Html;
using ServiceStack.ServiceClient.Web;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAlertas
{
    public partial class wfVeDocumento : System.Web.UI.Page
    {
        #region Variables de entorno

        /// <summary>
        /// Idnetificador 
        /// </summary>
        private decimal _idTipoRespaldo;
        private string _cite;
        string strAccion = "";
        private decimal _idDocumento;
        JsonServiceClient clienteJson = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJson"]);

        #endregion

        protected void Page_Load(object sender, EventArgs e)
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
                    #region Codigo a controlar

                    _idTipoRespaldo = Convert.ToDecimal(Request["idTipoRespaldo"]);
                    _cite = Request.QueryString["Cite"];
                    _idDocumento = Convert.ToDecimal(Request["idDocumento"]);
                    string _citeConvert = null;

                    if (!string.IsNullOrEmpty(_cite))
                    {
                        _citeConvert = (_cite.Replace("%20", " ")).Replace("/", "_");
                    }
                    
                    List<O_LISTA_DOCUMENTOS_CTY> imagenDoc = null;

                    if (_idTipoRespaldo == 1)
                    {
                        imagenDoc = clienteJson.Get<List<O_LISTA_DOCUMENTOS_CTY>>("/ListarDocumentosDetalle/" + cParametrosHydro.strCredencial + "/" +
                                                                          _idDocumento + "?format=json");
                    }
                    else
                    {
                        imagenDoc = clienteJson.Get<List<O_LISTA_DOCUMENTOS_CTY>>("/ListarDocumentos/" + cParametrosHydro.strCredencial + "/" +
                                                                          _citeConvert + "/" + _idTipoRespaldo +
                                                                          "?format=json");
                    }

                    if (imagenDoc != null)
                        {
                            if (imagenDoc.Count != 0)
                            {
                                byte[] byteContenido = null;
                                foreach (var varDocu in imagenDoc)
                                {
                                    byteContenido = varDocu.DOCUMENTO;
                                    break;
                                }
                                if (byteContenido != null)
                                {
                                    /*String strReporte = Convert.ToBase64String(byteContenido);
                                    iframeInforme.Attributes["src"] =
                                        String.Format("data:application/pdf;base64,{0}" + strReporte);*/
                                  
                                    if (byteContenido.Count() > 10)
                                    {
                                        Response.ContentType = "application/pdf";
                                        Response.AddHeader("content-length", byteContenido.Length.ToString());
                                        Response.BinaryWrite(byteContenido);
                                    }
                                }
                                else
                                {
                                    lblMensaje.Visible = true;
                                    panelDetalle.Visible = false;
                                }
                            }
                            else
                            {
                                lblMensaje.Visible = true;
                                panelDetalle.Visible = false;
                            }

                        }
                        else
                        {
                            lblMensaje.Visible = true;
                            panelDetalle.Visible = false;
                        }

                    #endregion
                }
                catch (Exception exp)
                {
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                    CCalidadLibreria.ValidacionAdministradorHydro(exp, Session[CVariablesSesion.UsuarioId], strAccion);
                }
            }
        }
    }
}