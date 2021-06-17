using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System;
using System.Windows;

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
            this.Loaded += DiffusionsList_Loaded;
        }

        private void DiffusionsList_Loaded(object sender, RoutedEventArgs e)
        {
            this.SelectedDiffusion = new Models.Diffusion();
        }

        private async void Fetch_Data()
        {
            AppLevel.APIClients.AppRestClient2.Authenticator = new JwtAuthenticator(AppLevel.JWTAuthServices.jwtSvc.TokenizeSessionID(
                                     AppLevel.CachingService.appCache.Get<Models.SessionVM>("SessionVM").SessionID, "Get-Diffusions"));

            var req = new RestRequest("Diffusions", RestSharp.DataFormat.Json);

            var response = await AppLevel.APIClients.AppRestClient2.ExecuteAsync<List<Models.Diffusion>>(request: req, Method.GET);


            if (response.IsSuccessful)
            {
                this.Diffusions = response.Data;

                this.DiffusionsListView.ItemsSource = this.Diffusions;
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
            if (this.DiffusionsListView.Items.Count > 0)
            {
                this.DetailsCard.Visibility = Visibility.Collapsed;

                if (this.DiffusionsListView.SelectedIndex > -1)
                {
                    var selectedItem = (DiffusionsListView.SelectedItem as Models.Diffusion);
                    this.SelectedDiffusion = selectedItem;

                    this.DataContext = this;

                    this.DetailsCard.Visibility = Visibility.Visible;
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
