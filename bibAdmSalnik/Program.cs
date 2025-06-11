using System;
using System.Linq;
using bibModelSalnik.Model;
using System.Collections.Generic;


class Program
{
    static void Main(string[] args)
    {
        char opcja;
        do
        {
            Console.Clear();
            Console.WriteLine("=== MENU BIBLIOTEKI ===");
            Console.WriteLine("W - Wyświetl dane");
            Console.WriteLine("A - Książki dla podanego autora");
            Console.WriteLine("X - Koniec");
            Console.Write("Wybierz opcję: ");

            opcja = Console.ReadKey(true).KeyChar;
            Console.WriteLine();

            switch (char.ToUpper(opcja))
            {
                case 'W':
                    ShowData(); // do zdefiniowania
                    Console.WriteLine("\nNaciśnij dowolny klawisz, aby powrócić do menu...");
                    Console.ReadKey();
                    break;

                case 'A':
                    ShowBooksByAuthor(); // do zdefiniowania
                    Console.WriteLine("\nNaciśnij dowolny klawisz, aby powrócić do menu...");
                    Console.ReadKey();
                    break;

                case 'X':
                    Console.WriteLine("Zakończono program.");
                    break;

                default:
                    Console.WriteLine("Nieprawidłowy wybór.");
                    Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                    Console.ReadKey();
                    break;
            }

        } while (char.ToUpper(opcja) != 'X');
    }

    static void ShowData()
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\";
        BDLibrary lib = new BDLibrary(folderPath);

        Autorzy authors = lib.ReportData2();
        Ksiazki books = lib.ReportData3();

        // Wyświetl autorów
        if (authors?.Autor != null && authors.Autor.Length > 0)
        {
            Console.WriteLine("Autorzy:");
            foreach (var a in authors.Autor)
                Console.WriteLine($"{a.id}\t{a.nazwisko}\t{a.imię}\t{a.rokUr}");
        }
        else
        {
            Console.WriteLine("Brak danych o autorach.");
        }

        Console.WriteLine("\nKsiążki:");
        if (books?.Items != null && books.Items.Length > 0)
        {
            foreach (var k in books.Items)
            {
                Console.WriteLine($"ID: {k.id}, Tytuł: {k.tytul}, IdAutora: {k.IdAutora}, Rok wydania: {k.rok_wydania}, IdWydawcy: {k.IdWydawcy}, ISBN: {k.ISBN}, Cena: {k.cena}");
            }
        }
        else
        {
            Console.WriteLine("Brak danych o książkach.");
        }

        var booksExt = lib.ReportDataLQ2();

        if (booksExt != null && booksExt.Count > 0)
        {
            Console.WriteLine("\nKsiążki (z nazwiskiem i imieniem autora oraz nazwą wydawnictwa):");
            foreach (var bk in booksExt)
            {
                Console.WriteLine($"ID: {bk.id}, Tytuł: {bk.tytul}, Autor: {bk.nazwiskoImie}, Wydawnictwo: {bk.nazwaWydawnictwa}, Cena: {bk.cena:C}");
            }
        }
        else
        {
            Console.WriteLine("Brak rozszerzonych danych o książkach.");
        }
    }







    static void ShowBooksByAuthor()
    {
        // Tu dodamy kod do wyszukiwania książek autora (LINQ where)
        Console.WriteLine(">> [TODO] Wyszukiwanie książek dla autora.");
    }
}