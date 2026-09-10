using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AnhPresentacionDTEP.Sitio.VolumenesCalidad.Reportes
{
    public partial class devReporteVerificacion : DevExpress.XtraReports.UI.XtraReport
    {
        private readonly string _user = string.Empty;

        public devReporteVerificacion()
        {
            InitializeComponent();
        }

        public devReporteVerificacion(string user)
        {
            InitializeComponent();
            _user = user;
        }

        private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel40.Text = _user;
        }
    }
}
