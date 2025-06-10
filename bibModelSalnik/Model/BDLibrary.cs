using bibModelSalnik.Model;
using System;
using System.IO;
using System.Xml.Serialization;

namespace bibModelSalnik.Model
{
    public class BDLibrary
    {
        private string authorsFile;
        private string publishersFile;
        private string booksFile;

        public BDLibrary(string folderPath)
        {
            authorsFile = Path.Combine(folderPath, DefaultFileNames.plikAutorzy);
            publishersFile = Path.Combine(folderPath, DefaultFileNames.plikWydawnictwa);
            booksFile = Path.Combine(folderPath, DefaultFileNames.plikKsiazki);
        }

        public bool TestData()
        {
            try
            {
                var authors = new Autorzy()
                {
                    Autor = new AutorzyAutor[]
                    {
                        new AutorzyAutor() { id = 1, nazwisko = "Mickiewicz", imię = "Adam", rokUr = 1798 },
                        new AutorzyAutor() { id = 2, nazwisko = "Słowacki", imię = "Juliusz", rokUr = 1809 },
                        new AutorzyAutor() { id = 3, nazwisko = "Prus", imię = "Bolesław", rokUr = 1847 },
                        new AutorzyAutor() { id = 4, nazwisko = "Żeromski", imię = "Stefan", rokUr = 1864 },
                        new AutorzyAutor() { id = 5, nazwisko = "TwojeNazwisko", imię = "TwojeImie", rokUr = 2000 }
                    }
                };

                var xs = new XmlSerializer(typeof(Autorzy));
                using (StreamWriter sw = new StreamWriter(authorsFile))
                {
                    xs.Serialize(sw, authors);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd zapisu danych testowych: " + ex.Message);
                return false;
            }
        }
    }
}
