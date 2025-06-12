using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;

namespace bibKliSalnik
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().Title = "BIBLIOTEKA ©Pawel Salnik";

            // Punkt 5a - ustaw szerokość menu masz w XAML (OpenPaneLength="200")

            // Punkt 5b - włącz ikonę "Wstecz"
            NavView.IsBackEnabled = true;

            // Podpięcie obsługi zdarzeń
            NavView.BackRequested += NavView_BackRequested;
            NavView.ItemInvoked += NavView_ItemInvoked;
        }

        // Punkt 5b ii - obsługa kliknięcia "Wstecz"
        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (frmMain.CanGoBack)
            {
                frmMain.GoBack();
            }
        }

        // Punkt 5d i, e - obsługa kliknięcia pozycji menu
        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                frmMain.Navigate(typeof(SettingsPage));
                return;
            }

            var item = args.InvokedItemContainer as NavigationViewItem;
            if (item == null) return;

            switch (item.Name)
            {
                case "AuthorListMenuItem":
                    frmMain.Navigate(typeof(AuthorListMenuItem));
                    break;

                case "PublisherListMenuItem":
                    frmMain.Navigate(typeof(PublisherListMenuItem));
                    break;

                case "BookListMenuItem":
                    frmMain.Navigate(typeof(BookListMenuItem));
                    break;

                case "WebPageMenuItem":
                    _ = Windows.System.Launcher.LaunchUriAsync(new Uri("http://www.ukw.edu.pl"));
                    break;

                case "HelpPageMenuItem":
                    frmMain.Navigate(typeof(HelpPage));
                    break;

                default:
                    break;
            }
        }

        // Twoje istniejące eventy - możesz je usunąć lub pozostawić (jeśli korzystasz z nich w XAML)
        private async void btStronaWWW_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("http://www.ukw.edu.pl"));
        }

        private void btUstawienia_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        private void btPomoc_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HelpPage));
        }
    }
}
