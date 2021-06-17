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
                AuthorId = 5,
                Cible = this.Cible.Text.ToString(),
                Details = this.Details.Text,
                DiffusionId = 4,
                Sujet = this.Subject.Text,
                PiecesJointes = null
            };


            AppLevel.APIClients.AppRestClient1.Authenticator = new RestSharp.Authenticators.JwtAuthenticator(AppLevel.JWTAuthServices.jwtSvc.TokenizeSessionID(
                                                                    AppLevel.CachingService.appCache.Get<Models.SessionVM>("SessionVM").SessionID, "Diffuse"));

            var req = new RestSharp.RestRequest("Diffusions").AddJsonBody(Diff);

            var response = await AppLevel.APIClients.AppRestClient1.ExecutePostAsync(req);

            if (response.IsSuccessful)
            {
                this.ShowNotification("Diffusion Effectuée", "#333", "#E0A030", "info");
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    this.ShowNotification("Requête non Authorisée", "#333", "#E0A030", "Avertissement");
                }
                else
                {
                    this.ShowNotification("Une erreur est survenue", "#333", "#E0A030", "Erreur");
                }
            }

        }

        private void ShowNotification(string Message, String BgBrush, String AccentBrush, string BadgeInfo)
        {

            AppLevel.NotificationManagers.InAppNotificationsManager.CreateMessage()
                                        .Accent(AccentBrush)
                                        .Animates(true)
                                        .AnimationInDuration(0.75)
                                        .AnimationOutDuration(2)
                                        .Background(BgBrush)
                                        .HasBadge(BadgeInfo)
                                        .HasMessage(Message)
                                        .Dismiss().WithButton("Ok", button => { })
                                        .Dismiss().WithDelay(TimeSpan.FromSeconds(25))
                                        .Queue();

            AppLevel.NotificationManagers.InAppNotificationsManager = new NotificationMessageManager();
        }
    }
}
