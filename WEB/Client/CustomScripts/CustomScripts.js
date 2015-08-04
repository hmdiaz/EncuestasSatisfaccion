// Función para Obtener Datos Asincronos
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

function AjaxPreguntas(EncuestaNo, PreguntaID, TipoPreguntaID, OpcionID, Observaciones) {
    $("#divWaiting").show();
    // Guardar Pregunta
    $.ajax({
        type: "POST",
        url: 'MisEncuestas.aspx/GuardarPregunta',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{EncuestaNo: " + EncuestaNo + ", PreguntaID: " + PreguntaID + ", TipoPreguntaID: " + TipoPreguntaID + ", OpcionID: " + OpcionID + ", TextoRespuesta: '" + Observaciones + "'}",
        success: function (msg) {
            $("#ResultadoGuardarPregunta").html(msg.d);
            //$("#divWaiting").hide();
            ObtenerDatosAjax('MisEncuestas.aspx/ObtenerEncuestasPorUsuario', '#body', '{}');
            SiguientePregunta(EncuestaNo, NumeroGrupo, 'S');
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert("Ocurrió un error inesperado al tratar de procesar esta acción: \n" + err.Message);
        }
    });
}

$(document).ready(function () {
    $('#btnGuardarPregunta').on('click', function (e) {
        var PreguntaID = $('#PreguntaID').val();
        var PreguntaObservacionID = $('#PreguntaObservacionID').val();
        var TipoPreguntaID = $('#TipoPreguntaID').val();
        var Observaciones = $('#txtObservaciones').val();
        var OpcionID = $("input[name='rbOpcion']:checked").val();
        var EncuestaNo = $('#EncuestaNo').val();
        var OpcionObservacionID = $('#OpcionObservacionID').val();
        var TipoPreguntaObservacionID = $('#TipoPreguntaObservacionID').val();

        if (!OpcionID) {
            bootbox.dialog({
                message: "<span class='bigger-110'>Debe seleccionar una de las opciones para poder continuar.</span>",
                buttons: {
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
        else {
            $("#Pregunta input[type='radio']").attr("disabled", true);
            $("#btnGuardarPregunta").attr("disabled", true);

            if (!OpcionID) {
                OpcionID = OpcionObservacionID;
                TipoPreguntaID = TipoPreguntaObservacionID;
                PreguntaID = PreguntaObservacionID;
            }
            AjaxPreguntas(EncuestaNo, PreguntaID, TipoPreguntaID, OpcionID, Observaciones);
        }
    });

    $('#btnFinalizarEncuesta').on('click', function (e) {
        var EncuestaNo = $('#EncuestaNo').val();
        $("#divWaiting").show();
        $.ajax({
            type: "POST",
            url: 'MisEncuestas.aspx/FinalizarEncuesta',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: "{EncuestaNo: " + EncuestaNo + "}",
            success: function (msg) {
                $("#divWaiting").hide();
                bootbox.dialog({
                    message: "<span class='bigger-110'>" + msg.d + "</span>",
                    buttons: {
                        "success":
							    {
							        "label": "<i class='ace-icon fa fa-check'></i> Aceptar!",
							        "className": "btn-sm btn-success",
							        "callback": function () {
							        }
							    }
                    }
                });
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert("Ocurrió un error inesperado al tratar de procesar esta acción: \n" + err.Message);
            }
        });
    });
});

function validarEmail(email) {
    var res = true;
    expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!expr.test(email))
        res = false;

    return res;
}