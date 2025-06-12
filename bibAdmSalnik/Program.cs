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
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\";
        BDLibrary lib = new BDLibrary(folderPath);

        Autorzy authors = lib.ReportData2();
        Ksiazki books = lib.ReportData3();

        if (authors?.Autor == null || books?.Items == null)
        {
            Console.WriteLine("Brak danych o autorach lub książkach.");
            return;
        }

        Console.Write("Podaj nazwisko autora: ");
        string inputName = Console.ReadLine().Trim().ToLower();

        // a. Porównanie ==
        var matchingAuthorIDsExact = authors.Autor
            .Where(a => a.nazwisko != null && a.nazwisko.ToLower() == inputName)
            .Select(a => (int)a.id) // rzutowanie byte -> int
            .ToList();

        Console.WriteLine("\nKsiążki dla autora (==):");
        var booksByExact = books.Items
            .Where(k => matchingAuthorIDsExact.Contains(k.IdAutora))
            .ToList();

        if (booksByExact.Count == 0)
            Console.WriteLine("Brak książek.");
        else
            foreach (var book in booksByExact)
                Console.WriteLine($"Tytuł: {book.tytul}, ISBN: {book.ISBN}");

        // b. Porównanie Contains
        var matchingAuthorIDsContains = authors.Autor
            .Where(a => a.nazwisko != null && a.nazwisko.ToLower().Contains(inputName))
            .Select(a => (int)a.id) // rzutowanie byte -> int
            .ToList();

        Console.WriteLine("\nKsiążki dla autora (Contains):");
        var booksByContains = books.Items
            .Where(k => matchingAuthorIDsContains.Contains(k.IdAutora))
            .ToList();

        if (booksByContains.Count == 0)
            Console.WriteLine("Brak książek.");
        else
            foreach (var book in booksByContains)
                Console.WriteLine($"Tytuł: {book.tytul}, ISBN: {book.ISBN}");
    }







}