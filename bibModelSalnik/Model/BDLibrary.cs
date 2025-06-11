using bibModelSalnik.Model;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;



namespace bibModelSalnik.Model
{
    public class BDLibrary
    {
        private string authorsFile;
        private string publishersFile;
        private string booksFile;
        private string folderPath;

        public BDLibrary(string folderPath)
        {
            this.folderPath = folderPath;
            authorsFile = Path.Combine(folderPath, DefaultFileNames.plikAutorzy);
            publishersFile = Path.Combine(folderPath, DefaultFileNames.plikWydawnictwa);
            booksFile = Path.Combine(folderPath, DefaultFileNames.plikKsiazki);
        }

        public string ReportData()
        {
            string filePath = authorsFile;


            try
            {
                XDocument xdoc = XDocument.Load(filePath);
                return xdoc.ToString(); // zwraca cały XML jako string
            }
            catch (Exception ex)
            {
                return $"Błąd podczas odczytu pliku: {ex.Message}";
            }
        }

        public T LoadFromXml<T>(string fileName)
        {
            string fullPath = Path.Combine(folderPath, fileName);
            if (!File.Exists(fullPath)) return default;

            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StreamReader s = new StreamReader(fullPath))
            {
                return (T)xs.Deserialize(s);
            }
        }
        public Autorzy ReportData2()
        {
            return LoadFromXml<Autorzy>("autorzy_Salnik.xml");
        }

        public Ksiazki ReportData3()
        {
            return LoadFromXml<Ksiazki>("ksiazki_Salnik.xml");
        }



        public List<AutorzyAutor> ReportDataLQ()
        {
            try
            {
                var xs = new XmlSerializer(typeof(Autorzy));
                using (var s = new StreamReader(authorsFile))
                {
                    Autorzy authors = (Autorzy)xs.Deserialize(s);
                    var sortLstAuthors = authors.Autor.OrderBy(a => a.nazwisko).ToList();
                    return sortLstAuthors;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd LINQ (autorzy): " + ex.Message);
                return new List<AutorzyAutor>();
            }
        }





        public IOrderedEnumerable<KsiazkiKsiazka> ReportDataLQW()
        {
            try
            {
                var xs = new XmlSerializer(typeof(Ksiazki));
                using (var s = new StreamReader(booksFile)) // ← poprawione
                {
                    Ksiazki ksiazki = (Ksiazki)xs.Deserialize(s);

                    var sortLstPublishers = from item in ksiazki.Items
                                            orderby item.tytul
                                            select item;

                    return sortLstPublishers as IOrderedEnumerable<KsiazkiKsiazka>;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd LINQ (wydawnictwa): " + ex.Message);
                return null;
            }
        }

        public List<KsiazkiKsiazkaExt> ReportDataLQ2()
        {

            // 1. Wczytaj autorów (posortowanych)
            var authorsOrdered = ReportDataLQ(); // teraz List<AutorzyAutor>

            if (authorsOrdered == null)
                return new List<KsiazkiKsiazkaExt>();

            // 2. Wczytaj książki
            var books = ReportData3();
            if (books == null || books.Items == null || books.Items.Length == 0)
                return new List<KsiazkiKsiazkaExt>();

            // 3. Wczytaj wydawnictwa
            var publishers = LoadFromXml<Wydawcy>(DefaultFileNames.plikWydawnictwa);
            if (publishers == null || publishers.Items == null || publishers.Items.Length == 0)
                return new List<KsiazkiKsiazkaExt>();

            var publishersOrdered = publishers.Items.OrderBy(p => p.nazwa).ToList();

            // 4. LINQ: join
            var query = from book in books.Items
                        join author in authorsOrdered on book.IdAutora equals author.id
                        join publisher in publishersOrdered on book.IdWydawcy equals publisher.id
                        orderby book.tytul
                        select new KsiazkiKsiazkaExt()
                        {
                            id = book.id,
                            tytul = book.tytul,
                            nazwiskoImie = author.nazwisko + " " + author.imię,
                            nazwaWydawnictwa = publisher.nazwa,
                            cena = book.cena
                        };

            return query.ToList();
        }


        /*
         public Ksiazki ReportData3()
         {

             try
             {
                 var xs = new XmlSerializer(typeof(Ksiazki));
                 using (var s = new StreamReader(booksFile))
                 {
                     Ksiazki books = (Ksiazki)xs.Deserialize(s);
                     return books;
                 }
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Błąd podczas deserializacji książek: " + ex.Message);
                 return null;
             }
         }
        */





        public bool TestData()
        {
            try
            {
                // Dane autorów (serializacja XML do pliku)
                var authors = new Autorzy()
                {
                    Autor = new AutorzyAutor[]
                    {
                        new AutorzyAutor() { id = 1, nazwisko = "Mickiewicz", imię = "Adam", rokUr = 1798 },
                        new AutorzyAutor() { id = 2, nazwisko = "Słowacki", imię = "Juliusz", rokUr = 1809 },
                        new AutorzyAutor() { id = 3, nazwisko = "Prus", imię = "Bolesław", rokUr = 1847 },
                        new AutorzyAutor() { id = 4, nazwisko = "Żeromski", imię = "Stefan", rokUr = 1864 },
                        new AutorzyAutor() { id = 5, nazwisko = "Salnik", imię = "Jan", rokUr = 2000 }
                    }
                };

                var xs = new XmlSerializer(typeof(Autorzy));
                using (StreamWriter sw = new StreamWriter(authorsFile))
                {
                    xs.Serialize(sw, authors);
                }

                // Dane książek (XDocument - bez klas i bez serializacji)
                if (!File.Exists(booksFile))
                {
                    var ksiazkiDoc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "no"),
                        new XComment("Dane testowe - książki"),
                        new XElement("Ksiazki",
                            new XElement("Ksiazka", new XAttribute("id", "1"), new XAttribute("tytul", "Lalka"), new XAttribute("idAutora", "3"), new XAttribute("ISBN", "978-83-01-00001-0"), new XAttribute("cena", "39.99"), new XAttribute("idWydawnictwa", "1")),
                            new XElement("Ksiazka", new XAttribute("id", "2"), new XAttribute("tytul", "Wesele"), new XAttribute("idAutora", "4"), new XAttribute("ISBN", "978-83-01-00002-0"), new XAttribute("cena", "35.50"), new XAttribute("idWydawnictwa", "2")),
                            new XElement("Ksiazka", new XAttribute("id", "3"), new XAttribute("tytul", "Placówka"), new XAttribute("idAutora", "3"), new XAttribute("ISBN", "978-83-01-00003-0"), new XAttribute("cena", "34.90"), new XAttribute("idWydawnictwa", "1")),
                            new XElement("Ksiazka", new XAttribute("id", "4"), new XAttribute("tytul", "Inny świat"), new XAttribute("idAutora", "5"), new XAttribute("ISBN", "978-83-01-00004-0"), new XAttribute("cena", "31.00"), new XAttribute("idWydawnictwa", "2")),
                            new XElement("Ksiazka", new XAttribute("id", "5"), new XAttribute("tytul", "Powrót z gwiazd"), new XAttribute("idAutora", "2"), new XAttribute("ISBN", "978-83-01-00005-0"), new XAttribute("cena", "36.40"), new XAttribute("idWydawnictwa", "3")),
                            new XElement("Ksiazka", new XAttribute("id", "6"), new XAttribute("tytul", "Heban"), new XAttribute("idAutora", "4"), new XAttribute("ISBN", "978-83-01-00006-0"), new XAttribute("cena", "40.00"), new XAttribute("idWydawnictwa", "4")),
                            new XElement("Ksiazka", new XAttribute("id", "7"), new XAttribute("tytul", "Faraon"), new XAttribute("idAutora", "3"), new XAttribute("ISBN", "978-83-01-00007-0"), new XAttribute("cena", "38.75"), new XAttribute("idWydawnictwa", "1")),
                            new XElement("Ksiazka", new XAttribute("id", "8"), new XAttribute("tytul", "Ziemia obiecana"), new XAttribute("idAutora", "1"), new XAttribute("ISBN", "978-83-01-00008-0"), new XAttribute("cena", "42.00"), new XAttribute("idWydawnictwa", "3")),
                            new XElement("Ksiazka", new XAttribute("id", "9"), new XAttribute("tytul", "Ogniem i mieczem"), new XAttribute("idAutora", "1"), new XAttribute("ISBN", "978-83-01-00009-0"), new XAttribute("cena", "45.90"), new XAttribute("idWydawnictwa", "2")),
                            new XElement("Ksiazka", new XAttribute("id", "10"), new XAttribute("tytul", "Dzieła zebrane"), new XAttribute("idAutora", "5"), new XAttribute("ISBN", "978-83-01-00010-0"), new XAttribute("cena", "50.00"), new XAttribute("idWydawnictwa", "4"))
                        )
                    );

                    ksiazkiDoc.Save(booksFile);
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
