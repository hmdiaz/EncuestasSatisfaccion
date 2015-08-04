using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BAL
{
    [DataContract]
    public class UsuarioEncuesta
    {
        [DataMember]
        public int UsuarioEncuestaID { get; set; }
        [DataMember]
        public string PrimerNombre { get; set; }
        [DataMember]
        public string SegundoNombre { get; set; }
        [DataMember]
        public string PrimerApellido { get; set; }
        [DataMember]
        public string SegundoApellido { get; set; }
        [DataMember]
        public string CorreoElectronico { get; set; }
        [DataMember]
        public string NombreEntidad { get; set; }
        [DataMember]
        public string NombreProyecto { get; set; }
        [DataMember]
        public string CargoFuncionario { get; set; }
        [DataMember]
        public DateTime FechaDiligenciamiento { get; set; }
        [DataMember]
        public bool EsAdministrador { get; set; }
    }
}
