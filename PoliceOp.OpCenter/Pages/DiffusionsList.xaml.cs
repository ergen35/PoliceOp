using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PoliceOp.OpCenter.Pages
{
    /// <summary>
    /// Interaction logic for DiffusionsList.xaml
    /// </summary>
    public partial class DiffusionsList : Page
    {
        public List<Models.Diffusion> Diffusions { get; set; }
        public Models.Diffusion SelectedDiffusion { get; set; }

        public DiffusionsList()
        {
            Fetch_Data();

            InitializeComponent();
            Loaded += DiffusionsList_Loaded;
        }

        private void DiffusionsList_Loaded(object sender, RoutedEventArgs e)
        {
            SelectedDiffusion = new Models.Diffusion();
        }

        private async void Fetch_Data()
        {
            AppLevel.APIClients.AppRestClient2.Authenticator = new JwtAuthenticator(AppLevel.JWTAuthServices.jwtSvc.TokenizeSessionID(
                                     AppLevel.CachingService.appCache.Get<Models.SessionVM>("SessionVM").SessionID, "Get-Diffusions"));

            var req = new RestRequest("Diffusions", RestSharp.DataFormat.Json);

            var response = await AppLevel.APIClients.AppRestClient2.ExecuteAsync<List<Models.Diffusion>>(request: req, Method.GET);


            if (response.IsSuccessful)
            {
                Diffusions = response.Data;

                DiffusionsListView.ItemsSource = Diffusions;
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    AppLevel.NotificationManagers.ShowNotification("Requête Non Authorisée", "Avertissement", AppLevel.NotificationLevel.Warning);
                }
                else
                {
                    AppLevel.NotificationManagers.ShowNotification("Une Erreur est survenue", "Erreur", AppLevel.NotificationLevel.Error);
                }
            }
        }

        private void DiffusionsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DiffusionsListView.Items.Count > 0)
            {
                DetailsCard.Visibility = Visibility.Collapsed;

                if (DiffusionsListView.SelectedIndex > -1)
                {
                    var selectedItem = (DiffusionsListView.SelectedItem as Models.Diffusion);
                    SelectedDiffusion = selectedItem;

                    DataContext = this;

                    DetailsCard.Visibility = Visibility.Visible;
                }
            }
        }

        private async void NewDiffusion_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window item in App.Current.Windows)
            {
                if (item.Title.ToLower() == "opcenter")
                {
                    (item as MainWindow).LoadingIndicator.Visibility = Visibility.Visible;

                    await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 5));

                    (item as MainWindow).ContentFrame.Navigate(new DiffusionPage());
                    break;
                }
            }
        }
    }
}
