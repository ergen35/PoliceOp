using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PoliceOp.OpCenter.Pages
{
    /// <summary>
    /// Interaction logic for BioPage.xaml
    /// </summary>
    public partial class BioPage : Page
    {
        public Models.Personne Personne { get; set; }
        public BioPage()
        {
            InitializeComponent();

            this.DataContext = this;

        }
        public BioPage(int Pid)
        {
            fecthPersonData(Pid);

            InitializeComponent();

            this.DataContext = this;
        }

        public async void fecthPersonData(int Pid)
        {

            AppLevel.APIClients.AppRestClient1.Authenticator = new JwtAuthenticator(
                AppLevel.JWTAuthServices.jwtSvc.TokenizeSessionID(
                    AppLevel.CachingService.appCache.Get<Models.SessionVM>("SessionVM").SessionID, "personData"));

            var req = new RestRequest($"Identification/{Pid}", RestSharp.DataFormat.Json);

            var response = await AppLevel.APIClients.AppRestClient1.ExecuteAsync<Models.Personne>(request: req, httpMethod: Method.GET);

            if (response.IsSuccessful)
            {
                this.Personne = response.Data;
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    AppLevel.NotificationManagers.ShowNotification("Requête non Authorisée", "Avertissement", AppLevel.NotificationLevel.Warning);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    AppLevel.NotificationManagers.ShowNotification("Impossible d'obtenir les informations demandées", "Erreur", AppLevel.NotificationLevel.Error);
                }
                else
                {
                    AppLevel.NotificationManagers.ShowNotification("Une Erreur est survenue", "Erreur", AppLevel.NotificationLevel.Error);
                }
            }

        }

        private async void BtnPere_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window item in App.Current.Windows)
            {
                if (item.Title.ToLower() == "opcenter")
                {
                    (item as MainWindow).LoadingIndicator.Visibility = Visibility.Visible;

                    await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 2));

                    (item as MainWindow).ContentFrame.Navigate(new BioPage(Personne.PereId));
                    break;
                }
            }
        }

        private async void BtnMere_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window item in App.Current.Windows)
            {
                if (item.Title.ToLower() == "opcenter")
                {
                    (item as MainWindow).LoadingIndicator.Visibility = Visibility.Visible;

                    await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 2));

                    (item as MainWindow).ContentFrame.Navigate(new BioPage(Personne.MereId));
                    break;
                }
            }
        }

        private void FaceDataBtn_Click(object sender, RoutedEventArgs e)
        {
            this.FaceDataExp.IsExpanded = !this.FaceDataExp.IsExpanded;
        }

        private void FingerPrintBtn_Click(object sender, RoutedEventArgs e)
        {
            this.FingerDataExp.IsExpanded = !this.FingerDataExp.IsExpanded;
        }

    }
}
