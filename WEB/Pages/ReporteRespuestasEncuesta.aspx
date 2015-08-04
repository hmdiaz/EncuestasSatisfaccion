<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReporteRespuestasEncuesta.aspx.cs" Inherits="WEB.Pages.ReporteRespuestasEncuesta" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" runat="server">
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="rptRespuestasEncuesta" runat="server" Width="1100" Height="800"></rsweb:ReportViewer>
    </form>
</asp:Content>

