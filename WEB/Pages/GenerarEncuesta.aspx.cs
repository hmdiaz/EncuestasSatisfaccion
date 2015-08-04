using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.EncuestaService;
using WEB.CatalogoService;
using System.Web.Services;
using System.Threading;
using AztecaClientV2;
using Tickets.Layouts;
using System.Configuration;

namespace WEB.Pages
{
    public partial class GenerarEncuesta : PageHelper //System.Web.UI.Page
    {
        private static int UsuarioAuditID = 0;
        protected static string usuario;
        protected static int usuarioID;
        protected static ResponseLogin UsuarioObj;
        private static int FormularioID = Int32.Parse(ConfigurationManager.AppSettings["FormularioID"].ToString());
        private static int ProyectoID = Int32.Parse(ConfigurationManager.AppSettings["ProyectoID"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            PanelResultado.Visible = false;
            if (Session["UsuarioID"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
        }

        [WebMethod]
        public static string GuardarEncuesta(string CorreoElectronico)
        {
            Thread.Sleep(1000);
            string msg = "";
            int n = 0;
            try
            {
                using (EncuestaServiceClient client = new EncuestaServiceClient())
                {
                    n = client.GuardarEncuesta(ProyectoID, FormularioID, CorreoElectronico, UsuarioAuditID);
                }
                if (n == 0)
                    msg = "La Encuesta se generó de manera satisfactoria, se enviará un correo de notificación al usuario asociado";
                else
                    msg = "Hubo algún error al realizar la generación de la encuesta";
            }
            catch (Exception ex)
            {

            }
            return msg;
        }

        protected void btnGenerarEncuesta_Click(object sender, EventArgs e)
        {
            string CorreoElectronico = txtCorreoElectronico.Text;
            Thread.Sleep(1000);
            string msg = "";
            int n = 0;
            try
            {
                using (EncuestaServiceClient client = new EncuestaServiceClient())
                {
                    n = client.GuardarEncuesta(ProyectoID, FormularioID, CorreoElectronico, UsuarioAuditID);
                }
                if (n == 0)
                    msg = "La Encuesta se generó de manera satisfactoria, se enviará un correo de notificación al usuario asociado";
                else if(n == 1000)
                    msg = "No se puede enviar una encuesta a este usuario porque tiene encuestas activas";
                else if (n == 2000)
                    msg = "No se puede enviar una encuesta a este usuario porque ya se le generó una encuesta en el actual periodo";
                else
                    msg = "Hubo algún error al realizar la generación de la encuesta";

                PanelResultado.Visible = true;
                txtCorreoElectronico.Text = "";
                Label1.Text = msg;
            }
            catch (Exception ex)
            {

            }
        }
    }
}