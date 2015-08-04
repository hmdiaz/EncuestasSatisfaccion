<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Registro.Master" AutoEventWireup="true" CodeBehind="DatosUsuario.aspx.cs" Inherits="WEB.Pages.DatosUsuario" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="server">
    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" runat="server">
    <form id="Form1" class="form-horizontal" role="form" runat="server">
        <div class="page-header">
            <center>
                <h1>Completar Datos del Usuario</h1>
            </center>
        </div>
        <asp:ValidationSummary HeaderText="<center><h4>Los siguientes campos son requeridos</h4></center>" CssClass="alert alert-danger" ID="ValidationSummary1" runat="server" ValidationGroup="1" />
		<div class="clear"></div>
        <div class="form-group">
			<label class="col-sm-3 control-label no-padding-right" for="form-field-1">Correo Electrónico</label>
			<div class="col-sm-9">
                <asp:TextBox ReadOnly="true" ID="txtCorreoElectronico" runat="server" placeholder="Correo Electrónico..." CssClass="col-xs-10 col-sm-5 form-input-readonly"></asp:TextBox>
			</div>
		</div>

		<div class="form-group">
			<label class="col-sm-3 control-label no-padding-right" for="form-field-1">Primer Nombre</label>
			<div class="col-sm-9">
                <asp:TextBox ID="txtPrimerNombre" runat="server" placeholder="Primer Nombre..." CssClass="col-xs-10 col-sm-5"></asp:TextBox>
			    &nbsp;&nbsp;<asp:RequiredFieldValidator ControlToValidate="txtPrimerNombre" ValidationGroup="1" ID="RequiredFieldValidator2" Text="<span style='color:red'>(*)</span>" runat="server" ErrorMessage="(*) El Primer Nombre es requerido"></asp:RequiredFieldValidator>
            </div>
		</div>

        <div class="form-group">
			<label class="col-sm-3 control-label no-padding-right" for="form-field-1">Segundo Nombre</label>
			<div class="col-sm-9">
                <asp:TextBox ID="txtSegundoNombre" runat="server" placeholder="Segundo Nombre..." CssClass="col-xs-10 col-sm-5"></asp:TextBox>
			</div>
		</div>

        <div class="form-group">            
			<label class="col-sm-3 control-label no-padding-right" for="form-field-1">Primer Apellido</label>
			<div class="col-sm-9">
                <asp:TextBox ID="txtPrimerApellido" runat="server" placeholder="Primer Apellido..." CssClass="col-xs-10 col-sm-5"></asp:TextBox>
			    &nbsp;&nbsp;<asp:RequiredFieldValidator ControlToValidate="txtPrimerApellido" ValidationGroup="1" ID="RequiredFieldValidator1" Text="<span style='color:red'>(*)</span>" runat="server" ErrorMessage="(*) El Primer Apellido es requerido"></asp:RequiredFieldValidator>
            </div>            
		</div>

        <div class="form-group">
			<label class="col-sm-3 control-label no-padding-right" for="form-field-1">Segundo Apellido</label>
			<div class="col-sm-9">
                <asp:TextBox ID="txtSegundoApellido" runat="server" placeholder="Segundo Apellido..." CssClass="col-xs-10 col-sm-5"></asp:TextBox>
			</div>
		</div>

        <div class="form-group">
			<label class="col-sm-3 control-label no-padding-right" for="form-field-1">Nombre del Proyecto</label>
			<div class="col-sm-9">
                <asp:TextBox ID="txtNombreProyecto" runat="server" placeholder="Nombre del Proyecto..." CssClass="col-xs-10 col-sm-5"></asp:TextBox>
			    &nbsp;&nbsp;<asp:RequiredFieldValidator ControlToValidate="txtNombreProyecto" ValidationGroup="1" ID="RequiredFieldValidator3" Text="<span style='color:red'>(*)</span>" runat="server" ErrorMessage="(*) El Nombre del Proyecto es requerido"></asp:RequiredFieldValidator>
            </div>
		</div>

        <div class="form-group">
			<label class="col-sm-3 control-label no-padding-right" for="form-field-1">Nombre Entidad</label>
			<div class="col-sm-9">
                <asp:TextBox ID="txtNombreEntidad" runat="server" placeholder="Nombre Entidad..." CssClass="col-xs-10 col-sm-5"></asp:TextBox>
			    &nbsp;&nbsp;<asp:RequiredFieldValidator ControlToValidate="txtNombreEntidad" ValidationGroup="1" ID="RequiredFieldValidator4" Text="<span style='color:red'>(*)</span>" runat="server" ErrorMessage="(*) El Nombre de la Entidad es requerido"></asp:RequiredFieldValidator>
            </div>
		</div>

        <div class="form-group">
			<label class="col-sm-3 control-label no-padding-right" for="form-field-1">Cargo Funcionario</label>
			<div class="col-sm-9">
                <asp:TextBox ID="txtCargoFuncionario" runat="server" placeholder="Cargo Funcionario..." CssClass="col-xs-10 col-sm-5"></asp:TextBox>
			    &nbsp;&nbsp;<asp:RequiredFieldValidator ControlToValidate="txtCargoFuncionario" ValidationGroup="1" ID="RequiredFieldValidator5" Text="<span style='color:red'>(*)</span>" runat="server" ErrorMessage="(*) El Cargo del Funcionario es requerido"></asp:RequiredFieldValidator>
            </div>
		</div>
		<div class="space-4"></div>
        <center>
            <asp:Button ValidationGroup="1" ID="btnGuardarInfoUsuario" CssClass="btn btn-info" runat="server" Text="Guardar Información" onclick="btnGuardarInfoUsuario_Click" />
        </center>
	</form>
</asp:Content>
