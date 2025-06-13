using System;
using System.Collections.ObjectModel;
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
        private ObservableCollection<AutorzyAutor> AuthorsObservable;

        public AuthorListMenuItem()
        {
            this.InitializeComponent();

            var app = (App)App.Current;
            if (app.dbUWP?.AuthorsLst != null)
            {
                AuthorsObservable = new ObservableCollection<AutorzyAutor>(
                    app.dbUWP.AuthorsLst.OrderBy(a => a.nazwisko)
                );
            }
            else
            {
                AuthorsObservable = new ObservableCollection<AutorzyAutor>();
            }

            authorsDataGrid.ItemsSource = AuthorsObservable;
        }

        private async void SaveAuthors()
        {
            try
            {
                var authors = new Autorzy
                {
                    Autor = AuthorsObservable.ToArray()
                };

                // aktualizacja danych w dbUWP
                var app = (App)App.Current;
                app.dbUWP.AuthorsLst = AuthorsObservable.ToList();

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
            SaveAuthors();
            base.OnNavigatingFrom(e);
        }

        private void AddAuthor_Click(object sender, RoutedEventArgs e)
        {
            int nextId = AuthorsObservable.Any() ? AuthorsObservable.Max(a => a.id) + 1 : 1;

            var nowyAutor = new AutorzyAutor
            {
                id = (byte)nextId,
                imię = "",
                nazwisko = "",
                rokUr = 0
            };

            AuthorsObservable.Insert(0, nowyAutor);
        }

        private void DeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (authorsDataGrid.SelectedIndex >= 0 && authorsDataGrid.SelectedIndex < AuthorsObservable.Count)
            {
                AuthorsObservable.RemoveAt(authorsDataGrid.SelectedIndex);
            }
        }
    }
}
