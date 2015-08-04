<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/MasterPage.Master" AutoEventWireup="true" CodeBehind="GenerarEncuesta.aspx.cs" Inherits="WEB.Pages.GenerarEncuesta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="server">
    <script src="../Client/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Client/js/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../Client/js/jquery.dataTables.bootstrap.js" type="text/javascript"></script>
    <script src="../Client/CustomScripts/CustomScripts.js" type="text/javascript"></script>
    <link href="../Client/CustomStyles/Estilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#divWaiting").hide();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <form runat="server" id="frmGenerarEncuesta">
        <div id="">
            <div class="page-header">
                <center>
                    <h1>Enviar Encuesta</h1>
                </center>
            </div>
            <asp:ValidationSummary HeaderText="<center><h4>Los siguientes campos son requeridos</h4></center>" CssClass="alert alert-danger" ID="ValidationSummary1" runat="server" ValidationGroup="1" />
            <table class="table table-striped">
                <tr>
                    <th colspan="5">
                        &nbsp;
                    </th>
                </tr>
                <tr>
                    <td colspan="5">
                        <div class="form-group">
					        <label class="col-sm-3 control-label no-padding-right" for="form-field-1"> Correo Electrónico </label>
					        <div class="col-sm-9">
                                <asp:TextBox ID="txtCorreoElectronico" runat="server" placeholder="Correo Electrónico ..." CssClass="col-xs-10 col-sm-5"></asp:TextBox>
                                <asp:RegularExpressionValidator 
                                                ValidationGroup="1" 
                                                ID="RegularExpressionValidator1" 
                                                runat="server" 
                                                ControlToValidate="txtCorreoElectronico"
                                                ErrorMessage="(*) El Formato del Correo es Incorrecto. Ejemplo: correo@azteca-comunicaciones.com" 
                                                Text="<span style='color:red'>(*)</span>"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                </asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator 
                                                ValidationGroup="1" 
                                                ID="RequiredFieldValidator1" 
                                                runat="server" 
                                                Text="<span style='color:red'>(*)</span>"
                                                ErrorMessage="(*) El Correo Electrónico es requerido."
                                                ControlToValidate="txtCorreoElectronico">                                                
                                </asp:RequiredFieldValidator>
					        </div>
				        </div>                                   
                    </td>
                </tr>                
                <tr>
                    <td colspan="5">
                        <asp:Button ID="btnGenerarEncuesta" runat="server" Text="Enviar Encuesta" 
                            CssClass="btn btn-info" onclick="btnGenerarEncuesta_Click" ValidationGroup="1" />
                    </td>
                </tr>
            </table>

       </div>
       <asp:Panel ID="PanelResultado" runat="server" CssClass="alert alert-success">
            <button type="button" class="close" data-dismiss="alert">
				<i class="ace-icon fa fa-times"></i>
			</button>            
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
       </asp:Panel>
   </form>
   <div class="divWaiting" id="divWaiting">	                        
	    <asp:Image ID="imgWaitBuscar" 
                    runat="server" 
	                ImageAlign="AbsMiddle" 
                    ImageUrl="~/Images/Loaders/image_717505.gif" />
                    <br />
        <asp:Label ID="lblWaitExperiencia" 
                    runat="server" 
	                Text=" Por favor espere la información esta siendo procesada... " />
    </div>
</asp:Content>
