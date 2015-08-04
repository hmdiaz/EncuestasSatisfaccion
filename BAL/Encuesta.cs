using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BAL
{
    [DataContract]
    public class Encuesta
    {
        [DataMember]
        public int EncuestaID { get; set; }
        [DataMember]
        public string Documento { get; set; }
        [DataMember]
        public string PrimerNombre { get; set; }
        [DataMember]
        public string SegundoNombre { get; set; }
        [DataMember]
        public string PrimerApellido { get; set; }
        [DataMember]
        public string SegundoApellido { get; set; }        
        [DataMember]
        public string NombreProyecto { get; set; }
        [DataMember]
        public string NombreFormulario { get; set; }
        [DataMember]
        public DateTime FechaDiligenciamiento { get; set; }        
        [DataMember]
        public int TotalPreguntas { get; set; }
        [DataMember]
        public int TotalRespuestas { get; set; }
        [DataMember]
        public int FormularioID { get; set; }
        [DataMember]
        public int ProyectoID { get; set; }  
        [DataMember]
        public Boolean Activo { get; set; }
        [DataMember]
        public int length { get; set; }
    }
}
