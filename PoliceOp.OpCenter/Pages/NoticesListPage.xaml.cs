using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PoliceOp.OpCenter.Pages
{
    /// <summary>
    /// Interaction logic for NoticesListPage.xaml
    /// </summary>
    public partial class NoticesListPage : Page
    {
        public List<Models.AvisRecherche> ListWanted { get; set; }

        public NoticesListPage()
        {
            ListWanted = new List<Models.AvisRecherche>();
            FetchData();
            InitializeComponent();
        }

        private async void FetchData()
        {
            AppLevel.APIClients.AppRestClient3.Authenticator = new JwtAuthenticator(
                AppLevel.JWTAuthServices.jwtSvc.TokenizeSessionID(
                    AppLevel.CachingService.appCache.Get<Models.SessionVM>("SessionVM").SessionID, "wanted_notices"));

            var req = new RestRequest(resource: "AvisRecherche", DataFormat.Json);
            var response = await AppLevel.APIClients.AppRestClient3.ExecuteAsync<List<Models.AvisRecherche>>(request: req, httpMethod: Method.GET);


            if (response.IsSuccessful)
            {
                ListWanted = response.Data;
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    AppLevel.NotificationManagers.ShowNotification("Requête non Authorisée", "Avertissement", AppLevel.NotificationLevel.Warning);
                }
                else
                {
                    AppLevel.NotificationManagers.ShowNotification("Une Erreur S'est Produite", "Erreur", AppLevel.NotificationLevel.Warning);
                }
            }
        }
    }
}
