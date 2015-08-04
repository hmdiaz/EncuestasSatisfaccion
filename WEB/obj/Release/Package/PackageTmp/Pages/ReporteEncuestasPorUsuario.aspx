<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReporteEncuestasPorUsuario.aspx.cs" Inherits="WEB.Pages.ReporteEncuestasPorUsuario" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="alert alert-info">
            <center>
		        <strong>
                    <i class="ace-icon fa fa-check"></i>
                    Tenga en cuenta que!
                </strong>
            </center>
            En el reporte solo aparecen las encuestas que hayan sido finalizadas.
	    </div>
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="rptRespuestasEncuestaPorUsuario" runat="server" Width="1100" Height="800"></rsweb:ReportViewer>
    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="server">
</asp:Content>
