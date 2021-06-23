using RestSharp;
using RestSharp.Authenticators;
using System;
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

            ListWanted = new List<Models.AvisRecherche>()
            {
                new Models.AvisRecherche()
                {
                    PersonneRecherchee = new Models.Personne(){Nom = "Gaston", Prenom = "Phil", PersonneId = 854},
                     DateEmission = DateTime.Now, Informations = "klfkjzejfjzefq", StatutRecherche = "Actif", UID = Guid.NewGuid()
                },
                  new Models.AvisRecherche()
                {
                    PersonneRecherchee = new Models.Personne(){Nom = "Gaston", Prenom = "Phil", PersonneId = 854},
                     DateEmission = DateTime.Now, Informations = "klfkjzejfjzefq", StatutRecherche = "Actif", UID = Guid.NewGuid()
                },
                    new Models.AvisRecherche()
                {
                    PersonneRecherchee = new Models.Personne(){Nom = "Gaston", Prenom = "Phil", PersonneId = 854},
                     DateEmission = DateTime.Now, Informations = "klfkjzejfjzefq", StatutRecherche = "Actif", UID = Guid.NewGuid()
                },
                      new Models.AvisRecherche()
                {
                    PersonneRecherchee = new Models.Personne(){Nom = "Gaston", Prenom = "Phil", PersonneId = 854},
                     DateEmission = DateTime.Now, Informations = "klfkjzejfjzefq", StatutRecherche = "Actif", UID = Guid.NewGuid()
                },
                       new Models.AvisRecherche()
                {
                    PersonneRecherchee = new Models.Personne(){Nom = "Gaston", Prenom = "Phil", PersonneId = 854},
                     DateEmission = DateTime.Now, Informations = "klfkjzejfjzefq", StatutRecherche = "Actif", UID = Guid.NewGuid()
                },
                          new Models.AvisRecherche()
                        {
                            PersonneRecherchee = new Models.Personne(){Nom = "Gaston", Prenom = "Phil", PersonneId = 854},
                             DateEmission = DateTime.Now, Informations = "klfkjzejfjzefq", StatutRecherche = "Actif", UID = Guid.NewGuid()
                        },
                            new Models.AvisRecherche()
                        {
                            PersonneRecherchee = new Models.Personne(){Nom = "Gaston", Prenom = "Phil", PersonneId = 854},
                             DateEmission = DateTime.Now, Informations = "klfkjzejfjzefq", StatutRecherche = "Actif", UID = Guid.NewGuid()
                        },
                              new Models.AvisRecherche()
                        {
                            PersonneRecherchee = new Models.Personne(){Nom = "Gaston", Prenom = "Phil", PersonneId = 854},
                             DateEmission = DateTime.Now, Informations = "klfkjzejfjzefq", StatutRecherche = "Actif", UID = Guid.NewGuid()
                        },
                               new Models.AvisRecherche()
                {
                    PersonneRecherchee = new Models.Personne(){Nom = "Gaston", Prenom = "Phil", PersonneId = 854},
                     DateEmission = DateTime.Now, Informations = "klfkjzejfjzefq", StatutRecherche = "Actif", UID = Guid.NewGuid()
                },
                                  new Models.AvisRecherche()
                                {
                                    PersonneRecherchee = new Models.Personne(){Nom = "Gaston", Prenom = "Phil", PersonneId = 854},
                                     DateEmission = DateTime.Now, Informations = "klfkjzejfjzefq", StatutRecherche = "Actif", UID = Guid.NewGuid()
                                },
                                    new Models.AvisRecherche()
                                {
                                    PersonneRecherchee = new Models.Personne(){Nom = "Gaston", Prenom = "Phil", PersonneId = 854},
                                     DateEmission = DateTime.Now, Informations = "klfkjzejfjzefq", StatutRecherche = "Actif", UID = Guid.NewGuid()
                                },
                                      new Models.AvisRecherche()
                {
                    PersonneRecherchee = new Models.Personne(){Nom = "Gaston", Prenom = "Phil", PersonneId = 854},
                     DateEmission = DateTime.Now, Informations = "klfkjzejfjzefq", StatutRecherche = "Actif", UID = Guid.NewGuid()
                },
            };

            WantedListView.ItemsSource = ListWanted;
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
                WantedListView.ItemsSource = ListWanted;
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

        private void NewNotice_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Dialogs.NewWantedNoticeDialog noticeDialog = new Dialogs.NewWantedNoticeDialog();

            noticeDialog.Show();
        }
    }
}
