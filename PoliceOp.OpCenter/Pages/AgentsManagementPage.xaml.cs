﻿using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PoliceOp.OpCenter.Pages
{
    /// <summary>
    /// Interaction logic for AgentsManagementPage.xaml
    /// </summary>
    public partial class AgentsManagementPage : Page
    {
        public string keyword { get; set; }
        public List<Models.Agent> AList { get; set; }
        public List<Models.Agent> AListLimited { get; set; }

        public AgentsManagementPage()
        {
            AList = new List<Models.Agent>();

            InitializeComponent();

            Loaded += AgentsManagementPage_Loaded;
        }

        private void AgentsManagementPage_Loaded(object sender, RoutedEventArgs e)
        {
            SearchWdgt.Text = "Ang";
            SearchWdgt_SearchStarted(this, null);
            SearchWdgt.Text = string.Empty;
        }

        private async void SearchWdgt_SearchStarted(object sender, HandyControl.Data.FunctionEventArgs<string> e)
        {
            if (SearchWdgt.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Veuillez Entrer un mot clé, Matricule, Nom, Prénom, NPI, IFU....");
                return;
            }

            SearchLoadingLine.Visibility = Visibility.Visible;

            AppLevel.APIClients.AppRestClient2.Authenticator = new JwtAuthenticator(
                    AppLevel.JWTAuthServices.jwtSvc.TokenizeSessionID(
                        AppLevel.CachingService.appCache.Get<Models.SessionVM>("SessionVM").SessionID, "search_for"));

            var Req = new RestRequest(resource: $"Agents/search/{keyword = SearchWdgt.Text}", method: Method.GET);

            var response = await AppLevel.APIClients.AppRestClient2.ExecuteAsync<List<Models.Agent>>(request: Req);

            if (response.IsSuccessful)
            {
                AList = response.Data;
                AgentsListView.ItemsSource = AList;
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

            SearchLoadingLine.Visibility = Visibility.Collapsed;
        }


    }
}
