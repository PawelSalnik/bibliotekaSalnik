using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using bibModelSalnik.Model;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace bibKliSalnik
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class AuthorListMenuItem : Page
    {
        public AuthorListMenuItem()
        {
            this.InitializeComponent();
            AuthorsViewModel = new DataGridDataSourceAuthors();

            var app = (App)App.Current;
            if (app.dbUWP?.AuthorsLst != null)
            {
                // sortowanie wg nazwiska
                AuthorsViewModel.Autorzy = (from a in app.dbUWP.AuthorsLst
                                            orderby a.nazwisko
                                            select a).ToList();
            }
            else
            {
                AuthorsViewModel.Autorzy = new List<AutorzyAutor>(); // fallback pusty
            }

            this.InitializeComponent();


        }
        private DataGridDataSourceAuthors AuthorsViewModel;


    }
}
