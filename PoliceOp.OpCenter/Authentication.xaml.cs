using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Extensions.Configuration;
using LazyCache;
using System.Windows.Threading;
using Enterwell.Clients.Wpf.Notifications;
using PoliceOp.OpCenter.Services;
using Tiny.RestClient;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;


namespace PoliceOp.OpCenter
{
    /// <summary>
    /// Interaction logic for Authentication.xaml
    /// </summary>
    public partial class Authentication : MahApps.Metro.Controls.MetroWindow
    {
        public JWTServices jWTServices { get; set; }
        public Authentication()
        {
            InitializeComponent();

            this.AuthMessages.Manager = AppLevel.NotificationManagers.AuthNotificationsManager;

            jWTServices = new JWTServices();

            //this.KeyDown += Authentication_KeyDown;
        }

        private async void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            Models.SessionVM sessionVM = new Models.SessionVM();

            string login = this.LoginTxtb.Text;
            string pwd = this.PwdTxtb.Password;

            if ( login != "" &&  pwd != "")
            {
                if (this.LoginTxtb.VerifyData())
                {
                    
                    // Laoding Icon

                    this.AuthBtn.Content = new MahApps.Metro.IconPacks.PackIconFontAwesome() { 
                        Kind = MahApps.Metro.IconPacks.PackIconFontAwesomeKind.CircleNotchSolid,
                        Spin = true
                    };


                    //Send Request to API
                    AppLevel.APIClients.AppRestClient1.Authenticator = new RestSharp.Authenticators.JwtAuthenticator(
                                                                                jWTServices.TokenizeID(login, pwd, "Auth",
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
                            this.ShowNotification("Mauvaises informations de compte", "#333", "#1751C3", "Error");
                            return;

                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        {
                            this.ShowNotification("Jeton d'authorisation inexistant", "#333", "#1751C3", "Error");
                            return;
                        }
                        else
                        {
                            this.ShowNotification("Une erreur est survenue" + response.StatusDescription, "#333", "#1751C3", "Error");
                            return;
                        }
                    }

                    await Task.Delay(new TimeSpan(0, 0, 3));

                    this.AuthBtn.Content = new MahApps.Metro.IconPacks.PackIconBoxIcons()
                    {
                        Kind = MahApps.Metro.IconPacks.PackIconBoxIconsKind.RegularCheckShield,
                        Spin = false
                    };



                    if (sessionVM.SessionID == (new Models.Session()).SessionID.ToString())
                    {
                        this.ShowNotification("Une nouvelle session ne peut être ouverte", "#333", "#1751C3", "Info");
                    }

                    else
                    {
                        // Get Key from AppConfig
                        string key = System.Configuration.ConfigurationManager.AppSettings.Get("HashKey");
                        
                        //Generate Hash

                        //Caching

                        AppLevel.CachingService.appCache.Add<Models.SessionVM>("SessionVM", sessionVM, new TimeSpan(23, 30, 0));

                        //Close this window and try to call The OpCenter
                        this.Visibility = Visibility.Collapsed;
                        //Clear Password
                        this.PwdTxtb.Clear();

                        // When OpCenter Opens, check Hash in the cache
                        MainWindow MW = new MainWindow();
                        MW.Show();

                        // When OpCenter Closes, Delete hash from cache
                    }

                }
                else
                    MessageBox.Show("La Vérification des données entrées a échoué\nVeuillez réésayer", "Avertissement", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
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

        private void ShowNotification(string Message, Brush BgBrush, Brush AccentBrush, string BadgeInfo)
        {
            AppLevel.NotificationManagers.AuthNotificationsManager.CreateMessage()
                                        .Accent(AccentBrush)
                                        .Animates(true)
                                        .AnimationInDuration(0.75)
                                        .AnimationOutDuration(2)
                                        .Background(BgBrush)
                                        .HasBadge(BadgeInfo)
                                        .HasMessage(Message)
                                        .Dismiss().WithButton("Ok", button => { })
                                        .Dismiss().WithButton("Réesayer", button => { AuthBtn_Click(this, null); })
                                        .Dismiss().WithDelay(TimeSpan.FromSeconds(6))
                                        .Queue();
        }

        private void ShowNotification(string Message, String BgBrush, String AccentBrush, string BadgeInfo)
        {
            AppLevel.NotificationManagers.AuthNotificationsManager.CreateMessage()
                                        .Accent(AccentBrush)
                                        .Animates(true)
                                        .AnimationInDuration(0.75)
                                        .AnimationOutDuration(2)
                                        .Background(BgBrush)
                                        .HasBadge(BadgeInfo)
                                        .HasMessage(Message)
                                        .Dismiss().WithButton("Ok", button => { })
                                        .Dismiss().WithButton("Réesayer", button => { AuthBtn_Click(this, null); })
                                        .Dismiss().WithDelay(TimeSpan.FromSeconds(6))
                                        .Queue();
        }
    }
}
