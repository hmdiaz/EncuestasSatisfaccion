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
    public class GrupoPreguntaDAL
    {
        public List<GrupoPregunta> ListarPreguntasDisponibles(int FormularioID, int EncuestaNo)
        {
            List<GrupoPregunta> listado = new List<GrupoPregunta>();
            using (SqlDataAdapter adaptador = new SqlDataAdapter("[SATISFACCION].[sps_ListadoPreguntasDisponibles]", ConfigurationManager.ConnectionStrings["AztecaCString"].ConnectionString))
            {
                DataTable dataTable = new DataTable();
                adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pFormularioID", SqlDbType.Int)).Value = FormularioID;
                adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pEncuestaNo", SqlDbType.Int)).Value = EncuestaNo;

                adaptador.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    GrupoPregunta encuesta = new GrupoPregunta()
                    {
                        NombreGrupoPregunta = row["GrupoPregunta"].ToString(),
                        NumeroGrupo = Int32.Parse(row["NumeroGrupo"].ToString()),
                        FormularioID = Int32.Parse(row["FormularioID"].ToString())
                    };
                    listado.Add(encuesta);
                }

                return listado;
            }
        }
    }
}
