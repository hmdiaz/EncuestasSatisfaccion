using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAL
{
    public class EncuestaDAL
    {
        public List<Encuesta> ListarEncuestasPorUsuario(int UsuarioID)
        {
            List<Encuesta> listado = new List<Encuesta>();            
            using (SqlDataAdapter adaptador = new SqlDataAdapter("[SATISFACCION].[sps_EncuestasUsuario]", ConfigurationManager.ConnectionStrings["AztecaCString"].ConnectionString))
            {
                DataTable dataTable = new DataTable();
                adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pUsuarioID", SqlDbType.Int)).Value = UsuarioID;

                adaptador.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    Encuesta encuesta = new Encuesta()
                    {
                        EncuestaID = Int32.Parse(row["EncuestaID"].ToString()),
                        PrimerNombre = row["PrimerNombre"].ToString(),
                        SegundoNombre = row["SegundoNombre"].ToString(),
                        PrimerApellido = row["PrimerApellido"].ToString(),
                        SegundoApellido = row["SegundoApellido"].ToString(),
                        NombreProyecto = row["NombreProyecto"].ToString(),
                        NombreFormulario = row["NombreFormulario"].ToString(),
                        TotalPreguntas = Int32.Parse(row["TotalPreguntas"].ToString()),
                        TotalRespuestas = Int32.Parse(row["TotalRespuestas"].ToString()),
                        FormularioID = Int32.Parse(row["FormularioID"].ToString()),
                        Activo = Boolean.Parse(row["Activo"].ToString())
                    };
                    listado.Add(encuesta);
                }

                return listado;
            }
        }

        public List<Encuesta> ListarEstatusEncuestasPorUsuario(int UsuarioID)
        {
            List<Encuesta> listado = new List<Encuesta>();
            using (SqlDataAdapter adaptador = new SqlDataAdapter("[SATISFACCION].[sps_EstatusEncuestas]", ConfigurationManager.ConnectionStrings["AztecaCString"].ConnectionString))
            {
                DataTable dataTable = new DataTable();
                adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pUsuarioID", SqlDbType.Int)).Value = UsuarioID;

                adaptador.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    Encuesta encuesta = new Encuesta()
                    {
                        EncuestaID = Int32.Parse(row["EncuestaID"].ToString()),
                        PrimerNombre = row["PrimerNombre"].ToString(),
                        SegundoNombre = row["SegundoNombre"].ToString(),
                        PrimerApellido = row["PrimerApellido"].ToString(),
                        SegundoApellido = row["SegundoApellido"].ToString(),
                        NombreProyecto = row["NombreProyecto"].ToString(),
                        NombreFormulario = row["NombreFormulario"].ToString(),
                        TotalPreguntas = Int32.Parse(row["TotalPreguntas"].ToString()),
                        TotalRespuestas = Int32.Parse(row["TotalRespuestas"].ToString()),
                        FormularioID = Int32.Parse(row["FormularioID"].ToString()),
                        Activo = Boolean.Parse(row["Activo"].ToString())
                    };
                    listado.Add(encuesta);
                }

                return listado;
            }
        }


        public int GuardarEncuesta(int ProyectoID, int FormularioID, string CorreoElectronico, int UsuarioAuditID)
        {
            using (SqlDataAdapter adaptador = new SqlDataAdapter("[SATISFACCION].[spi_Encuesta]", ConfigurationManager.ConnectionStrings["AztecaCString"].ConnectionString))
            {
                # region Parámetros
                adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
                                
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pProyectoID", SqlDbType.VarChar)).Value = ProyectoID;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pFormularioID", SqlDbType.VarChar)).Value = FormularioID;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pCorreoElectronico", SqlDbType.VarChar)).Value = CorreoElectronico;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pUsuarioAuditID", SqlDbType.VarChar)).Value = UsuarioAuditID;

                SqlParameter retval = adaptador.SelectCommand.Parameters.Add("@pRetorno", SqlDbType.Int);
                retval.Direction = ParameterDirection.ReturnValue;
                #endregion

                adaptador.SelectCommand.Connection.Open();
                adaptador.SelectCommand.ExecuteNonQuery();

                return Convert.ToInt32(adaptador.SelectCommand.Parameters["@pRetorno"].Value);
            }
        }

        public int GuardarPregunta(int ProyectoID, int FormularioID, int TipoPreguntaID, int PreguntaID, int OpcionID, 
                                   int UsuarioAuditID, int EncuestaNo, string TextoRespuesta = "")
        {
            using (SqlDataAdapter adaptador = new SqlDataAdapter("[SATISFACCION].[spi_RespuestaKVD]", ConfigurationManager.ConnectionStrings["AztecaCString"].ConnectionString))
            {
                # region Parámetros
                adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
                
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pProyectoID", SqlDbType.Int)).Value = ProyectoID;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pFormularioID", SqlDbType.Int)).Value = FormularioID;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pTipoPreguntaID", SqlDbType.Int)).Value = TipoPreguntaID;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pPreguntaID", SqlDbType.Int)).Value = PreguntaID;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pOpcionID", SqlDbType.Int)).Value = OpcionID;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pTextoRespuesta", SqlDbType.VarChar)).Value = TextoRespuesta;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pEncuestaNo", SqlDbType.Int)).Value = EncuestaNo;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pUsuarioAuditID", SqlDbType.Int)).Value = UsuarioAuditID;

                SqlParameter retval = adaptador.SelectCommand.Parameters.Add("@pRetorno", SqlDbType.Int);
                retval.Direction = ParameterDirection.ReturnValue;
                #endregion

                adaptador.SelectCommand.Connection.Open();
                adaptador.SelectCommand.ExecuteNonQuery();

                return Convert.ToInt32(adaptador.SelectCommand.Parameters["@pRetorno"].Value);
            }
        }

        public int VerificarUsuario(string CorreoElectronico)
        {
            int UsuarioID = 0;
            using (SqlDataAdapter adaptador = new SqlDataAdapter("[SATISFACCION].[sps_VerificarUsuario]", 
                ConfigurationManager.ConnectionStrings["AztecaCString"].ConnectionString))
            {
                DataTable dataTable = new DataTable();
                adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pCorreoElectronico", SqlDbType.VarChar)).Value = CorreoElectronico;
                SqlParameter retval = adaptador.SelectCommand.Parameters.Add("@pRetorno", SqlDbType.Int);
                retval.Direction = ParameterDirection.ReturnValue;

                adaptador.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    UsuarioEncuesta encuesta = new UsuarioEncuesta()
                    {
                        UsuarioEncuestaID = Int32.Parse(row["UsuarioEncuestaID"].ToString()) != null ? Int32.Parse(row["UsuarioEncuestaID"].ToString()) : 0
                    };
                }
                return Convert.ToInt32(adaptador.SelectCommand.Parameters["@pRetorno"].Value);;
            }
        }

        public UsuarioEncuesta ObtenerUsuario(int UsuarioEncuestaID)
        {
            UsuarioEncuesta Usuario = new UsuarioEncuesta();
            using (SqlDataAdapter adaptador = new SqlDataAdapter("[SATISFACCION].[sps_ObtenerUsuario]",
                ConfigurationManager.ConnectionStrings["AztecaCString"].ConnectionString))
            {
                DataTable dataTable = new DataTable();
                adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pUsuarioEncuestaID", SqlDbType.Int)).Value = UsuarioEncuestaID;
                SqlParameter retval = adaptador.SelectCommand.Parameters.Add("@pRetorno", SqlDbType.Int);
                retval.Direction = ParameterDirection.ReturnValue;

                adaptador.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {                    
                    Usuario.UsuarioEncuestaID = Int32.Parse(row["UsuarioEncuestaID"].ToString()) != null ? Int32.Parse(row["UsuarioEncuestaID"].ToString()) : 0;
                    Usuario.PrimerNombre = row["PrimerNombre"].ToString();
                    Usuario.SegundoNombre = row["SegundoNombre"].ToString();
                    Usuario.PrimerApellido = row["PrimerApellido"].ToString();
                    Usuario.SegundoApellido = row["SegundoApellido"].ToString();
                    Usuario.CorreoElectronico = row["CorreoElectronico"].ToString();
                    Usuario.CargoFuncionario = row["CargoFuncionario"].ToString();
                    Usuario.NombreEntidad = row["NombreEntidad"].ToString();
                    Usuario.NombreProyecto = row["NombreProyecto"].ToString();
                    Usuario.EsAdministrador = Boolean.Parse(row["EsAdministrador"].ToString());
                }

                return Usuario;
            }
        }

        public int ActualizarUsuarioEncuesta(int UsuarioEncuestaID, string PrimerNombre, string SegundoNombre, string PrimerApellido, 
            string SegundoApellido, string NombreEntidad, string NombreProyecto, string CargoFuncionario)
        {
            using (SqlDataAdapter adaptador = new SqlDataAdapter("[SATISFACCION].[spu_UsuarioEncuesta]", ConfigurationManager.ConnectionStrings["AztecaCString"].ConnectionString))
            {
                # region Parámetros
                adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pUsuarioEncuestaID", SqlDbType.Int)).Value = UsuarioEncuestaID;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pPrimerNombre", SqlDbType.VarChar)).Value = PrimerNombre;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pSegundoNombre", SqlDbType.VarChar)).Value = SegundoNombre;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pPrimerApellido", SqlDbType.VarChar)).Value = PrimerApellido;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pSegundoApellido", SqlDbType.VarChar)).Value = SegundoApellido;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pNombreEntidad", SqlDbType.VarChar)).Value = NombreEntidad;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pNombreProyecto", SqlDbType.VarChar)).Value = NombreProyecto;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pCargoFuncionario", SqlDbType.VarChar)).Value = CargoFuncionario;

                SqlParameter retval = adaptador.SelectCommand.Parameters.Add("@pRetorno", SqlDbType.Int);
                retval.Direction = ParameterDirection.ReturnValue;
                #endregion

                adaptador.SelectCommand.Connection.Open();
                adaptador.SelectCommand.ExecuteNonQuery();

                return Convert.ToInt32(adaptador.SelectCommand.Parameters["@pRetorno"].Value);
            }
        }

        public int FinalizarEncuesta(int EncuestaNo)
        {
            using (SqlDataAdapter adaptador = new SqlDataAdapter("[SATISFACCION].[spo_FinalizarEncuesta]",
                ConfigurationManager.ConnectionStrings["AztecaCString"].ConnectionString))
            {
                DataTable dataTable = new DataTable();
                adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pEncuestaID", SqlDbType.VarChar)).Value = EncuestaNo;
                SqlParameter retval = adaptador.SelectCommand.Parameters.Add("@pRetorno", SqlDbType.Int);
                retval.Direction = ParameterDirection.ReturnValue;
                
                return Convert.ToInt32(adaptador.SelectCommand.Parameters["@pRetorno"].Value);
            }
        }

        public List<Pregunta> SelectPreguntasFaltantes(int EncuestaNo)
        {
            List<Pregunta> listado = new List<Pregunta>();
            using (SqlDataAdapter adaptador = new SqlDataAdapter("[SATISFACCION].[sps_PreguntasFaltantes]",
                ConfigurationManager.ConnectionStrings["AztecaCString"].ConnectionString))
            {
                DataTable dataTable = new DataTable();
                adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pEncuestaID", SqlDbType.Int)).Value = EncuestaNo;
                SqlParameter retval = adaptador.SelectCommand.Parameters.Add("@pRetorno", SqlDbType.Int);
                retval.Direction = ParameterDirection.ReturnValue;

                adaptador.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    Pregunta pregunta = new Pregunta()
                    {
                        TextoPregunta = row["TextoPregunta"].ToString(),
                        NumeroPregunta = Int32.Parse(row["NumeroPregunta"].ToString()),
                        PreguntaID = Int32.Parse(row["PreguntaID"].ToString()),
                        FormularioID = Int32.Parse(row["FormularioID"].ToString())
                    };
                    listado.Add(pregunta);
                }

                return listado;
            }
        }
    }
}
