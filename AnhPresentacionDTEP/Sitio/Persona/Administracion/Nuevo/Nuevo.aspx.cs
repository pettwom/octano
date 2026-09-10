using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AnhPresentacionDTEP.Sitio.Persona.Administracion.Nuevo
{
    public partial class Nuevo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable listadoNombres = new DataTable();
            listadoNombres.Columns.Add("Nro");
            listadoNombres.Columns.Add("Nombre");
            listadoNombres.Columns.Add("Id Funcionario");
            listadoNombres.Columns.Add("Rol Funcionario");
            listadoNombres.Columns.Add("Editar");
            listadoNombres.Rows.Add("Kein Becil","1234","Admin");
            listadoNombres.Rows.Add("Abran Cogido Rico","4567","Supervisor");
            listadoNombres.Rows.Add("Jesucristo Hitler Montoya", "8910", "Operador");
            listadoNombres.Rows.Add("Rosa Melano", "1112", "Admin");
            listadoNombres.Rows.Add("Soila Cerda", "1314", "Supervisor");
            listadoNombresRoles.DataSource = listadoNombres;
            listadoNombresRoles.DataBind();
        }     

        protected void newTabButton2_Click(object sender, EventArgs e)
        {

        }
    }
}