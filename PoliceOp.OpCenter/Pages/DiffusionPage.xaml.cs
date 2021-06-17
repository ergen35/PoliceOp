using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Enterwell.Clients.Wpf.Notifications;
using Microsoft.Win32;

namespace PoliceOp.OpCenter.Pages
{
    /// <summary>
    /// Interaction logic for DiffusionPage.xaml
    /// </summary>
    public partial class DiffusionPage : Page
    {
        public DiffusionPage()
        {
            InitializeComponent();
        }

        private async void DiffuseBtn_Click(object sender, RoutedEventArgs e)
        {

            Models.Diffusion Diff = new Models.Diffusion()
            {
                AuthorId = AppLevel.CachingService.appCache.Get<Models.SessionVM>("SessionVM").AgentID,
                Sujet = this.Subject.Text,
                Cible = this.Cible.Text.ToString(),
                Details = this.Details.Text,
                
                //If Any
                PiecesJointes = new List<Models.PieceJointe>()
            };


            AppLevel.APIClients.AppRestClient1.Authenticator = new RestSharp.Authenticators.JwtAuthenticator(AppLevel.JWTAuthServices.jwtSvc.TokenizeSessionID(
                                                                   AppLevel.CachingService.appCache.Get<Models.SessionVM>("SessionVM").SessionID, "Diffuse"));

            var req = new RestSharp.RestRequest("Diffusions").AddJsonBody(Diff);

            var response = await AppLevel.APIClients.AppRestClient1.ExecutePostAsync(req);

            if (response.IsSuccessful)
            {
                AppLevel.NotificationManagers.ShowNotification("Diffusion Effectuée !", "info");
                this.DiffuseBtn.Visibility = Visibility.Collapsed;
                
                this.BackBtn_Click(this, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    AppLevel.NotificationManagers.ShowNotification("Requête non Authorisée", "Avertissement", AppLevel.NotificationLevel.Warning);
                }
                else
                {
                    AppLevel.NotificationManagers.ShowNotification("Une erreur est survenue", "Erreur", AppLevel.NotificationLevel.Error);
                }
            }
        }

        private async void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window item in App.Current.Windows)
            {
                if (item.Title.ToLower() == "opcenter")
                {
                    (item as MainWindow).LoadingIndicator.Visibility = Visibility.Visible;

                    await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 2));

                    (item as MainWindow).ContentFrame.Navigate(new DiffusionsList());
                    break;
                }
            }
        }

        private void AddPJ_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OPFD = new OpenFileDialog();

            OPFD.ShowDialog();
        }
    }
}
