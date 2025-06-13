using bibModelSalnik.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bibKliSalnik
{
    public class DataGridDataSourceAuthors
    {
        public List<AutorzyAutor> Autorzy { get; set; }
        public ObservableCollection<AutorzyAutor> AutorzyObservable { get; set; }


        public DataGridDataSourceAuthors()
        {
            Autorzy = new List<AutorzyAutor>
        {
            new AutorzyAutor { id = 1, imię = "Jan", nazwisko = "Kowalski" },
            new AutorzyAutor { id = 2, imię = "Anna", nazwisko = "Nowak" },
            new AutorzyAutor { id = 3, imię = "Piotr", nazwisko = "Wiśniewski" },
            new AutorzyAutor { id = 4, imię = "Ewa", nazwisko = "Zielińska" },
            new AutorzyAutor { id = 5, imię = "Marek", nazwisko = "Lewandowski" }
        };
            AutorzyObservable = new ObservableCollection<AutorzyAutor>();
        }
    }
}
