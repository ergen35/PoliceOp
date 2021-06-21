using LazyCache;
using MahApps.Metro.IconPacks;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;



namespace PoliceOp.OpCenter
{
    /// <summary>
    /// Interaction logic for Authentication.xaml
    /// </summary>
    public partial class Authentication : MahApps.Metro.Controls.MetroWindow
    {
        public Authentication()
        {
            InitializeComponent();

            this.AuthMessages.Manager = AppLevel.NotificationManagers.AuthNotificationsManager;
        }

        private async void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            Models.SessionVM sessionVM; ;

            string login = this.LoginTxtb.Text;
            string pwd = this.PwdTxtb.Password;

            if (login == "" || pwd == "")
            {
                MessageBox.Show("Veuillez Renseigner le Login et Mot de Passe",
                              "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                if (this.LoginTxtb.VerifyData())
                {
                    this.AuthBtn.Content = new PackIconFontAwesome()
                    {
                        Kind = PackIconFontAwesomeKind.CircleNotchSolid,
                        Spin = true
                    };


                    AppLevel.APIClients.AppRestClient1.Authenticator = new JwtAuthenticator(
                                        AppLevel.JWTAuthServices.jwtSvc.TokenizeID(login, pwd, "Auth",
                                        Models.Issuers.OpCenterApp, Models.Audiences.PoliceOpAPI));


                    var Request = new RestRequest("Auth", RestSharp.DataFormat.Json);

                    var response = await AppLevel.APIClients.AppRestClient1.ExecutePostAsync<Models.SessionVM>(Request);

                    if (response.IsSuccessful)
                    {
                        sessionVM = response.Data;
                    }
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            AppLevel.NotificationManagers.ShowAuthNotification("Mauvaises informations de compte", "Erreur", AppLevel.NotificationLevel.Error);
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        {
                            AppLevel.NotificationManagers.ShowAuthNotification("Jeton d'authorisation inexistant", "Erreur", AppLevel.NotificationLevel.Error);
                        }
                        else
                        {
                            AppLevel.NotificationManagers.ShowAuthNotification("Une erreur est survenue" + response.StatusDescription, "Erreur", AppLevel.NotificationLevel.Error);
                        }

                        this.AuthBtn.Content = new PackIconBoxIcons()
                        {
                            Kind = PackIconBoxIconsKind.RegularCheckShield,
                            Spin = false
                        };

                        return;
                    }

                    await Task.Delay(new TimeSpan(0, 0, 3));

                    this.AuthBtn.Content = new PackIconBoxIcons()
                    {
                        Kind = PackIconBoxIconsKind.RegularCheckShield,
                        Spin = false
                    };

                    if (sessionVM.SessionID == (new Models.Session()).SessionID.ToString())
                    {
                        AppLevel.NotificationManagers.ShowAuthNotification("Une nouvelle session ne peut être ouverte", "Info", AppLevel.NotificationLevel.Info);
                        return;
                    }

                    //Caching
                    AppLevel.CachingService.appCache.Add<Models.SessionVM>("SessionVM", sessionVM, new TimeSpan(23, 30, 0));

                    //Close this window and try to call The OpCenter
                    this.Visibility = Visibility.Collapsed;
                    //Clear Password
                    this.PwdTxtb.Clear();

                    // When OpCenter Opens, check Hash in the cache
                    MainWindow MW = new MainWindow();
                    MW.Show();
                }
                else
                {
                    MessageBox.Show("La Vérification des données entrées a échoué\nVeuillez réésayer",
                                    "Avertissement", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                }
            }

        }

        private void FingerPrintIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            this.FingerPrintIcon.Effect = new HandyControl.Media.Effects.BrightnessEffect();

        }

        private void FingerPrintIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            this.FingerPrintIcon.Effect = new System.Windows.Media.Effects.BlurEffect();
        }

    }
}
