using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using WEB.EncuestaService;
using BAL;

namespace WEB.Pages
{
    public partial class DatosUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioID"] != null)
            {
                UsuarioEncuesta Usuario = new UsuarioEncuesta();
                try
                {
                    int UsuarioID = Int32.Parse(Session["UsuarioID"].ToString());
                    using (EncuestaServiceClient client = new EncuestaServiceClient())
                    {
                        Usuario = client.ObtenerUsuario(UsuarioID);
                    }

                    Session["EsAdministrador"] = Usuario.EsAdministrador;                    

                    if (!Page.IsPostBack)
                    {
                        txtCorreoElectronico.Text = Usuario.CorreoElectronico;
                        txtPrimerNombre.Text = Usuario.PrimerNombre;
                        txtSegundoNombre.Text = Usuario.SegundoNombre;
                        txtPrimerApellido.Text = Usuario.PrimerApellido;
                        txtSegundoApellido.Text = Usuario.SegundoApellido;
                        txtNombreEntidad.Text = Usuario.NombreEntidad;
                        txtNombreProyecto.Text = Usuario.NombreProyecto;
                        txtCargoFuncionario.Text = Usuario.CargoFuncionario;
                    }                    
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
        }

        protected void btnGuardarInfoUsuario_Click(object sender, EventArgs e)
        {
            int n = 0;
            int UsuarioID = Int32.Parse(Session["UsuarioID"].ToString());
            try
            {
                string CorreoElectronico = txtCorreoElectronico.Text;
                string PrimerNombre = txtPrimerNombre.Text;
                string SegundoNombre = txtSegundoNombre.Text;
                string PrimerApellido = txtPrimerApellido.Text;
                string SegundoApellido = txtSegundoApellido.Text;
                string NombreEntidad = txtNombreEntidad.Text;
                string NombreProyecto = txtNombreProyecto.Text;
                string CargoFuncionario = txtCargoFuncionario.Text;

                using (EncuestaServiceClient client = new EncuestaServiceClient())
                {
                    n = client.ActualizarUsuarioEncuesta(UsuarioID, PrimerNombre, SegundoNombre, PrimerApellido,
                                                         SegundoApellido, NombreEntidad, NombreProyecto, CargoFuncionario);

                    Session["UsuarioID"] = UsuarioID.ToString();
                    Response.Redirect("~/Pages/MisEncuestas.aspx");
                }
                
            }
            catch (Exception ex)
            {

            }
        }
    }
}