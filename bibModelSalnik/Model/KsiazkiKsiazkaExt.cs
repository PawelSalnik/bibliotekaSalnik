using System;
using System.Collections.Generic;
using System.Text;
using bibModelSalnik.Model;

namespace bibModeSalnik.Model
{
    public class KsiazkiKsiazkaExt : KsiazkiKsiazka
    {
        public string nazwiskoImie { get; set; }  // np. "Mickiewicz Adam"
        public string nazwaWydawnictwa { get; set; }
    }
}

