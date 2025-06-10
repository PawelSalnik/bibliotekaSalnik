using System;

using bibModelSalnik.Model;

class Program
{
    static void Main(string[] args)
    {
        var autor1 = new AutorzyAutor() { id = 1, imię = "Adam", nazwisko = "Mickiewicz", rokUr = 1798 };
        var autor2 = new AutorzyAutor() { id = 2, imię = "Juliusz", nazwisko = "Słowacki", rokUr = 1809 };
        var listaAutor = new Autorzy()
        {
            Autor = new AutorzyAutor[] { autor1, autor2 }
        };

        Console.WriteLine(listaAutor.Autor[0].nazwisko);  // Mickiewicz
        Console.WriteLine(listaAutor.Autor[1].nazwisko);  // Słowacki
    }
}
