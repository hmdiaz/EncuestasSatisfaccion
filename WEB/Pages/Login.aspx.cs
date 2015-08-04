using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Threading;
using WEB.EncuestaService;

namespace WEB.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PanelResultado.Visible = false;
        }

        [WebMethod]

        protected void Button1_Click(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            string msg = "";
            int UsuarioID = 0;
            try
            {
                string CorreoElectronico = txtCorreoElectronico.Text;
                using (EncuestaServiceClient client = new EncuestaServiceClient())
                {
                    UsuarioID = client.VerificarUsuario(CorreoElectronico);
                }
                if (UsuarioID == 0)
                {
                    PanelResultado.Visible = true;
                }
                else
                {
                    Session["UsuarioID"] = UsuarioID.ToString();
                    Session["CorreoElectronico"] = CorreoElectronico;
                    Response.Redirect("~/Pages/DatosUsuario.aspx");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}