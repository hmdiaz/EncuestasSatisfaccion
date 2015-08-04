using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BAL;
using System.Diagnostics;
using DAL;

namespace WCF
{
    public class CatalogoServices : ICatalogoServices
    {
        public List<Catalogo> CargarCatalogoWCF(string listado)
        {
            List<Catalogo> listadoTem = new List<Catalogo>();

            try
            {
                listadoTem = new CatalogoDAL().CargarCatalogoDAL(listado);
            }
            catch (Exception ex)
            {
                //if (!EventLog.SourceExists("SistemaRRHH App"))
                //    EventLog.CreateEventSource("GestionKPIs App", "Application");

                //EventLog.WriteEntry("SistemaRRHH App", string.Format("Error: {0}. StackTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error);
            }

            return listadoTem;
        }

        public List<Catalogo> CargarCatalogoDependienteWCF(string listado, string id)
        {
            List<Catalogo> listadoTem = new List<Catalogo>();

            try
            {
                listadoTem = new CatalogoDAL().CargarCatalogoDependienteDAL(listado, id);
            }
            catch (Exception ex)
            {
                //if (!EventLog.SourceExists("GestionKPIs App"))
                //    EventLog.CreateEventSource("GestionKPIs App", "Application");

                //EventLog.WriteEntry("GestionKPIs App", string.Format("Error: {0}. StackTrace: {1}", ex.Message, ex.StackTrace), EventLogEntryType.Error);
            }

            return listadoTem;
        }
    }
}
