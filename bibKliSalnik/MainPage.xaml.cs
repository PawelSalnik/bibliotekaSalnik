using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.Storage;

namespace bibKliSalnik
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().Title = "BIBLIOTEKA ©Pawel Salnik";

            var app = (App)App.Current;
            _ = app.dbUWP.TestData();



            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("AppTheme", out object value))
            {
                if (value is int themeValue && Enum.IsDefined(typeof(ElementTheme), themeValue))
                {
                    if (Window.Current.Content is FrameworkElement rootElement)
                    {
                        rootElement.RequestedTheme = (ElementTheme)themeValue;
                    }
                }
            }




            // Zdarzenia dla przycisku "Wstecz" i menu
            NavView.BackRequested += NavView_BackRequested;
            NavView.ItemInvoked += NavView_ItemInvoked;

            // Nasłuch zmiany nawigacji — by aktualizować IsBackEnabled
            frmMain.Navigated += FrmMain_Navigated;
        }

        // Ustawienie widoczności przycisku "Wstecz" (punkt 8a)
        private void FrmMain_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = frmMain.CanGoBack;
        }

        // Obsługa przycisku "←"
        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (frmMain.CanGoBack)
            {
                frmMain.GoBack();
            }
        }

        // Obsługa menu
        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                // Punkt 8b.i - nie przechodź ponownie do tej samej strony
                if (frmMain.CurrentSourcePageType != typeof(SettingsPage))
                    frmMain.Navigate(typeof(SettingsPage));
                return;
            }

            var item = args.InvokedItemContainer as NavigationViewItem;
            if (item == null) return;

            switch (item.Name)
            {
                case "AuthorListMenuItem":
                    if (frmMain.CurrentSourcePageType != typeof(AuthorListMenuItem))
                        frmMain.Navigate(typeof(AuthorListMenuItem));
                    break;

                case "PublisherListMenuItem":
                    if (frmMain.CurrentSourcePageType != typeof(PublisherListMenuItem))
                        frmMain.Navigate(typeof(PublisherListMenuItem));
                    break;

                case "BookListMenuItem":
                    if (frmMain.CurrentSourcePageType != typeof(BookListMenuItem))
                        frmMain.Navigate(typeof(BookListMenuItem));
                    break;

                case "WebPageMenuItem":
                    _ = Windows.System.Launcher.LaunchUriAsync(new Uri("http://www.ukw.edu.pl"));
                    break;

                case "HelpPageMenuItem":
                    // Punkt 8b.ii
                    if (frmMain.CurrentSourcePageType != typeof(HelpPage))
                        frmMain.Navigate(typeof(HelpPage));
                    break;
            }
        }

        // Stare metody kliknięć (opcjonalne)
        private async void btStronaWWW_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("http://www.ukw.edu.pl"));
        }

        private void btUstawienia_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (frmMain.CurrentSourcePageType != typeof(SettingsPage))
                frmMain.Navigate(typeof(SettingsPage));
        }

        private void btPomoc_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (frmMain.CurrentSourcePageType != typeof(HelpPage))
                frmMain.Navigate(typeof(HelpPage));
        }
    }
}
