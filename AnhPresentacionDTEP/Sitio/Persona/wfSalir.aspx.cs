using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnhPresentacionDTEP.Sitio.Persona
{
    public partial class wfSalir : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx");
        }
    }
}