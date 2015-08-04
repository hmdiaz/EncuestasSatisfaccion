using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Masters
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ltUsuario.Text = Convert.ToString(Session["CorreoElectronico"]);
            PanelAdministrador.Visible = Convert.ToBoolean(Session["EsAdministrador"]);
            PanelReportes.Visible = Convert.ToBoolean(Session["EsAdministrador"]);
        }
    }
}