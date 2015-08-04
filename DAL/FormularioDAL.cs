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
    public class FormularioDAL
    {
        public List<Formulario> ObtenerPreguntas(int FormularioID, int NumeroGrupo)
        {
            List<Formulario> listado = new List<Formulario>();
            using (SqlDataAdapter adaptador = new SqlDataAdapter("[SATISFACCION].[sps_Encuesta]", ConfigurationManager.ConnectionStrings["AztecaCString"].ConnectionString))
            {
                DataTable dataTable = new DataTable();
                adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pFormularioID", SqlDbType.Int)).Value = FormularioID;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pNumeroGrupo", SqlDbType.Int)).Value = NumeroGrupo;

                adaptador.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    Formulario form = new Formulario()
                    {
                        FormularioID = Int32.Parse(row["FormularioID"].ToString()),
                        NombreFormulario = row["NombreFormulario"].ToString(),
                        NumeroGrupo = Int32.Parse(row["NumeroGrupo"].ToString()),
                        NombreGrupo = row["NombreGrupo"].ToString(),
                        PreguntaID = Int32.Parse(row["PreguntaID"].ToString()),
                        GrupoPreguntaID = Int32.Parse(row["GrupoPreguntaID"].ToString()),
                        NumeroPregunta = Int32.Parse(row["NumeroPregunta"].ToString()),
                        TextoPregunta = row["TextoPregunta"].ToString(),
                        Obligatoria = Boolean.Parse(row["Obligatoria"].ToString()),
                        TipoPreguntaID = Int32.Parse(row["TipoPreguntaID"].ToString()),
                        NumeroOpcion = Int32.Parse(row["NumeroOpcion"].ToString()),
                        TextoOpcion = row["TextoOpcion"].ToString(),
                        PuntajeOpcion = Int32.Parse(row["PuntajeOpcion"].ToString()),
                        OpcionID = Int32.Parse(row["OpcionID"].ToString()),
                        TipoCampoID = Int32.Parse(row["TipoCampoID"].ToString())  
                    };
                    listado.Add(form);
                }

                return listado;
            }
        }

        public List<Formulario> ObtenerFormularios()
        {
            List<Formulario> listado = new List<Formulario>();
            using (SqlDataAdapter adaptador = new SqlDataAdapter("[SATISFACCION].[sps_Formulario]", ConfigurationManager.ConnectionStrings["AztecaCString"].ConnectionString))
            {
                DataTable dataTable = new DataTable();
                adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
                adaptador.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    Formulario form = new Formulario()
                    {
                        FormularioID = Int32.Parse(row["FormularioID"].ToString()),
                        NombreFormulario = row["NombreFormulario"].ToString()
                    };
                    listado.Add(form);
                }

                return listado;
            }
        }
    }
}
