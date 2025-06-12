using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace bibKliSalnik
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private void DarkTheme_Checked(object sender, RoutedEventArgs e)
        {
            ApplyTheme(ElementTheme.Dark);
        }

        private void LightTheme_Checked(object sender, RoutedEventArgs e)
        {
            ApplyTheme(ElementTheme.Light);
        }

        private void DefaultTheme_Checked(object sender, RoutedEventArgs e)
        {
            ApplyTheme(ElementTheme.Default);
        }

        private void ApplyTheme(ElementTheme theme)
        {
            if (Window.Current.Content is FrameworkElement rootElement)
            {
                rootElement.RequestedTheme = theme;

                // Zapisz motyw do lokalnych ustawień jako int
                ApplicationData.Current.LocalSettings.Values["AppTheme"] = (int)theme;
            }
        }
    }
}
