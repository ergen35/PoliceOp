using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PoliceOp.OpCenter.Pages
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {

        public string keyword { get; set; }
        public List<Models.Personne> PList { get; set; }
        public SearchPage()
        {
            PList = new List<Models.Personne>();

            InitializeComponent();
        }



        private async void DetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            int PersonneId = Convert.ToInt32((sender as Button).Tag);

            foreach (Window item in App.Current.Windows)
            {
                if (item.Title.ToLower() == "opcenter")
                {
                    (item as MainWindow).LoadingIndicator.Visibility = Visibility.Visible;

                    await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 9));

                    (item as MainWindow).ContentFrame.Navigate(new BioPage(PersonneId));
                    break;
                }
            }

        }

        private async void SearchWdgt_SearchStarted(object sender, HandyControl.Data.FunctionEventArgs<string> e)
        {
            if (SearchWdgt.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Veuillez Entrer un mot clé, Nom, Prénom, NPI, IFU....");
                return;
            }

            SearchLoadingLine.Visibility = Visibility.Visible;

            AppLevel.APIClients.AppRestClient2.Authenticator = new JwtAuthenticator(
                    AppLevel.JWTAuthServices.jwtSvc.TokenizeSessionID(
                        AppLevel.CachingService.appCache.Get<Models.SessionVM>("SessionVM").SessionID, "search_for"));

            var Req = new RestRequest(resource: $"Identification/search/{keyword = SearchWdgt.Text}", method: Method.GET);

            var response = await AppLevel.APIClients.AppRestClient2.ExecuteAsync<List<Models.Personne>>(request: Req);

            if (response.IsSuccessful)
            {
                PList = response.Data;
                MessageBox.Show(PList.First().PersonnePhoto);
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

            SearchLoadingLine.Visibility = Visibility.Collapsed;
        }
    }

}
