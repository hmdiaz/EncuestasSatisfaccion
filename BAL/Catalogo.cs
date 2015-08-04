using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BAL
{
    [DataContract]
    public class Catalogo
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Etiqueta { get; set; }
    }
}
