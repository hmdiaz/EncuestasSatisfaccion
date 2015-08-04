using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BAL
{
    public class Pregunta
    {
        [DataMember]
        public int PreguntaID { get; set; }
        [DataMember]
        public int FormularioID { get; set; }
        [DataMember]
        public string TextoPregunta { get; set; }
        [DataMember]
        public int NumeroPregunta { get; set; }
    }
}
