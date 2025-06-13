using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using bibModelSalnik.Model;
using System.Xml.Serialization;

namespace bibKliSalnik
{
    public class BDLibraryUWP
    {
        public StorageFolder documentsFolder;
        public string dataFileName;
        public List<AutorzyAutor> AuthorsLst;

        public BDLibraryUWP()
        {
            // Folder Dokumenty
            documentsFolder = KnownFolders.DocumentsLibrary;

            // Domyślna nazwa pliku (można nadpisać)
            dataFileName = DefaultFileNames.plikAutorzy;
        }

        /// <summary>
        /// Deserializacja XML (prywatna pomocnicza)
        /// </summary>
        private T Deserialize<T>(StorageFile file)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                using (Stream reader = file.OpenStreamForReadAsync().Result)
                {
                    return (T)xmlSerializer.Deserialize(reader);
                }
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Ładowanie danych autorów z pliku
        /// </summary>
        public async Task<bool> TestData()
        {
            // Test pliku ogólnie
            var item = await documentsFolder.TryGetItemAsync(dataFileName);
            if (item == null)
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Brak danych",
                    Content = "Brakuje pliku danych.\nZałóż go w module administracyjnym.",
                    CloseButtonText = "Zamknij"
                };

                await dialog.ShowAsync();
                App.Current.Exit();
                return false;
            }

            // Próba odczytu pliku autorów
            IStorageItem itemAutorzy = await documentsFolder.TryGetItemAsync(DefaultFileNames.plikAutorzy);
            if (itemAutorzy is StorageFile plikAutorzy)
            {
                Autorzy dane = Deserialize<Autorzy>(plikAutorzy);
                if (dane != null)
                    AuthorsLst = dane.Autor.ToList();
            }

            return true;
        }

        /// <summary>
        /// Zapis danych autorów do pliku XML w Dokumentach
        /// </summary>
        /// <param name="authors">obiekt klasy Autorzy</param>
        /// <param name="fileName">nazwa pliku np. "autorzy_Salnik.xml"</param>
        public async void SaveAuthorsToFile(Autorzy authors, string fileName)
        {
            try
            {
                StorageFile file = await documentsFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                using (Stream stream = await file.OpenStreamForWriteAsync())
                {
                    var serializer = new XmlSerializer(typeof(Autorzy));
                    serializer.Serialize(stream, authors);
                }
            }
            catch (Exception ex)
            {
                var dialog = new Windows.UI.Popups.MessageDialog("Błąd zapisu XML autorów: " + ex.Message);
                await dialog.ShowAsync();
            }
        }
    }
}
