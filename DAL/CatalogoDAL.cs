using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using BAL;

namespace DAL
{
    public class CatalogoDAL
    {
        // Método para cargar los catálogos
        public List<Catalogo> CargarCatalogoDAL(string listado)
        {
            List<Catalogo> listadoTem = new List<Catalogo>();

            switch (listado)
            {
                case "Formulario":
                    #region Formulario
                    using (SqlDataAdapter adaptador = new SqlDataAdapter("[SATISFACCION].[sps_Formulario]", ConfigurationManager.ConnectionStrings["AztecaCString"].ConnectionString))
                    {
                        DataTable dataTable = new DataTable();
                        adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adaptador.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            Catalogo catalogoTem = new Catalogo()
                            {
                                Id = Int32.Parse(row["FormularioID"].ToString()),
                                Etiqueta = row["NombreFormulario"].ToString()
                            };

                            listadoTem.Add(catalogoTem);
                        }
                    }
                    break;
                    #endregion
                case "Proyecto":
                    #region Proyecto
                    using (SqlDataAdapter adaptador = new SqlDataAdapter("[SATISFACCION].[sps_Proyecto]", ConfigurationManager.ConnectionStrings["AztecaCString"].ConnectionString))
                    {
                        DataTable dataTable = new DataTable();
                        adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adaptador.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            Catalogo catalogoTem = new Catalogo()
                            {
                                Id = Int32.Parse(row["ProyectoID"].ToString()),
                                Etiqueta = row["NombreProyecto"].ToString()
                            };

                            listadoTem.Add(catalogoTem);
                        }
                    }
                    break;
                    #endregion
                case "Usuarios":
                    #region Usuarios
                    using (SqlDataAdapter adaptador = new SqlDataAdapter("[SATISFACCION].[sps_Usuarios]", ConfigurationManager.ConnectionStrings["AztecaCString"].ConnectionString))
                    {
                        DataTable dataTable = new DataTable();
                        adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adaptador.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            Catalogo catalogoTem = new Catalogo()
                            {
                                Id = Int32.Parse(row["UsuarioID"].ToString()),
                                Etiqueta = row["NombreCompleto"].ToString()
                            };

                            listadoTem.Add(catalogoTem);
                        }
                    }
                    break;
                    #endregion
                default:
                    break;
            }
            return listadoTem;
        }

        // Método para cargar los catálogos que dependen de un valor seleccionado previamente
        public List<Catalogo> CargarCatalogoDependienteDAL(string listado, string id)
        {
            List<Catalogo> listadoTem = new List<Catalogo>();

            switch (listado)
            {
                case "Departamentos":
                    #region Departamentos
                    using (SqlDataAdapter adaptador = new SqlDataAdapter("[dbo].[sps_GetDepartamentosByPais]", ConfigurationManager.ConnectionStrings["AztecaCStringSitesDB"].ConnectionString))
                    {
                        DataTable dataTable = new DataTable();
                        adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adaptador.SelectCommand.Parameters.Add(new SqlParameter("@pPaisID", SqlDbType.Int)).Value = Int32.Parse(id);

                        adaptador.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            Catalogo catalogoTem = new Catalogo()
                            {
                                Id = Int32.Parse(row["DepartamentoID"].ToString()),
                                Etiqueta = row["Departamento"].ToString()
                            };

                            listadoTem.Add(catalogoTem);
                        }
                    }
                    break;
                    #endregion
                default:
                    break;
            }
            return listadoTem;
        }
    }
}
