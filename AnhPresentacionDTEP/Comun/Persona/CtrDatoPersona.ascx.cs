using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI.WebControls;

namespace AnhHydroTalleresOpeGarrafasPresentacion.Controles
{
    public partial class CtrDatoPersona : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Campos de Ingreso de Datos

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
        private string _mensaje;

        [Category("Campos de Ingreso de Datos")]
        public string TipoIdentificacion
        {
            get { return cmbTipoIdentificacion.SelectedValue; }
            set
            {
                if (cmbTipoIdentificacion.SelectedValue != value)
                {
                    cmbTipoIdentificacion.SelectedValue = value;
                    _tipoIdentificacion = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string NroIdentificacion
        {
            get { return txtNroIdentificacion.Text; }
            set
            {
                if (txtNroIdentificacion.Text != value)
                {
                    txtNroIdentificacion.Text = value;
                    _nroIdentificacion = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string Expedicion
        {
            get { return cmbExpedicion.SelectedValue; }
            set
            {
                if (cmbExpedicion.SelectedValue != value)
                {
                    cmbExpedicion.SelectedValue = value;
                    _expedicion = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string Complemento
        {
            get { return txtComplemento.Text; }
            set
            {
                if (txtComplemento.Text != value)
                {
                    txtComplemento.Text = value;
                    _complemento = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string Nombres
        {
            get { return txtNombres.Text; }
            set
            {
                if (txtNombres.Text != value)
                {
                    txtNombres.Text = value;
                    _nombres = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string PrimerApellido
        {
            get { return txtPrimerApellido.Text; }
            set
            {
                if (txtPrimerApellido.Text != value)
                {
                    txtPrimerApellido.Text = value;
                    _primerApellido = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string SegundoApellido
        {
            get { return txtSegundoApellido.Text; }
            set
            {
                if (txtSegundoApellido.Text != value)
                {
                    txtSegundoApellido.Text = value;
                    _segundoApellido = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string Genero
        {
            get { return cmbGenero.SelectedValue; }
            set
            {
                if (cmbGenero.SelectedValue != value)
                {
                    cmbGenero.SelectedValue = value;
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
                    return Convert.ToDateTime(txtFechaNacimiento.Text);
                }
                catch (Exception)
                {
                    return Convert.ToDateTime("01/01/1900");
                }
            }
            set
            {
                if (txtFechaNacimiento.Text != value.ToShortDateString())
                {
                    txtFechaNacimiento.Text = value.ToShortDateString() == "01/01/1900" ? "" : value.ToShortDateString();
                    _fechaNacimiento = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public string Email
        {
            get { return txtEmail.Text; }
            set
            {
                if (txtEmail.Text != value)
                {
                    txtEmail.Text = value;
                    _email = value;
                }
            }
        }

        [Category("Campos de Ingreso de Datos")]
        public bool TipoIdentificacionReadOnly
        {
            get { return cmbTipoIdentificacion.Enabled; }
            set { cmbTipoIdentificacion.Enabled = !value; }
        }

        [Category("Campos de Ingreso de Datos")]
        public bool NroIdentificacionReadOnly
        {
            get { return txtNroIdentificacion.ReadOnly; }
            set { txtNroIdentificacion.ReadOnly = value; }
        }

        [Category("Campos de Ingreso de Datos")]
        public bool ExpedicionReadOnly
        {
            get { return cmbExpedicion.Enabled; }
            set { cmbExpedicion.Enabled = !value; }
        }

        [Category("Campos de Ingreso de Datos")]
        public bool ComplementoReadOnly
        {
            get { return txtComplemento.ReadOnly; }
            set { txtComplemento.ReadOnly = value; }
        }

        [Category("Campos de Ingreso de Datos")]
        public bool NombresReadOnly
        {
            get { return txtNombres.ReadOnly; }
            set { txtNombres.ReadOnly = value; }
        }

        [Category("Campos de Ingreso de Datos")]
        public bool PrimerApellidoReadOnly
        {
            get { return txtPrimerApellido.ReadOnly; }
            set { txtPrimerApellido.ReadOnly = value; }
        }

        [Category("Campos de Ingreso de Datos")]
        public bool SegundoApellidoReadOnly
        {
            get { return txtSegundoApellido.ReadOnly; }
            set { txtSegundoApellido.ReadOnly = value; }
        }

        [Category("Campos de Ingreso de Datos")]
        public bool FechaNacimientoReadOnly
        {
            get { return txtFechaNacimiento.ReadOnly; }
            set { txtFechaNacimiento.ReadOnly = value; }
        }

        [Category("Campos de Ingreso de Datos")]
        public bool GeneroReadOnly
        {
            get { return cmbGenero.Enabled; }
            set { cmbGenero.Enabled = !value; }
        }

        [Category("Campos de Ingreso de Datos")]
        public bool EmailReadOnly
        {
            get { return txtEmail.ReadOnly; }
            set { txtEmail.ReadOnly = value; }
        }

        [Category("Campos de Ingreso de Datos")]
        public string Mensaje
        {
            get { return _mensaje; }
            set
            {
                _mensaje = value;
                lblMensaje.Text = _mensaje;
            }
        }

        #endregion

        #region Fuentes de Datos - Data Sources

        private object _dataSourceDepartamentos;
        private object _dataSourceGeneros;
        private object _dataSourceTiposIdentificacion;

        [Category("Data Sources")]
        public object DataSourceDepartamentos
        {
            get { return _dataSourceDepartamentos; }
            set
            {
                if (_dataSourceDepartamentos != value)
                {
                    _dataSourceDepartamentos = value;
                    cmbExpedicion.DataSource = value;
                    cmbExpedicion.DataBind();
                }
            }
        }

        [Category("Data Sources")]
        public object DataSourceGeneros
        {
            get { return _dataSourceGeneros; }
            set
            {
                if (_dataSourceGeneros != value)
                {
                    _dataSourceGeneros = value;
                    cmbGenero.DataSource = value;
                    cmbGenero.DataBind();
                }
            }
        }

        [Category("Data Sources")]
        public object DataSourceTiposIdentificacion
        {
            get { return _dataSourceTiposIdentificacion; }
            set
            {
                if (_dataSourceTiposIdentificacion != value)
                {
                    _dataSourceTiposIdentificacion = value;
                    cmbTipoIdentificacion.DataSource = value;
                    cmbTipoIdentificacion.DataBind();
                }
            }
        }

        #endregion

        #region Eventos

        [Category("Eventos")]
        [Description("Evento que se envia cuando el texto del campo Nro de Documento cambia.")]
        public event EventHandler NroDocumentoTextChanged;

        protected void txtCi_TextChanged(object sender, EventArgs e)
        {
            EventHandler handler = NroDocumentoTextChanged;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
        [Category("Eventos")]
        [Description("Evento que se envia cuando se selecciona tipo de nacionalidad.")]
        public event EventHandler TipoPersonaSelectedIndexChanged;

        protected void cmbTipoIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            EventHandler handler = TipoPersonaSelectedIndexChanged;
            if (handler != null)
            {
                
                handler(sender, e);
               if(cmbTipoIdentificacion.SelectedIndex == 0)
               {
                   txtNroIdentificacion.Text = "";
                   txtNroIdentificacion.MaxLength = 8;
               }
               else
               {
                   txtNroIdentificacion.Text = "";
                   txtNroIdentificacion.MaxLength = 15;
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
                if (value > 550)
                {
                    _width = value;
                    panelFormulario.Width = value;
                }
            }
        }

        [Category("Formato de Presentancion del Control")]
        public string Titulo
        {
            set
            {
                _titulo = value;
                lblTitulo.Text = value;
            }
        }

        [Category("Formato de Presentancion del Control")]
        public bool SoloCI
        {
            get { return _soloCI; }
            set
            {
                _soloCI = value;
                if (_soloCI)
                {
                    lblIdentificacion.Visible = false;
                    lblIdentificaciona.Visible = false;
                    cmbTipoIdentificacion.Visible = false;
                    lblNumeroCI.Attributes.Remove("width");
                    lblNumeroCI.Text = "Nro. de C.I.";
                    txtNroIdentificacion.Width = Unit.Percentage(100d);
                }
            }
        }

        public bool ComplementoRemarcar
        {
            set
            {
                if (value)
                {
                    txtComplemento.BackColor = Color.PaleGoldenrod;
                }
                else
                {
                    txtComplemento.BackColor = Color.White;
                }
            }
        }

        #endregion
    }
}