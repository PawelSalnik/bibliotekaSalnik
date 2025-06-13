using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using bibModelSalnik.Model;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace bibKliSalnik
{
    public class BDLibraryUWP
    {
        public StorageFolder documentsFolder;
        public string dataFileName;
        public List<AutorzyAutor> AuthorsLst;


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




        public BDLibraryUWP()
        {
            documentsFolder = KnownFolders.DocumentsLibrary;
            dataFileName = DefaultFileNames.plikKsiazki;
            dataFileName = DefaultFileNames.plikWydawnictwa;
            dataFileName = DefaultFileNames.plikAutorzy;
        }

        public async Task<bool> TestData()
        {
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

            // próba odczytu pliku autorów
            IStorageItem itemAutorzy = await documentsFolder.TryGetItemAsync(DefaultFileNames.plikAutorzy);
            if (itemAutorzy is StorageFile plikAutorzy)
            {
                Autorzy dane = Deserialize<Autorzy>(plikAutorzy);
                if (dane != null)
                    AuthorsLst = dane.Autor.ToList();
            }

            return true;
        }

    }
}
