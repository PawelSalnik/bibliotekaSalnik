using System;
using System.Xml.Serialization;


namespace bibModelSalnik.Model
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [Serializable]
    [System.Diagnostics.DebuggerStepThrough]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class Ksiazki
    {
        private KsiazkiKsiazka[] itemsField;

        [XmlElement("Ksiazka", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public KsiazkiKsiazka[] Items
        {
            get { return this.itemsField; }
            set { this.itemsField = value; }
        }





    }




    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [Serializable]
    [System.Diagnostics.DebuggerStepThrough]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class KsiazkiKsiazka
    {
        private int idField;
        private string tytulField;
        private int idAutoraField;
        private int rok_wydaniaField;
        private int idWydawcyField;
        private string iSBNField;
        private decimal cenaField;

        [XmlAttribute]
        public int id
        {
            get { return this.idField; }
            set { this.idField = value; }
        }

        [XmlAttribute]
        public string tytul
        {
            get { return this.tytulField; }
            set { this.tytulField = value; }
        }

        [XmlAttribute]
        public int IdAutora
        {
            get { return this.idAutoraField; }
            set { this.idAutoraField = value; }
        }

        [XmlAttribute]
        public int rok_wydania
        {
            get { return this.rok_wydaniaField; }
            set { this.rok_wydaniaField = value; }
        }

        [XmlAttribute]
        public int IdWydawcy
        {
            get { return this.idWydawcyField; }
            set { this.idWydawcyField = value; }
        }

        [XmlAttribute]
        public string ISBN
        {
            get { return this.iSBNField; }
            set { this.iSBNField = value; }
        }

        [XmlAttribute]
        public decimal cena
        {
            get { return this.cenaField; }
            set { this.cenaField = value; }
        }
    }




}
