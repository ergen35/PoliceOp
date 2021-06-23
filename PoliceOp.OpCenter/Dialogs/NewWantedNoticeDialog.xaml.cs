using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Windows;

namespace PoliceOp.OpCenter.Dialogs
{
    /// <summary>
    /// Interaction logic for NewWantedNoticeDialog.xaml
    /// </summary>
    public partial class NewWantedNoticeDialog : HandyControl.Controls.PopupWindow
    {
        public List<Models.Personne> PList { get; set; }
        public string keyword { get; set; }
        public NewWantedNoticeDialog()
        {
            PList = new List<Models.Personne>();

            InitializeComponent();
        }

        private async void DetailsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void SendAvisBtn_Click(object sender, RoutedEventArgs e)
        {
            SendAvisBtn.IsEnabled = false;

            if (PersonnesListView.SelectedIndex < 0 || StatutCbbx.SelectedIndex < 0)
            {
                MessageBox.Show("Veuillez Renseigner tous les champs néccessaires");
                SendAvisBtn.IsEnabled = false;
                return;
            }

            LoadingInd.Visibility = Visibility.Visible;

            Models.AvisRecherche avis = new Models.AvisRecherche()
            {
                DateEmission = DateTime.Now,
                Informations = InfosTxtb.Text,
                PersonneRechercheeId = (PersonnesListView.SelectedItem as Models.Personne).PersonneId,
                StatutRecherche = StatutCbbx.SelectedItem.ToString(),
            };

            AppLevel.APIClients.AppRestClient2.Authenticator = new JwtAuthenticator(
                AppLevel.JWTAuthServices.jwtSvc.TokenizeSessionID(
                  AppLevel.CachingService.appCache.Get<Models.SessionVM>("SessionVM").SessionID, "New Avis"));

            var req = new RestRequest(resource: "AvisRecherche", Method.POST).AddJsonBody(avis);

            var response = await AppLevel.APIClients.AppRestClient2.ExecuteAsync(req);


            if (response.IsSuccessful)
            {
                AppLevel.NotificationManagers.ShowNotification("Avis Publié Avec Succès", "Info", AppLevel.NotificationLevel.Info);
            }
            else
            {
                AppLevel.NotificationManagers.ShowNotification(response.ResponseStatus.ToString(), "Info", AppLevel.NotificationLevel.Error);
                SendAvisBtn.IsEnabled = true;
            }

            LoadingInd.Visibility = Visibility.Collapsed;

        }

        private async void SearchWdgt_SearchStarted(object sender, HandyControl.Data.FunctionEventArgs<string> e)
        {
            if (SearchWdgt.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Veuillez Entrer un mot clé, Nom, Prénom, NPI, IFU....");
                return;
            }

            LoadingInd.Visibility = Visibility.Visible;

            AppLevel.APIClients.AppRestClient2.Authenticator = new JwtAuthenticator(
                    AppLevel.JWTAuthServices.jwtSvc.TokenizeSessionID(
                        AppLevel.CachingService.appCache.Get<Models.SessionVM>("SessionVM").SessionID, "search_for"));

            var Req = new RestRequest(resource: $"Identification/search/{keyword = SearchWdgt.Text}", method: Method.GET);

            var response = await AppLevel.APIClients.AppRestClient2.ExecuteAsync<List<Models.Personne>>(request: Req);

            if (response.IsSuccessful)
            {
                PList = response.Data;
                PersonnesListView.ItemsSource = PList;
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    AppLevel.NotificationManagers.ShowNotification("Requête Non Authorisée", "Avertissement", AppLevel.NotificationLevel.Warning);
                }
                else
                {
                    AppLevel.NotificationManagers.ShowNotification("Une Erreur est Survenue", "Erreur", AppLevel.NotificationLevel.Error);

                }
            }

            await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 4));
            e.Handled = true;

            LoadingInd.Visibility = Visibility.Collapsed;
        }
    }
}
