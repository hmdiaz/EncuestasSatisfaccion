using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BAL
{
    public class Formulario
    {
        [DataMember]
        public int FormularioID { get; set; }
        [DataMember]
        public string NombreFormulario { get; set; }
        [DataMember]
        public int NumeroGrupo { get; set; }        
        [DataMember]
        public string NombreGrupo { get; set; }
        [DataMember]
        public int PreguntaID { get; set; }
        [DataMember]
        public int GrupoPreguntaID { get; set; }
        [DataMember]
        public int NumeroPregunta { get; set; }
        [DataMember]
        public string TextoPregunta { get; set; }
        [DataMember]
        public bool Obligatoria { get; set; }
        [DataMember]
        public int TipoPreguntaID { get; set; }
        [DataMember]
        public int NumeroOpcion { get; set; }
        [DataMember]
        public string TextoOpcion { get; set; }
        [DataMember]
        public double PuntajeOpcion { get; set; }
        [DataMember]
        public int OpcionID { get; set; }
        [DataMember]
        public int TipoCampoID { get; set; }
    }
}
