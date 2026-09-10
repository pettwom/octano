using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit.Fields;
using ServiceStack.ServiceHost;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.Reportes
{
    public partial class devReporteCalidadObservados : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly string _user = string.Empty;
        private readonly string _cite = string.Empty;
        private readonly string _producto = string.Empty;
        private readonly string _entidad = string.Empty;
        private readonly string _tanque = string.Empty;
        private readonly string _marca = string.Empty;
        private readonly string _codigoImporte = string.Empty;
        private readonly decimal _volumen;
        private readonly double? _precio;
        private readonly double? _volumenMuestra;
        private readonly string _umVolumen = string.Empty;
        private readonly string _lote = string.Empty;
        private readonly DateTime _fechaOperacion = DateTime.MinValue;
        private readonly DateTime _fechaRegistro = DateTime.MinValue;
        private readonly string _nombreProducto = string.Empty;

        private readonly string _userGenerador = string.Empty;
        
        public devReporteCalidadObservados()
        {
            InitializeComponent();
        }
        public devReporteCalidadObservados(string user, string userGenerador, string cite, string producto, string entidad, string tanque, decimal volumen, string umVolumen, string lote, DateTime fechaOperacion, DateTime fechaRegistro, string pMarca, double? pPrecio, string pCodigoImporte, double? pVolumenMuestra, string pNombreProducto)
        {
            InitializeComponent();
            _user = user;
            _cite = cite;
            _producto = producto;
            _entidad = entidad;
            _tanque = tanque;
            _volumen = volumen;
            _umVolumen = umVolumen;
            _lote = lote;
            _fechaOperacion = fechaOperacion;
            _fechaRegistro = fechaRegistro;
            _userGenerador=userGenerador;
            _marca = pMarca;
            _codigoImporte = pCodigoImporte;
            _precio = pPrecio;
            _volumenMuestra = pVolumenMuestra;
            _nombreProducto = pNombreProducto;
        }

        private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          //  xrLabel30.Text = _user;
        }

      
        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblCite.Text = _cite;
            lblProducto.Text = _producto;
            lblTanque.Text = _tanque;
            lblVolumen.Text = _volumen.ToString();
            lblUmVolumen.Text = _umVolumen;
            lblLote.Text = _lote;
            lblFechaOp.Text = _fechaOperacion.ToString("dd/MM/yyyy");
            lblFechaReg.Text = _fechaRegistro.ToString("dd/MM/yyyy");
            lblUser.Text = _user;
            lblEntidad.Text = _entidad;
            lblUsuario.Text = _userGenerador;
            if (_precio != null)
            {
                lblPrecio.Text = Convert.ToString(_precio);
                lblCodigoPrecio.Text = _codigoImporte;
                lblPrecio.Visible = true;
                lblCodigoPrecio.Visible = true;
                xrLabel17.Visible = true;
            }

            if (_marca != null)
            {
                lblMarca.Text = _marca;
                lblMarca.Visible = true;
                xrLabel35.Visible = true;                
            }

            if (_volumenMuestra != null)
            {
                lblVolumenMuestra.Text = Convert.ToString(_volumenMuestra);
                lblCodigoVolumen.Text = _umVolumen;
                lblVolumenMuestra.Visible = true;
                lblCodigoVolumen.Visible = true;
                xrLabel34.Visible = true;
            }

            if (_nombreProducto != null)
            {
                lblNombreProducto.Text = _nombreProducto;
                lblNombreProducto.Visible = true;
                xrLabel7.Visible = true;
            }
        }

    }
}
