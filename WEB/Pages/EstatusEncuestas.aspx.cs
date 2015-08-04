using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.CatalogoService;
using WEB.EncuestaService;
using BAL;
using System.Threading;
using System.Web.Services;
using Tickets.Layouts;

namespace WEB.Pages
{
    public partial class EstatusEncuestas : System.Web.UI.Page //PageHelper
    {
        protected static string usuario;
        protected static int usuarioID;
        protected static ResponseLogin UsuarioObj;
        private static int UsuarioID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioID"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
            else
            {
                using (CatalogoServicesClient client = new CatalogoServicesClient())
                {
                    if (!Page.IsPostBack)
                    {
                        // Cargamos los Usuarios
                        ddlUsuarios.DataSource = client.CargarCatalogoWCF("Usuarios");
                        ddlUsuarios.DataTextField = "Etiqueta";
                        ddlUsuarios.DataValueField = "Id";
                        ddlUsuarios.DataBind();
                        ddlUsuarios.Items.Insert(0, new ListItem("[Seleccione uno...]", ""));
                    }
                }
            }
        }

        [WebMethod]
        public static string ObtenerEncuestasPorUsuario(int UsuarioID)
        {
            Thread.Sleep(1000);
            string tabla = "";
            double Porcentaje = 0;
            try
            {
                List<Encuesta> encuestas = new List<Encuesta>();
                using (EncuestaServiceClient client = new EncuestaServiceClient())
                {
                    encuestas = client.ListarEstatusEncuestasPorUsuario(UsuarioID);

                    foreach (Encuesta row in encuestas)
                    {
                        Porcentaje = (Double.Parse(row.TotalRespuestas.ToString()) / Double.Parse(row.TotalPreguntas.ToString())) * 100;
                        tabla += "<tr>";
                        tabla += "<td>" + row.NombreProyecto + "</td>";
                        tabla += "<td>" + row.NombreFormulario + "</td>";
                        tabla += "<td style=\"text-align: center\">" + row.TotalPreguntas + "</td>";
                        tabla += "<td style=\"text-align: center\">" + row.TotalRespuestas + "</td>";
                        tabla += "<td>";
                        tabla += "<div class=\"progress progress-striped pos-rel\" data-percent=\"" + Porcentaje + "%\">";
                        tabla += "<div class=\"progress-bar progress-bar-blue\" style=\"width:" + Porcentaje + "%;\"></div>";
                        tabla += "</div>";
                        tabla += "</td>";
                        tabla += "<td>" + (row.Activo ? "<span class=\"label label-sm label-success\">Activo</span>" : "<span class=\"label label-sm label-danger\">Completado</span>") + "</td>";
                        
                        tabla += "</tr>";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return tabla;
        }
    }
}