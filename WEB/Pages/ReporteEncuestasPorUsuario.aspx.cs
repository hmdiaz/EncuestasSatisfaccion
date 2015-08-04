using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Helper;
using Microsoft.Reporting.WebForms;

namespace WEB.Pages
{
    public partial class ReporteEncuestasPorUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Set the processing mode for the ReportViewer to Remote
                rptRespuestasEncuestaPorUsuario.ProcessingMode = ProcessingMode.Remote;
                rptRespuestasEncuestaPorUsuario.ShowParameterPrompts = true;

                ServerReport serverReport = rptRespuestasEncuestaPorUsuario.ServerReport;

                // Set the report server URL and report path
                //serverReport.ReportServerUrl = new Uri("http://10.142.6.57/reportserver");
                serverReport.ReportServerUrl = new Uri(String.Format("{0}/{1}", ConfigurationManager.AppSettings["ReportHost"],
                                                                                ConfigurationManager.AppSettings["ReportServer"]));
                serverReport.ReportPath = ConfigurationManager.AppSettings["ReportPathEncuestasUsuario"];
                Microsoft.Reporting.WebForms.IReportServerCredentials irsc = new ReportAuthHelper
               (ConfigurationManager.AppSettings["ReportAuthUser"], ConfigurationManager.AppSettings["ReportAuthPwd"], ConfigurationManager.AppSettings["ReportDomain"]);
                rptRespuestasEncuestaPorUsuario.ServerReport.ReportServerCredentials = irsc;

            }
        }
    }
}