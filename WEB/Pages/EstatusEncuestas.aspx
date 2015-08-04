<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/MasterPage.Master" AutoEventWireup="true" CodeBehind="EstatusEncuestas.aspx.cs" Inherits="WEB.Pages.EstatusEncuestas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="server">
    <script src="../Client/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Client/js/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../Client/js/jquery.dataTables.bootstrap.js" type="text/javascript"></script>
    <script src="../Client/CustomScripts/CustomScripts.js" type="text/javascript"></script>
    <link href="../Client/CustomStyles/Estilos.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#divWaiting").hide();
            $("#cphContenido_ddlUsuarios").on('change', function (e) {
                var UsuarioID = $("#cphContenido_ddlUsuarios").val();
                ObtenerDatosAjax('EstatusEncuestas.aspx/ObtenerEncuestasPorUsuario', '#body', "{UsuarioID: " + UsuarioID + "}")
            })
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <form runat="server">
        <div class="page-header">
            <center>
                <h1>Estatus de Encuestas</h1>
            </center>
        </div>
        <table class="table table-striped">
            <tr>
                <th colspan="5">
                    &nbsp;
                </th>
            </tr>
            <tr>
                <td colspan="5">
                    <div class="form-inline">
                        <div class="input-group col-xs-15">
                            <div class="input-group-addon">
                                <asp:Label ID="Label3" runat="server" Text="Usuario : "></asp:Label>
                            </div>
                            <asp:DropDownList ID="ddlUsuarios" runat="server" CssClass="form-control col-xs-15">
                            </asp:DropDownList>
                            <div class="input-group-btn">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ErrorMessage="(*) El Usuario es requerido" Text="(*)" 
                                    ControlToValidate="ddlUsuarios" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </div>                                            
                        </div>                            
                    </div>                                   
                </td>
            </tr>
        </table>
        <div id="divEstatus">
            <div class="page-header">
                <center>
                    <h1>Encuestas del Usuario</h1>
                </center>
            </div>
            <table id="tblEncuestas" class="table table-striped table-bordered table-hover">
			    <thead>
                    <tr>
					    <th style="width: 150px; text-align: center">Proyecto</th>
					    <th style="width: 250px; text-align: center">Formulario</th>
					    <th style="width: 130px; text-align: center">Total Preguntas</th>
					    <th style="width: 120px">Respondidas</th>
                        <th style="text-align: center">Progreso</th>
					    <th style="width: 100px; text-align: center">Estado</th>
				    </tr>
			    </thead>

			    <tbody id="body">
				
                </tbody>
            </table>
        </div>
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
