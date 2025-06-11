using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Diagnostics;

// Reprezentacja listy wydawców (root XML)
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlRoot("Wydawcy", Namespace = "", IsNullable = false)]
public class Wydawcy
{
    [XmlElement("Wydawca", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public WydawcyWydawca[] Items { get; set; }
}

// Reprezentacja pojedynczego wydawcy
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
public class WydawcyWydawca
{
    // Poprawione: typ int — spójny z IdWydawnictwa w książce
    [XmlAttribute("id")]
    public int id { get; set; }

    [XmlAttribute("nazwa")]
    public string nazwa { get; set; }

    [XmlAttribute("strona")]
    public string strona { get; set; }
}
