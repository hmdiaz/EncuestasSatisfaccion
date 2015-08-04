using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BAL
{
    public class GrupoPregunta
    {
        [DataMember]
        public int GrupoPreguntaID { get; set; }
        [DataMember]
        public int FormularioID { get; set; }
        [DataMember]
        public int NumeroGrupo { get; set; }
        [DataMember]
        public string NombreGrupoPregunta { get; set; }
    }
}
