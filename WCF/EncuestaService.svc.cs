using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BAL;
using DAL;
using System.Diagnostics;

namespace WCF
{
    public class EncuestaService : IEncuestaService
    {
        public List<Encuesta> ListarEncuestasPorUsuario(int UsuarioID)
        {
            List<Encuesta> listado = new List<Encuesta>();
            try
            {
                listado = new EncuestaDAL().ListarEncuestasPorUsuario(UsuarioID);
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists("EncuestasEvaluacion App"))
                    EventLog.CreateEventSource("EncuestasEvaluacion App", "Application");

                EventLog.WriteEntry("EncuestasEvaluacion App", string.Format("Error: {0}. StackTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error);
            }
            return listado;
        }

        public List<Encuesta> ListarEstatusEncuestasPorUsuario(int UsuarioID)
        {
            List<Encuesta> listado = new List<Encuesta>();
            try
            {
                listado = new EncuestaDAL().ListarEstatusEncuestasPorUsuario(UsuarioID);
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists("EncuestasEvaluacion App"))
                    EventLog.CreateEventSource("EncuestasEvaluacion App", "Application");

                EventLog.WriteEntry("EncuestasEvaluacion App", string.Format("Error: {0}. StackTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error);
            }
            return listado;
        }

        public List<GrupoPregunta> ListarPreguntasDisponibles(int FormularioID, int EncuestaNo)
        {
            List<GrupoPregunta> listado = new List<GrupoPregunta>();
            try
            {
                listado = new GrupoPreguntaDAL().ListarPreguntasDisponibles(FormularioID, EncuestaNo);
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists("EncuestasEvaluacion App"))
                    EventLog.CreateEventSource("EncuestasEvaluacion App", "Application");

                EventLog.WriteEntry("EncuestasEvaluacion App", string.Format("Error: {0}. StackTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error);
            }
            return listado;
        }

        public List<Formulario> ObtenerPreguntas(int FormularioID, int GrupoPreguntaID)
        {
            List<Formulario> listado = new List<Formulario>();
            try
            {
                listado = new FormularioDAL().ObtenerPreguntas(FormularioID, GrupoPreguntaID);
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists("EncuestasEvaluacion App"))
                    EventLog.CreateEventSource("EncuestasEvaluacion App", "Application");

                EventLog.WriteEntry("EncuestasEvaluacion App", string.Format("Error: {0}. StackTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error);
            }
            return listado;
        }

        public int GuardarEncuesta(int ProyectoID, int FormularioID, string CorreoElectronico, int UsuarioAuditID)
        {
            int n = 0;
            try
            {
                n = new EncuestaDAL().GuardarEncuesta(ProyectoID, FormularioID, CorreoElectronico, UsuarioAuditID);
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists("EncuestasEvaluacion App"))
                    EventLog.CreateEventSource("EncuestasEvaluacion App", "Application");

                EventLog.WriteEntry("EncuestasEvaluacion App", string.Format("Error: {0}. StackTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error);
            }
            return n;
        }
        public int GuardarPregunta(int ProyectoID, int FormularioID, int TipoPreguntaID, int PreguntaID, int OpcionID,
                                   int UsuarioAuditID, int EncuestaNo, string TextoRespuesta = "")
        {
            int n = 0;
            try
            {
                n = new EncuestaDAL().GuardarPregunta(ProyectoID, FormularioID, TipoPreguntaID, PreguntaID, OpcionID, UsuarioAuditID, 
                            EncuestaNo, TextoRespuesta);
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists("EncuestasEvaluacion App"))
                    EventLog.CreateEventSource("EncuestasEvaluacion App", "Application");

                EventLog.WriteEntry("EncuestasEvaluacion App", string.Format("Error: {0}. StackTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error);
            }
            return n;
        }

        public int VerificarUsuario(string CorreoElectronico)
        {
            {
                int n = 0;
                try
                {
                    n = new EncuestaDAL().VerificarUsuario(CorreoElectronico);
                }
                catch (Exception ex)
                {
                    if (!EventLog.SourceExists("EncuestasEvaluacion App"))
                        EventLog.CreateEventSource("EncuestasEvaluacion App", "Application");

                    EventLog.WriteEntry("EncuestasEvaluacion App", string.Format("Error: {0}. StackTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error);
                }
                return n;
            }
        }

        public UsuarioEncuesta ObtenerUsuario(int UsuarioEncuestaID)
        {
            UsuarioEncuesta Usuario = new UsuarioEncuesta();
            try
            {
                Usuario = new EncuestaDAL().ObtenerUsuario(UsuarioEncuestaID);
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists("EncuestasEvaluacion App"))
                    EventLog.CreateEventSource("EncuestasEvaluacion App", "Application");

                EventLog.WriteEntry("EncuestasEvaluacion App", string.Format("Error: {0}. StackTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error);
            }
            return Usuario;
        }

        public int ActualizarUsuarioEncuesta(int UsuarioEncuestaID, string PrimerNombre, string SegundoNombre, string PrimerApellido,
            string SegundoApellido, string NombreEntidad, string NombreProyecto, string CargoFuncionario)
        {
            int n = 0;
            try
            {
                n = new EncuestaDAL().ActualizarUsuarioEncuesta(UsuarioEncuestaID, PrimerNombre, SegundoNombre, PrimerApellido,
                                                                SegundoApellido, NombreEntidad, NombreProyecto, CargoFuncionario);
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists("EncuestasEvaluacion App"))
                    EventLog.CreateEventSource("EncuestasEvaluacion App", "Application");

                EventLog.WriteEntry("EncuestasEvaluacion App", string.Format("Error: {0}. StackTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error);
            }
            return n;
        }

        public int FinalizarEncuesta(int EncuestaNo)
        {
            int n = 0;
            try
            {
                n = new EncuestaDAL().FinalizarEncuesta(EncuestaNo);
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists("EncuestasEvaluacion App"))
                    EventLog.CreateEventSource("EncuestasEvaluacion App", "Application");

                EventLog.WriteEntry("EncuestasEvaluacion App", string.Format("Error: {0}. StackTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error);
            }
            return n;
        }

        public List<Pregunta> SelectPreguntasFaltantes(int EncuestaNo)
        {
            List<Pregunta> preguntas = new List<Pregunta>();
            try
            {
                preguntas = new EncuestaDAL().SelectPreguntasFaltantes(EncuestaNo);
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists("EncuestasEvaluacion App"))
                    EventLog.CreateEventSource("EncuestasEvaluacion App", "Application");

                EventLog.WriteEntry("EncuestasEvaluacion App", string.Format("Error: {0}. StackTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error);
            }
            return preguntas;
        }
    }
}
