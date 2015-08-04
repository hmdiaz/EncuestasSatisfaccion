using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


[DataContract(Name = "Res", Namespace = "R")]
public class ResponseLogin
{
    [DataMember(Name = "S", EmitDefaultValue = false)]
    public List<Sistema> Systems { get; set; }

    [DataMember(Name = "Ul", EmitDefaultValue = false)]
    public string UserLogin { get; set; }

    [DataMember(Name = "Ui", EmitDefaultValue = false)]
    public int UserID { get; set; }

    [DataMember(Name = "Nu", EmitDefaultValue = false)]
    public string PrimerNombre { get; set; }

    [DataMember(Name = "Su", EmitDefaultValue = false)]
    public string SegundoNombre { get; set; }

    [DataMember(Name = "Pu", EmitDefaultValue = false)]
    public string PrimerApellido { get; set; }

    [DataMember(Name = "Ps", EmitDefaultValue = false)]
    public string SegundoApellido { get; set; }

    [DataMember(Name = "St", EmitDefaultValue =false)]
    public int statusRespuesta { get; set; }
}

[DataContract(Name = "Sis", Namespace = "S")]
public class Sistema
{
    [DataMember(Name = "Si", EmitDefaultValue = false)]
    public string sistema { get; set; }

    [DataMember(Name = "P", EmitDefaultValue = false)]
    public List<Pantalla> pantallas { get; set; }

    public Sistema()
    {
        pantallas = new List<Pantalla>();
    }
}

[DataContract(Name = "Pan", Namespace = "P")]
public class Pantalla
{
    [DataMember(Name = "Pa", EmitDefaultValue = false)]
    public string pantalla { get; set; }

    [DataMember(Name = "R", EmitDefaultValue = false)]
    public List<string> recursos { get; set; }

    public Pantalla()
    {
        recursos = new List<string>();
    }
}
