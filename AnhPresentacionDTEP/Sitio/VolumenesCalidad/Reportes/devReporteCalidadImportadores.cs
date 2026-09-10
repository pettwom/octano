using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.Reportes
{
    public partial class devReporteCalidadImportadores : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly string _user = string.Empty;

        public devReporteCalidadImportadores()
        {
            InitializeComponent();
        }

        public devReporteCalidadImportadores(string user, bool pCosto, bool pVolumenMuestra, bool pMarca, bool pResolucion, bool pNombreProducto)
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
            xrLabel37.Visible = pResolucion;
            xrLabel38.Visible = pResolucion;
            xrLabel18.Visible = pNombreProducto;
            xrLabel19.Visible = pNombreProducto;
       }

        private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {            
            xrLabel30.Text = _user;
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
