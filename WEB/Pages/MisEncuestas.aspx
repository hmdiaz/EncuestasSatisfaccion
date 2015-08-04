<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/MasterPage.Master" AutoEventWireup="true" CodeBehind="MisEncuestas.aspx.cs" Inherits="WEB.Masters.MisEncuestas" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="server">    
    <script src="../Client/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Client/js/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../Client/js/jquery.dataTables.bootstrap.js" type="text/javascript"></script>
    <link href="../Client/CustomStyles/Estilos.css" rel="stylesheet" type="text/css" />    

    <script type="text/javascript">
        $(document).ready(function () {
            $("#divWaiting").hide();
            var tbl = $('#tblEncuestas').dataTable({
                "language": {
                    "lengthMenu": "Mostrando _MENU_ registros por página",
                    "zeroRecords": "No se encontraron registros",
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "infoEmpty": "No se encontraron registros",
                    "infoFiltered": "(Filtrados de _MAX_ total de registros)",
                    "sSearch": "Buscar : ",
                    "sFirst": "Primero",
                    "sLast": "Último",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                },
                "bPaginate": false,
                "bLengthChange": false,
                "bFilter": false,
                "bInfo": false,
                "aoColumnDefs": [{ "bSortable": false, "aTargets": ["_all"]}]
            });

            ObtenerDatosAjax('MisEncuestas.aspx/ObtenerEncuestasPorUsuario', '#body', "{}")
            
            $("#btnGuardarPregunta").on('click', function (e) {
                var frm = $("#FormularioPreguntas").serialize();
            });

        });

        function ObtenerDatosAjax(url, selector, data) {
            $("#divWaiting").show();
            $.ajax({
                type: "POST",
                url: url,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: data,
                success: function (msg) {
                    $(selector).html(msg.d);
                    $("#divWaiting").hide();
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert("Ocurrió un error inesperado al tratar de procesar esta acción: \n" + err.Message);
                }
            });
        }

        function VerPreguntas(EncuestaNo) {
            ObtenerDatosAjax('MisEncuestas.aspx/ObtenerPreguntas', '#Preguntas', "{EncuestaNo: " + EncuestaNo + ", NumeroGrupo: 0}")
        }

        function IrPregunta(EncuestaNo, NumeroGrupo) {
            ObtenerDatosAjax('MisEncuestas.aspx/ObtenerPreguntas', '#Preguntas', "{EncuestaNo: " + EncuestaNo + ", NumeroGrupo: " + NumeroGrupo + "}")
        }

        var NumeroGrupo = 0

        function SiguientePregunta(EncuestaNo, NumeroGrupoLast, Pregunta) {
            var msgRespuesta = "<div class=\"alert alert-block alert-success\">";
            msgRespuesta +=         "<p>";
            msgRespuesta +=             "<strong>";
            msgRespuesta +=                 "<i class=\"ace-icon fa fa-check\"></i>";
            msgRespuesta +=                 "Señor Interventor!";
            msgRespuesta +=             "</strong><br />";
            msgRespuesta +=             "Ha llegado al Final de las preguntas. Muchas gracias por su participación";
            msgRespuesta +=         "</p>";
            msgRespuesta +=   "</div>";

            $.ajax({
                type: "POST",
                url: 'MisEncuestas.aspx/ObtenerNumeroGrupoSiguiente',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{EncuestaNo: " + EncuestaNo + ", NumeroGrupo: " + NumeroGrupoLast + ", Pregunta: '" + Pregunta + "'}",
                success: function (msg) {
                    if (NumeroGrupoLast != msg.d) {
                        ObtenerDatosAjax('MisEncuestas.aspx/ObtenerPreguntas', '#Preguntas', "{EncuestaNo: " + EncuestaNo + ", NumeroGrupo: " + msg.d + "}")
                    }
                    else if (NumeroGrupoLast == msg.d || msg.d == 0) {
                        $("#divPreguntas").html(msgRespuesta);
                        bootbox.dialog({
                            message: "<span class='bigger-110'>Ha finalizado la encuesta de manera satisfactoria.</span>",
                            buttons:
						    {
						        "success":
							     {
							         "label": "<i class='ace-icon fa fa-check'></i> Aceptar!",
							         "className": "btn-sm btn-success",
							         "callback": function () {
							             //Example.show("great success");
							         }
							     }
						    }
                        });
                    }
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert("Ocurrió un error inesperado al tratar de procesar esta acción: \n" + err.Message);
                }
            });
            return NumeroGrupo;
        }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" runat="server">
    <div id="">
        <div class="page-header">
            <center>
                <h1>Encuestas Disponibles</h1>
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
					<th></th>
				</tr>
			</thead>

			<tbody id="body">
				
            </tbody>
        </table>
    </div>
    <br />
    
    <div id="divPreguntas">
        <div class="alert alert-block alert-success">
		    <p>
                <center>
			        <strong>
				        <i class="ace-icon fa fa-check"></i>
				        Señor Interventor!
			        </strong>
                </center>
			    A continuación realizaremos algunas preguntas que nos permitirán conocer su percepción de nuestro cumplimiento con respecto a los requisitos establecidos durante el desarrollo del Contrato.
		    </p>
	    </div>
        <div class="btn-group" id="ListadoPreguntas"></div>
        <div class="widget-box" id="Preguntas">
    
        </div>
    </div>
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
