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

        //private void Authentication_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Escape || e.Key == Key.Enter || e.Key == Key.Space)
        //    {
        //        MessageBox.Show("KeyEvent fired");
        //    }
        //}

        private async void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            Models.Session session = new Models.Session();

            string login = this.LoginTxtb.Text;
            string pwd = this.PwdTxtb.Password;

            if ( login != "" &&  pwd != "")
            {
                if (this.LoginTxtb.VerifyData())
                {
                    //Send Request to API


                    this.AuthBtn.Content = new MahApps.Metro.IconPacks.PackIconFontAwesome() { 
                        Kind = MahApps.Metro.IconPacks.PackIconFontAwesomeKind.CircleNotchSolid,
                        Spin = true
                    };

                        
                    try
                    {
                        session = await AppLevel.APIClients.v1Client
                                        .PostRequest(route: "Auth")
                                        .WithOAuthBearer(jWTServices.TokenizeID(login, pwd, "Auth", Models.Issuers.OpCenterApp, Models.Audiences.PoliceOpAPI))
                                        .ExecuteAsync<Models.Session>();
                    }
                    catch (HttpException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound || ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ShowNotification("L'authentification a échoué", "#F15B19", "#F15B19", "Echec");
                    }
                    catch (HttpException ex) when (ex.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ShowNotification("Le Serveur est Hors Service", "#F15B19", "#F15B19", "Echec");
                    }
                    catch(Exception)
                    {
                        ShowNotification("Impossible d'atteindre le serveur\nUne Erreur s'est Produite", "#F15B19", "#F15B19", "Echec");
                    }

                    await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 5));

                    this.AuthBtn.Content = new MahApps.Metro.IconPacks.PackIconBoxIcons()
                    {
                        Kind = MahApps.Metro.IconPacks.PackIconBoxIconsKind.RegularCheckShield,
                        Spin = false
                    };


                    if (session.SessionID == (new Models.Session()).SessionID)
                    {
                        return;
                    }
                    
                    else
                    {
                        // Get Key from AppConfig
                        string key = System.Configuration.ConfigurationManager.AppSettings.Get("HashKey");
                        
                        //Generate Hash

                        //Caching

                        AppLevel.CachingService.appCache.Add<string>("SessionID", session.SessionID.ToString(), new TimeSpan(23, 30, 0));

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
