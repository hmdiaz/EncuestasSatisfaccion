using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using WEB.EncuestaService;
using System.Web.Services;
using System.Runtime.Serialization.Json;
using System.Net;
using BAL;
using System.Threading;
using System.Text;
using Tickets.Layouts;
using System.Configuration;

namespace WEB.Masters
{
    public partial class MisEncuestas : System.Web.UI.Page
    {
        protected static string usuario;
        protected static int usuarioID;
        protected static ResponseLogin UsuarioObj;
        private static int UsuarioID = 0;
        private static int FormularioID = Int32.Parse(ConfigurationManager.AppSettings["FormularioID"].ToString());
        private static int ProyectoID = Int32.Parse(ConfigurationManager.AppSettings["ProyectoID"].ToString());

        protected void Page_Init(object sender, EventArgs e)
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["UsuarioID"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
            else
            {
                UsuarioID = Int32.Parse(Session["UsuarioID"].ToString());
            }
        }
        
        [WebMethod]
        public static string ObtenerEncuestasPorUsuario()
        {
            Thread.Sleep(1000);
            string tabla = "";
            double Porcentaje = 0;
            try
            {
                List<Encuesta> encuestas = new List<Encuesta>();
                using (EncuestaServiceClient client = new EncuestaServiceClient())
                {
                    encuestas = client.ListarEncuestasPorUsuario(UsuarioID);
                    if (encuestas.Count > 0)
                    {
                        foreach (Encuesta row in encuestas)
                        {
                            if ((row.TotalRespuestas / row.TotalPreguntas) < 1)
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
                                tabla += "<td>";

                                tabla += "<div class=\"hidden-sm hidden-xs action-buttons\">";
                                tabla += "<a class=\"tooltip-info blue\" title=\"Presione aquí para comenzar la encuesta\" href=\"javascript:VerPreguntas(" + row.EncuestaID + ")\" id=\"VerPreguntas\">";
                                tabla += "<i class=\"ace-icon fa fa-bar-chart-o bigger-130\"></i>";
                                tabla += "</a>";
                                tabla += "</div>";

                                tabla += "</td>";
                                tabla += "</tr>";
                            }                            
                        }
                    }
                    else
                    {
                        tabla += "<tr>";
                        tabla += "<td colspan=\"7\"><center><em>No tienen encuestas asignadas para responder</em></center></td>";
                        tabla += "</tr>";
                    }
                }
            }
            catch (Exception ex) { 
            
            }
            return tabla;
        }

        [WebMethod]
        public static string ObtenerListadoPreguntasDisponibles(int EncuestaNo)
        {
            Thread.Sleep(1000);
            string html = "";
            try
            {
                List<GrupoPregunta> listado = new List<GrupoPregunta>();
                using (EncuestaServiceClient client = new EncuestaServiceClient())
                {
                    html += "<button data-toggle=\"dropdown\" class=\"btn btn-primary btn-white dropdown-toggle\">";
                    html +=     "Ir a Pregunta";
			        html +=     "<i class=\"ace-icon fa fa-angle-down icon-on-right\"></i>";
		            html += "</button>";
		            html += "<ul class=\"dropdown-menu\">";			        
                    listado = client.ListarPreguntasDisponibles(FormularioID, EncuestaNo);
                    foreach (GrupoPregunta row in listado)
                    {
                        html += "<li>";
                        html +=     "<a href=\"javascript:IrPregunta('" + row.FormularioID + "', '" + row.NumeroGrupo + "')\">" + row.NombreGrupoPregunta + "</a>";
                        html += "</li>";
                    }
                    html += "</ul>";
                }
            }
            catch (Exception ex) 
            { 
            
            }
            return html;
        }

        [WebMethod]
        public static int ObtenerNumeroGrupoSiguiente(int EncuestaNo, int NumeroGrupo, string Pregunta = "S")
        {
            int NextValue = 0, LastIndex = 0, lenght = 0;
            try
            {
                List<GrupoPregunta> listado = new List<GrupoPregunta>();
                using (EncuestaServiceClient client = new EncuestaServiceClient())
                {                    
                    listado = client.ListarPreguntasDisponibles(FormularioID, EncuestaNo);
                    lenght = listado.Count;
                    LastIndex = listado.FindIndex(s => s.NumeroGrupo == NumeroGrupo);

                    if (Pregunta == "S" && ((LastIndex + 1) <= (lenght - 1)) && LastIndex != -1) // Pregunta Siguiente
                    {
                        // Obtiene el número de grupo anterior
                        NextValue = listado[LastIndex + 1].NumeroGrupo;                        
                    }
                    else if (Pregunta == "A" && ((LastIndex - 1) <= (lenght - 1)) && LastIndex != -1) // Pregunta Anterior
                    {
                        // Obtiene el número de grupo siguiente
                        NextValue = listado[LastIndex - 1].NumeroGrupo;
                    }
                    else if (LastIndex == -1)
                    {
                        // Si el grupo no existe seleccionamos la primera por defecto
                        NextValue = client.ListarPreguntasDisponibles(FormularioID, EncuestaNo).FirstOrDefault().NumeroGrupo;
                    }
                    else
                    {
                        // Si llega al final de las preguntas no pasa a la siguiente sino se mantiene en la misma
                        NextValue = NumeroGrupo;                        
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return NextValue;
        }

        [WebMethod]
        public static string ObtenerPreguntas(int EncuestaNo, int NumeroGrupo = 0)
        {
            Thread.Sleep(2000);
            string html = "";
            try
            {
                List<Formulario> listado = new List<Formulario>();
                using (EncuestaServiceClient client = new EncuestaServiceClient())
                {
                    int Pregunta = 0;
                    // Obtenemos la primera pregunta de las que estan disponibles

                    if (NumeroGrupo == 0)
                    {
                        Pregunta = client.ListarPreguntasDisponibles(FormularioID, EncuestaNo).FirstOrDefault().NumeroGrupo;
                    }
                    else 
                    {
                        Pregunta = NumeroGrupo;
                    }            

                    listado = client.ObtenerPreguntas(FormularioID, Pregunta);
                    html += "<script src='../Client/CustomScripts/CustomScripts.js' type'text/javascript'></script>";

                    html += "<form id=\"FormularioPreguntas\">";
                    html +=         "<input id=\"PreguntaID\" type=\"hidden\" value=\"" + listado.FirstOrDefault().PreguntaID + "\">";
                    html +=         "<input id=\"TipoPreguntaID\" type=\"hidden\" value=\"" + listado.FirstOrDefault().TipoPreguntaID + "\">";
                    html +=         "<input id=\"ProyectoID\" type=\"hidden\" value=\"8\">";
                    html +=         "<input id=\"FormularioID\" type=\"hidden\" value=\"" + listado.FirstOrDefault().FormularioID + "\">";
                    html +=         "<input id=\"EncuestaNo\" type=\"hidden\" value=\"" + EncuestaNo + "\">";
                    html +=         "<div class=\"widget-header\">";
                    html +=             "<h4 class=\"widget-title\">" + listado.FirstOrDefault().NumeroGrupo + " - " +listado.FirstOrDefault().NombreGrupo + "</h4>";
                    html +=         "</div>";
                    html +=         "<div class=\"widget-body\" id=\"Pregunta\">";
                    html +=             "<div class=\"widget-main no-padding\">";
                    html +=                 "<form>";
                    html +=                     "<fieldset>";
                    html +=                         "<div class=\"col-xs-8 col-sm-4\">";
                    html +=                             "<div class=\"control-group\">";

                    for (int i = 0; i < listado.Count() - 1; i++)
                    {
                        html +=                             "<div class=\"radio\">";
                        html +=                                 "<label>";
                        html +=                                     "<input id=\"rbOpcion_" + listado[i].OpcionID + "\" name=\"rbOpcion\" type=\"radio\" class=\"ace\" value=\"" + listado[i].OpcionID + "\" />";
                        html +=                                     "<span class=\"lbl\">" + listado[i].TextoOpcion + "</span>";
                        html +=                                 "</label>";
                        html +=                             "</div>";
                    }

                    html +=                             "</div>";
                    html +=                         "</div>";
                    html +=                     "<div>";
                    html +=                     "<label for=\"form-field-9\">Observaciones</label>";
                    html +=                     "<div class=\"form-inline\">";
                    html +=                         "<textarea cols=\"95\" rows=\"4\" class=\"form-control limited\" id=\"txtObservaciones\" name=\"txtObservaciones\" maxlength=\"2000\"></textarea>";
                    html +=                         "<input id=\"PreguntaObservacionID\" type=\"hidden\" value=\"" + listado.LastOrDefault().PreguntaID + "\">";
                    html +=                         "<input id=\"OpcionObservacionID\" type=\"hidden\" value=\"1\">";
                    html +=                         "<input id=\"TipoPreguntaObservacionID\" type=\"hidden\" value=\"" + listado.LastOrDefault().TipoPreguntaID + "\">";
                    html +=                     "</div>";                    
                    html +=                 "</div>";
                    html +=             "</fieldset>";
                    html +=             "<div class=\"form-actions center\">";
                    //html +=             "<button type=\"button\" class=\"btn btn-sm btn-success\" onclick=\"SiguientePregunta('" + EncuestaNo + "', '" + listado.FirstOrDefault().NumeroGrupo + "', 'A')\">";
                    //html +=                 "<i class=\"ace-icon fa fa-arrow-left icon-on-right bigger-110\"></i> Anterior";
                    //html +=             "</button>&nbsp;";
                    html +=             "<button type=\"button\" class=\"btn btn-sm btn-info\" id=\"btnGuardarPregunta\">";
                    html +=                 "Guardar esta pregunta <i class=\"ace-icon fa fa-save bigger-110\"></i>";
                    html +=             "</button>&nbsp;";
                    html +=                 "<button type=\"button\" class=\"btn btn-sm btn-success\" id=\"btnFinalizarEncuesta\">";
                    html +=             "Finalizar Encuesta <i class=\"ace-icon fa fa-check bigger-110\"></i>";
                    html +=             "</button>&nbsp;";
                    //html +=             "<button type=\"button\" class=\"btn btn-sm btn-success\" onclick=\"SiguientePregunta('" + EncuestaNo + "', '" + listado.FirstOrDefault().NumeroGrupo + "', 'S')\">";
                    //html +=                 "Siguiente <i class=\"ace-icon fa fa-arrow-right icon-on-right bigger-110\"></i>";
                    //html +=             "</button>";
                    html +=         "</div>";
                    html +=     "</form>";
                    html +=   "</div>";
                    html += "</div>";
                    html += "</form>";   
                }
            }
            catch (Exception ex)
            {

            }
            return html;
        }

        [WebMethod]
        public static string GuardarPregunta(int TipoPreguntaID, int PreguntaID, int OpcionID,
                                   int EncuestaNo, string TextoRespuesta = "")
        {
            Thread.Sleep(1000);
            string msg = "";
            int n = 0;
            try
            {
                using (EncuestaServiceClient client = new EncuestaServiceClient())
                {
                    n = client.GuardarPregunta(ProyectoID, FormularioID, TipoPreguntaID, PreguntaID, OpcionID, 
                                   UsuarioID, EncuestaNo, TextoRespuesta);
                }
                if (n == 0)
                    msg = "La Pregunta se guardó de manera satisfactoria";
                else
                    msg = "Hubo algún error al Guardar la Pregunta";
            }
            catch (Exception ex)
            {

            }
            return msg;
        }

        [WebMethod]
        public static string FinalizarEncuesta(int EncuestaNo)
        {
            Thread.Sleep(1000);
            List<Pregunta> preguntas = new List<Pregunta>();
            string listadoPreguntas = "";
            string msg = "";
            int n = 0;
            try
            {
                using (EncuestaServiceClient client = new EncuestaServiceClient())
                {
                    n = client.FinalizarEncuesta(EncuestaNo);
                    preguntas = client.SelectPreguntasFaltantes(EncuestaNo);
                }
                if (n == 0)
                {
                    for (int i = 0; i < preguntas.Count(); i++)
                    {
                        listadoPreguntas += "<strong>" + preguntas[i].NumeroPregunta + "</strong>-" + preguntas[i].TextoPregunta + "<br />";
                    }

                    msg = string.Format("<strong>La encuesta no se puede finalizar. Aun faltan preguntas por responder las siguientes preguntas.</strong> <br /><br /> {0}", listadoPreguntas);
                }
                else
                    msg = "La Encuesta Finalizó de manera satisfactoria.";
            }
            catch (Exception ex)
            {

            }
            return msg;
        }
    }
}