using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnhPresentacionDTEP.Comun.UControl
{
    public partial class ucMensajeError : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            divMensajeAlerta.Visible = false;
        }
        public void MensajeExito(string mensaje)
        {
            divMensajeAlerta.Attributes.Add("class", "exito");
            divMensajeAlerta.Visible = true;
            lblMensajeError.Text = mensaje;
        }
        public void MensajeAdvertencia(string mensaje)
        {
            divMensajeAlerta.Attributes.Add("class", "alerta");
            divMensajeAlerta.Visible = true;
            lblMensajeError.Text = mensaje;
        }

        public void MensajeInformacion(string mensaje)
        {
            divMensajeAlerta.Attributes.Add("class", "informacion");
            divMensajeAlerta.Visible = true;
            lblMensajeError.Text = mensaje;
        }
        public void MensajeError(string mensaje)
        {
            divMensajeAlerta.Attributes.Add("class", "error");
            divMensajeAlerta.Visible = true;
            lblMensajeError.Text = mensaje;
        }

        public void OcultarMensaje()
        {
            divMensajeAlerta.Visible = true;
            lblMensajeError.Text = "";
        }
    }
}