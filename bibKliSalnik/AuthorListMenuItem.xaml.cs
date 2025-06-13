using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using bibModelSalnik.Model;
using System.Xml.Serialization;

namespace bibKliSalnik
{
    public sealed partial class AuthorListMenuItem : Page
    {
        private DataGridDataSourceAuthors AuthorsViewModel;

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
        }

        private async void SaveAuthors()
        {
            try
            {
                var authors = new Autorzy
                {
                    Autor = (authorsDataGrid.ItemsSource as List<AutorzyAutor>)?.ToArray()
                };

                if (authors.Autor == null) return;

                StorageFolder folder = KnownFolders.DocumentsLibrary;
                StorageFile file = await folder.CreateFileAsync("autorzy_Salnik.xml", CreationCollisionOption.ReplaceExisting);

                using (var stream = await file.OpenStreamForWriteAsync())
                {
                    var xs = new XmlSerializer(typeof(Autorzy));
                    xs.Serialize(stream, authors);
                }
            }
            catch (Exception ex)
            {
                var dialog = new Windows.UI.Popups.MessageDialog("Błąd podczas zapisu autorów: " + ex.Message);
                await dialog.ShowAsync();
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            SaveAuthors(); // zapisz dane przed opuszczeniem strony
            base.OnNavigatingFrom(e);
        }
    }
}
