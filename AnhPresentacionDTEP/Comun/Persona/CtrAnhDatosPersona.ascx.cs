using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhAgenteServicios;
using AnhAgenteServicios.ServicioConsultasHydro;
using AnhAgenteServicios.ServicioHydroListados;
using AnhAgenteServicios.ServicioParametricas;
using AnhHydroTalleresOpeGarrafasPresentacion.Clases.Parametricas;
using AnhHydroTalleresOpeGarrafasPresentacion.Librerias;
using AnhPresentacionDTEP.Parametros;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;


namespace AnhHydroOctanoPresentacion.Controles
{
    public partial class CtrAnhDatosPersona : System.Web.UI.UserControl
    {
        private string _mensajeError;
        private const string _viewStateIdPersona = "id_persona";
        private const string _viewStatePersonas = "personas";
        //private const string _viewStateIdUsuario = "id_usuario";
        private CPersonaSesion _sesion;
        
        JsonServiceClient client = new JsonServiceClient(CAppSettings.ServicioOctanoVolumenes);

        protected void Page_Load(object sender, EventArgs e)
        { 
            _sesion = new CPersonaSesion(Server.MapPath("~"), Request.ServerVariables["REMOTE_ADDR"]);;
            mCargarDatosControl();
        }

        private void mCargarDatosControl()
        {
            if (_servicioParametricas != null)
            {
                datosPersona.DataSourceTiposIdentificacion = _servicioParametricas.listadoTiposIdentificacion(CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                if (datosPersona.TipoIdentificacion == "1" || datosPersona.TipoIdentificacion == "4")
                {
                datosPersona.DataSourceDepartamentos = _servicioParametricas.listadoDepartamentos(CParametrosHydro.StrCredencialEmpadronamiento,ref _mensajeError);
                    
                }
                else
                {
                    datosPersona.DataSourceDepartamentos = _servicioParametricas.listadoPaises(CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                    
                }
                datosPersona.DataSourceGeneros = _servicioParametricas.listadoGeneros(CParametrosHydro.StrCredencialEmpadronamiento,ref _mensajeError);   
            }
            else
            {
                //Session["usuario_registrado"] = null;
            }
        }

        #region Campos de Ingreso de Datos

        private decimal _idPersona;
        private string _tipoIdentificacion;
        private string _nroIdentificacion;
        private string _expedicion;
        private string _complemento;
        private string _nombres;
        private string _primerApellido;
        private string _segundoApellido;
        private string _genero;
        private DateTime _fechaNacimiento;
        private string _email;

        public decimal IdPersona
        {
            get
            {
                if (ViewState[_viewStateIdPersona] != null)
                {
                    return Convert.ToDecimal(ViewState[_viewStateIdPersona]);
                }
                else
                {
                    return 0;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string TipoIdentificacion
        {
            get { return datosPersona.TipoIdentificacion; }
            set
            {
                if (datosPersona.TipoIdentificacion != value)
                {
                    datosPersona.TipoIdentificacion = value;
                    _tipoIdentificacion = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string NroIdentificacion
        {
            get { return datosPersona.NroIdentificacion; }
            set
            {
                if (datosPersona.NroIdentificacion != value)
                {
                    datosPersona.NroIdentificacion = value;
                    _nroIdentificacion = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string Expedicion
        {
            get { return datosPersona.Expedicion; }
            set
            {
                if (datosPersona.Expedicion != value)
                {
                    datosPersona.Expedicion = value;
                    _expedicion = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string Complemento
        {
            get { return datosPersona.Complemento; }
            set
            {
                if (datosPersona.Complemento != value)
                {
                    datosPersona.Complemento = value;
                    _complemento = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string Nombres
        {
            get { return datosPersona.Nombres; }
            set
            {
                if (datosPersona.Nombres != value)
                {
                    datosPersona.Nombres = value;
                    _nombres = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string PrimerApellido
        {
            get { return datosPersona.PrimerApellido; }
            set
            {
                if (datosPersona.PrimerApellido != value)
                {
                    datosPersona.PrimerApellido = value;
                    _primerApellido = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string SegundoApellido
        {
            get { return datosPersona.SegundoApellido; }
            set
            {
                if (datosPersona.SegundoApellido != value)
                {
                    datosPersona.SegundoApellido = value;
                    _segundoApellido = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string Genero
        {
            get { return datosPersona.Genero; }
            set
            {
                if (datosPersona.Genero != value)
                {
                    datosPersona.Genero = value;
                    _genero = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public DateTime FechaNacimiento
        {
            get
            {
                try
                {
                    return datosPersona.FechaNacimiento;
                }
                catch (Exception)
                {
                    return Convert.ToDateTime("01/01/1900");
                }
            }
            set
            {
                if (datosPersona.FechaNacimiento != value)
                {
                    datosPersona.FechaNacimiento = value.ToShortDateString() == "01/01/1900" ? Convert.ToDateTime("01/01/1900") : value;
                    _fechaNacimiento = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string Email
        {
            get { return datosPersona.Email; }
            set
            {
                if (datosPersona.Email != value)
                {
                    datosPersona.Email = value;
                    _email = value;
                }
            }
        }

        #endregion

        #region Asignacion de Servicio Web

        private IServicioParametricas _servicioParametricas;
        private IServicioHydroConsultas _servicioHydroConsultas;
        private O_DETALLE_REPRESENTANTE_CTY _representante;

        public IServicioParametricas ServicioParametricas
        {
            //get { return _servicioParametricas; }
            set
            {
                if (value != null)
                {
                    _servicioParametricas = value;
                }
            }
        }

        public IServicioHydroConsultas ServicioHydroConsultas
        {
            set
            {
                if (value != null)
                {
                    _servicioHydroConsultas = value;
                }
            }
        }

        public O_DETALLE_REPRESENTANTE_CTY Representante
        {
            set
            {
                if (value != null)
                {
                    _representante = value;
                    _idPersona = _representante.ID_PERSONA;
                    datosPersona.TipoIdentificacion = _representante.TIPO_IDENTIFICACION_ID.ToString();////////////////////////////////////////////////////////
                    datosPersona.NroIdentificacion = _representante.CI;
                    datosPersona.Expedicion = _representante.EXPEDICION_ID.ToString();// .EXPEDICION_ID.ToString();
                    datosPersona.Complemento = _representante.COMPLEMENTO;
                    datosPersona.Nombres = _representante.NOMBRE;
                    datosPersona.PrimerApellido = _representante.PRIMER_APELLIDO;
                    datosPersona.SegundoApellido = _representante.SEGUNDO_APELLIDO;
                    datosPersona.FechaNacimiento = CFechas.ConvierteLongDateTime(_representante.FECHA_NACIMIENTO);
                    datosPersona.Genero = _representante.GENERO_ID.ToString();
                    datosPersona.Email = _representante.EMAIL;

                    /*datosPersona.TipoIdentificacionReadOnly = true;
                    datosPersona.ExpedicionReadOnly = true;
                    datosPersona.ComplementoReadOnly = true;
                    datosPersona.PrimerApellidoReadOnly = true;
                    datosPersona.NombresReadOnly = true;
                    datosPersona.SegundoApellidoReadOnly = true;
                    datosPersona.FechaNacimientoReadOnly = true;
                    datosPersona.GeneroReadOnly = true;
                    datosPersona.EmailReadOnly = !string.IsNullOrEmpty(datosPersona.Email);*/

                    ViewState[_viewStateIdPersona] = _representante.ID_PERSONA;
                }
            }
        }

        #endregion

        #region Eventos Privados del Control AnhDatosPersona

        protected void dtgPersonas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            _servicioParametricas = LocalizadorProxy.ServicioParametricas(); // se agrego algo aca
            switch (e.CommandName)
            {
                case "ELEGIR":
                    int intIdPersona = Convert.ToInt32(e.CommandArgument);
                    if (intIdPersona != 0)
                    {
                        if (ViewState[_viewStatePersonas] != null)
                        {
                            List<O_PERSONA_CTY> personas = (List<O_PERSONA_CTY>)ViewState["personas"];

                            O_PERSONA_CTY persona = personas.Find(p => p.ID_PERSONA == intIdPersona);
                            decimal decIdUsuario = Convert.ToDecimal( _sesion.ExisteUsuario(persona.EMAIL));
                            //var lista2 = client.Get<EResultadoEntidad>(CAppSettings.svc_obtener_entidades + "/" + decIdUsuario + "?format=json");

                            var lista2 = client.Post<EResultadoEntidad>(CAppSettings.svc_obtener_entidades,
                                new EObtenerEntidadesUsuario()
                                {
                                    decFiltro = 1,
                                    strParametro1 = decIdUsuario.ToString(),
                                    strParametro2 = CAppSettings.IdActividades,
                                    strParametro3 = CAppSettings.idAplicacion,
                                    strParametro4 = "",
                                    strParametro5 = ""
                                });
                            
                            List<O_OBTIENE_ENTIDADES_USR_CTY> lstEntidad;
                            lstEntidad = lista2.OResultado;
                            if (decIdUsuario > 0 )//&& lstEntidad!= null)
                            {
                                //lstEntidad = lstEntidad.OrderBy(x => x.DENOMINACION_PADRE).ThenBy(x => x.ACTIVIDAD).ThenBy(x => x.DENOMINACION).ToList();
                                //CMessageBoxManager.ShowMessageDialog(
                                //        "Disculpe las molestias, no se pudo registrada la información ingresada..", ref datosPersona.Mensaje,
                                //        CMessageBoxManager.ERROR_MESSAGE);
                                Session["lista_entidades"] = lstEntidad;
                                /*datosPersona.Mensaje = 
                                    "El usuario introducido ya tiene entidades asociadas, debe realizar la edicion de perfiles desde el arbol de entidades";
                                
                                wfAdministrarUsuario.CerrarControl();*/
                            /*}
                            else
                            {*/
                                cargaComboExpedicion(persona.ID_TIPO_IDENTIFICACION);

                                //datosPersona.TipoIdentificacion = "1";//TODO: cambiar con los datos del servcio
                                datosPersona.TipoIdentificacion = persona.ID_TIPO_IDENTIFICACION.ToString();
                                datosPersona.TipoIdentificacionReadOnly = true;

                                datosPersona.Expedicion = persona.EXPEDICION_ID.ToString();
                                datosPersona.ExpedicionReadOnly = true;

                                datosPersona.Complemento = persona.COMPLEMENTO == "0" ? "" : persona.COMPLEMENTO;
                                datosPersona.ComplementoReadOnly = true;

                                datosPersona.Nombres = persona.NOMBRE;
                                datosPersona.NombresReadOnly = true;

                                datosPersona.PrimerApellido = persona.PRIMER_APELLIDO;
                                datosPersona.PrimerApellidoReadOnly = true;

                                datosPersona.SegundoApellido = persona.SEGUNDO_APELLIDO;
                                datosPersona.SegundoApellidoReadOnly = true;

                                DateTime dateFechaNacimiento = CFechas.ConvierteLongDateTime(persona.FECHA_NACIMIENTO);
                                datosPersona.FechaNacimiento = dateFechaNacimiento;
                                datosPersona.FechaNacimientoReadOnly = dateFechaNacimiento.ToShortDateString() !=
                                                                       "01/01/1900";

                                datosPersona.Genero = persona.GENERO_ID.ToString();
                                datosPersona.GeneroReadOnly = true;

                                datosPersona.Email = persona.EMAIL;
                                datosPersona.EmailReadOnly = !string.IsNullOrEmpty(persona.EMAIL);

                                datosPersona.ComplementoRemarcar = false;
                                datosPersona.Mensaje = "El usuario introducido ya existe, asocie las entidades que desea asignarle al mismo";
                                Session["usuario_registrado"] = decIdUsuario;
                                //datosPersona.Titulo = decIdUsuario.ToString();
                            }
                            ViewState[_viewStateIdPersona] = persona.ID_PERSONA;
                            //ViewState[_viewStateIdUsuario] = decIdUsuario;

                        }
                    }
                    else
                    {
                        datosPersona.Nombres = "";
                        datosPersona.PrimerApellido = "";
                        datosPersona.SegundoApellido = "";
                        datosPersona.Complemento = "";
                        datosPersona.FechaNacimiento = Convert.ToDateTime("01/01/1900");
                        datosPersona.Email = "";
                        datosPersona.TipoIdentificacionReadOnly = false;
                        datosPersona.NroIdentificacionReadOnly = false;
                        datosPersona.ExpedicionReadOnly = false;
                        datosPersona.ComplementoReadOnly = false;
                        datosPersona.NombresReadOnly = false;
                        datosPersona.PrimerApellidoReadOnly = false;
                        datosPersona.SegundoApellidoReadOnly = false;
                        datosPersona.GeneroReadOnly = false;
                        datosPersona.FechaNacimientoReadOnly = false;
                        datosPersona.EmailReadOnly = false;
                        datosPersona.ComplementoRemarcar = true;
                        datosPersona.Mensaje = "Al haber encontrado un registro previo con el mismo Documento de Identidad, se recomienda introducir el Nro. de Complemento";
                        ViewState.Add(_viewStateIdPersona, 0);
                        Session["usuario_registrado"] = null;
                    }
                    panelPersona.Visible = false;
                    break;
            }
        }

        private void cargaComboExpedicion(decimal decIdTipoIdentificacion)
        {
            if (_servicioParametricas != null)
            {
                if (decIdTipoIdentificacion == 1)
                {
                    List<O_PARAMETRICA_CTY> oParametricaCtys =
                        _servicioParametricas.listadoDepartamentos(CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                    datosPersona.DataSourceDepartamentos = oParametricaCtys;
                }
                else
                {
                    if (decIdTipoIdentificacion == 2)
                    {
                        List<O_PARAMETRICA_CTY> oParametricaCtys =
                            _servicioParametricas.listadoPaises(CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                        foreach (var oParametricaCty in oParametricaCtys)
                        {
                            if (oParametricaCty.NOMBRE == "BOLIVIA")
                            {
                                oParametricaCtys.Remove(oParametricaCty);
                                break;
                            }
                        }
                        datosPersona.DataSourceDepartamentos = oParametricaCtys;
                    }
                    else
                    {
                        List<O_PARAMETRICA_CTY> oParametricaCtys =
                            _servicioParametricas.listadoPaises(CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                        datosPersona.DataSourceDepartamentos = oParametricaCtys;
                    }
                }
            }
        }

        #endregion

        #region Eventos del Control datosPersona

        public event EventHandler NroIdentificacionTextChanged;

        protected void nroIdentificacionTextChanged(object sender, EventArgs e)
        {
            EventHandler handler = NroIdentificacionTextChanged;
            if (handler != null)
            {
                handler(sender, e);
                if (_servicioHydroConsultas != null)
                {
                    List<O_PERSONA_CTY> personas =
                        _servicioHydroConsultas.ObtenerPersonaPorCI(CParametrosHydro.StrCredencialEmpadronamiento,
                            datosPersona.NroIdentificacion, ref _mensajeError);

                    if (personas != null && personas.Count > 0)
                    {
                        personas.Add(new O_PERSONA_CTY() { CI = "NINGUNO", NOMBRE_COMPLETO = "NO ESTA EN LA LISTA", ID_PERSONA = 0, EXPEDICION = "", COMPLEMENTO = "" });
                        dtgPersonas.DataSource = personas;
                        dtgPersonas.DataBind();
                        panelPersona.Visible = true;
                        ViewState[_viewStatePersonas] = personas;
                    }
                    else
                    {
                        datosPersona.Nombres = "";
                        datosPersona.PrimerApellido = "";
                        datosPersona.SegundoApellido = "";
                        datosPersona.Complemento = "";
                        datosPersona.FechaNacimiento = Convert.ToDateTime("01/01/1900");
                        datosPersona.Email = "";

                        datosPersona.TipoIdentificacionReadOnly = false;
                        datosPersona.NroIdentificacionReadOnly = false;
                        datosPersona.ExpedicionReadOnly = false;
                        //datosPersona.ComplementoReadOnly = false;
                        datosPersona.NombresReadOnly = false;
                        datosPersona.PrimerApellidoReadOnly = false;
                        datosPersona.SegundoApellidoReadOnly = false;
                        datosPersona.GeneroReadOnly = false;
                        datosPersona.FechaNacimientoReadOnly = false;
                        datosPersona.EmailReadOnly = false;

                        datosPersona.ComplementoRemarcar = false;
                        datosPersona.Mensaje = "";

                        ViewState.Add(_viewStateIdPersona, 0);
                        Session["usuario_registrado"] = null;
                    }
                }
            }
        }

        public event EventHandler TipoPersonaSelectedIndexChanged;

        protected void tipoPersonaSelectedIndexChanged(object sender, EventArgs e)
        {
            EventHandler handler = TipoPersonaSelectedIndexChanged;
            if (handler != null)
            {
                handler(sender, e);
                if (_servicioParametricas != null)
                {
                    if (datosPersona.TipoIdentificacion == "1" || datosPersona.TipoIdentificacion == "4")
                    {
                        List<O_PARAMETRICA_CTY> oParametricaCtys =
                            _servicioParametricas.listadoDepartamentos(CParametrosHydro.StrCredencialEmpadronamiento,
                                ref _mensajeError);
                        datosPersona.DataSourceDepartamentos = oParametricaCtys;
                    }
                    else
                    {
                        if (datosPersona.TipoIdentificacion == "2" )
                        {
                            List<O_PARAMETRICA_CTY> oParametricaCtys =
                                _servicioParametricas.listadoPaises(CParametrosHydro.StrCredencialEmpadronamiento,
                                    ref _mensajeError);
                            foreach (var oParametricaCty in oParametricaCtys)
                            {
                                if (oParametricaCty.NOMBRE == "BOLIVIA")
                                {
                                    oParametricaCtys.Remove(oParametricaCty);
                                    break;
                                }
                            }
                            datosPersona.DataSourceDepartamentos = oParametricaCtys;
                        }
                        else
                        {
                            List<O_PARAMETRICA_CTY> oParametricaCtys =
                                _servicioParametricas.listadoPaises(CParametrosHydro.StrCredencialEmpadronamiento,
                                    ref _mensajeError);
                            datosPersona.DataSourceDepartamentos = oParametricaCtys;
                        }
                    }
                }
            }
        }

        #endregion

        #region Formato de Presentacion del Control

        private int _width;
        private string _titulo;
        private bool _soloCI;

        [Category("Formato de Presentancion del Control")]
        public int Width
        {
            get { return _width; }
            set
            {
                if (value > 500)
                {
                    datosPersona.Width = value;
                }
            }
        }

        [Category("Formato de Presentancion del Control")]
        public string Titulo
        {
            set
            {
                _titulo = value;
                datosPersona.Titulo = value;
            }
        }

        [Category("Formato de Presentancion del Control")]
        public bool SoloCI
        {
            get { return _soloCI; }
            set
            {
                _soloCI = value;
                datosPersona.SoloCI = _soloCI;
            }
        }

        #endregion   
    }

    public class EResultadoEntidad
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_OBTIENE_ENTIDADES_USR_CTY> OResultado { get; set; }
    }
}
