using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Helper;
using System.Configuration;
using Microsoft.Reporting.WebForms;

namespace WEB.Pages
{
    public partial class ReporteRespuestasEncuesta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Set the processing mode for the ReportViewer to Remote
                rptRespuestasEncuesta.ProcessingMode = ProcessingMode.Remote;
                rptRespuestasEncuesta.ShowParameterPrompts = false;

                ServerReport serverReport = rptRespuestasEncuesta.ServerReport;

                // Set the report server URL and report path
                //serverReport.ReportServerUrl = new Uri("http://10.142.6.57/reportserver");
                serverReport.ReportServerUrl = new Uri(String.Format("{0}/{1}", ConfigurationManager.AppSettings["ReportHost"],
                                                                                ConfigurationManager.AppSettings["ReportServer"]));
                serverReport.ReportPath = ConfigurationManager.AppSettings["ReportPathSabanaEncuestas"];
                Microsoft.Reporting.WebForms.IReportServerCredentials irsc = new ReportAuthHelper
               (ConfigurationManager.AppSettings["ReportAuthUser"], ConfigurationManager.AppSettings["ReportAuthPwd"], ConfigurationManager.AppSettings["ReportDomain"]);
                rptRespuestasEncuesta.ServerReport.ReportServerCredentials = irsc;

            }
        }
    }
}