using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BAL;

namespace WCF
{
    [ServiceContract]
    public interface ICatalogoServices
    {
        #region Carga de catálogos
        [OperationContract]
        List<Catalogo> CargarCatalogoWCF(string listado);
        [OperationContract]
        List<Catalogo> CargarCatalogoDependienteWCF(string listado, string id);
        #endregion        
    }
}
