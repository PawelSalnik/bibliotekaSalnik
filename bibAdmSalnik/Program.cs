using System;

using bibModelSalnik.Model;

class Program
{
    static void Main(string[] args)
    {
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var db = new BDLibrary(documentsPath);

        bool sukces = db.TestData();
        Console.WriteLine(sukces ? "Dane testowe zapisane." : "Błąd zapisu danych.");
    }
}