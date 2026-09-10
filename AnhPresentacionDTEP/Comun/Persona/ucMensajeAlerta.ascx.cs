using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnhHydroTalleresOpeGarrafasPresentacion.Controles
{
    public partial class ucMensajeAlerta : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            divAlertas.Visible = false;
        }
        public void OcultarMensaje()
        {
            divAlertas.Visible = false;
        }
        public void MensajeExito(string mensaje)
        {

            divAlertas.Attributes.Add("class", "alert alert-success alert-dismissible fade in");
            divAlertas.Visible = true;
            lblTipo.Text = "MENSAJE DE ÉXITO:";
            lblMensaje.Text = mensaje;
        }
        public void MensajeAdvertencia(string mensaje)
        {
            divAlertas.Attributes.Add("class", "alert alert-warning alert-dismissible fade in");
            divAlertas.Visible = true;
            lblTipo.Text = "MENSAJE DE ADVERTENCIA:";
            lblMensaje.Text = mensaje;
        }

        public void MensajeInformacion(string mensaje)
        {
            divAlertas.Attributes.Add("class", "alert alert-info alert-dismissible fade in");
            divAlertas.Visible = true;
            lblTipo.Text = "MENSAJE DE INFORMACIÓN:";
            lblMensaje.Text = mensaje;
        }
        public void MensajeError(string mensaje)
        {
            divAlertas.Attributes.Add("class", "alert alert-danger alert-dismissible fade in");
            divAlertas.Visible = true;
            lblTipo.Text = "MENSAJE DE ERROR:";
            lblMensaje.Text = mensaje;
        }
    }
}