using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BAL;

namespace WCF
{
    [ServiceContract]
    public interface IEncuestaService
    {
        [OperationContract]
        List<Encuesta> ListarEncuestasPorUsuario(int UsuarioID);

        [OperationContract]
        List<GrupoPregunta> ListarPreguntasDisponibles(int FormularioID, int EncuestaNo);

        [OperationContract]
        List<Formulario> ObtenerPreguntas(int FormularioID, int GrupoPreguntaID);

        [OperationContract]
        int GuardarEncuesta(int ProyectoID, int FormularioID, string CorreoElectronico, int UsuarioAuditID);

        [OperationContract]
        int GuardarPregunta(int ProyectoID, int FormularioID, int TipoPreguntaID, int PreguntaID, int OpcionID, int UsuarioAuditID, 
                            int EncuestaNo, string TextoRespuesta = "");

        [OperationContract]
        int VerificarUsuario(string CorreoElectronico);

        [OperationContract]
        UsuarioEncuesta ObtenerUsuario(int UsuarioEncuestaID);

        [OperationContract]
        int ActualizarUsuarioEncuesta(int UsuarioEncuestaID, string PrimerNombre, string SegundoNombre, string PrimerApellido,
            string SegundoApellido, string NombreEntidad, string NombreProyecto, string CargoFuncionario);

        [OperationContract]
        List<Encuesta> ListarEstatusEncuestasPorUsuario(int UsuarioID);
        [OperationContract]
        int FinalizarEncuesta(int EncuestaNo);

        [OperationContract]
        List<Pregunta> SelectPreguntasFaltantes(int EncuestaNo);
    }
}
