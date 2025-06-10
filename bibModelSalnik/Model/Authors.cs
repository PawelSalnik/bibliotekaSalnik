using System;
using System.Xml.Serialization;

namespace bibModelSalnik.Model
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Autorzy
    {
        private AutorzyAutor[] autorField;

        /// <remarks/>
        [XmlElement("Autor")]
        public AutorzyAutor[] Autor
        {
            get { return this.autorField; }
            set { this.autorField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AutorzyAutor
    {
        private byte idField;

        private string nazwiskoField;

        private string imięField;

        private ushort rokUrField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte id
        {
            get { return this.idField; }
            set { this.idField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nazwisko
        {
            get { return this.nazwiskoField; }
            set { this.nazwiskoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string imię
        {
            get { return this.imięField; }
            set { this.imięField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort rokUr
        {
            get { return this.rokUrField; }
            set { this.rokUrField = value; }
        }
    }
}
