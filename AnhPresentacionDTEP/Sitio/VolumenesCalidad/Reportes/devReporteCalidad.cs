using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.Reportes
{
    public partial class devReporteCalidad : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly string _user = string.Empty;

        public devReporteCalidad()
        {
            InitializeComponent();
        }

        public devReporteCalidad(string user, bool pCosto, bool pVolumenMuestra, bool pMarca, bool pNombreProducto)
        {
            InitializeComponent();
            _user = user;
            xrLabel16.Visible = pCosto;
            xrLabel17.Visible = pCosto;
            xrLabel34.Visible = pVolumenMuestra;
            xrLabel33.Visible = pVolumenMuestra;
            xrLabel32.Visible = pVolumenMuestra;
            xrLabel35.Visible = pMarca;
            xrLabel36.Visible = pMarca;
            xrLabel13.Visible = pNombreProducto;
            xrLabel24.Visible = pNombreProducto;
        }

        private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel30.Text = _user;
        }
    }
}
