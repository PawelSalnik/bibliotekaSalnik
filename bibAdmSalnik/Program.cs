using System;

using bibModelSalnik.Model;

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

        string result = lib.ReportData();           
        Console.WriteLine("\n>> Dane autorów (plik XML):\n");
        Console.WriteLine(result);                   
    }



    static void ShowBooksByAuthor()
    {
        // Tu dodamy kod do wyszukiwania książek autora (LINQ where)
        Console.WriteLine(">> [TODO] Wyszukiwanie książek dla autora.");
    }
}