using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Linq.Expressions;
using LazyCache;
using ControlzEx.Theming;
using Enterwell.Clients.Wpf.Notifications;
using Tiny.RestClient;
using PoliceOp.OpCenter.Services;

namespace PoliceOp.OpCenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MahApps.Metro.Controls.MetroWindow
    {
        public Models.SessionVM SessionVM { get; set; }
        public Pages.HomePage Home { get; set; }
        public Models.Agent Agent { get; set; }

        public JWTServices jWTServices { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;


            try
            {
                SessionVM = AppLevel.CachingService.appCache.Get<Models.SessionVM>("SessionVM");
               
                if (SessionVM.SessionID == (new Models.Session()).SessionID.ToString())
                {
                    this.Close();
                }

            }
            catch (Exception)
            {
                this.Close();
            }

            jWTServices = new JWTServices();


            this.Home = new Pages.HomePage();

            this.ContentFrame.Navigate(Home, null);

            this.AlertCenterMessageContainer.Manager = AppLevel.NotificationManagers.AlertCenterManager;
            this.InAppMessageContainer.Manager = AppLevel.NotificationManagers.InAppNotificationsManager;

            this.Closed += MainWindow_Closed;

            //Load Agent Info once window is loaded
            this.Loaded += MainWindow_Loaded;

        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                AppLevel.APIClients.v1Client = new TinyRestClient(new System.Net.Http.HttpClient(), System.Configuration.ConfigurationManager.AppSettings["v1APIUrl"].ToString());
                
                //Models.Personne P = await AppLevel.APIClients.v1Client
                //            .GetRequest(route: "Agents")
                //            .WithOAuthBearer(jWTServices.TokenizeSessionID(SessionVM.SessionID.ToString(), "getAgentByID"))
                //            .AddQueryParameter("id", 1 /*SessionVM.AgentID*/)
                //            .ExecuteAsync<Models.Personne>();

                ShowNotification("Fait", "#333", "#1751C3", "Ok");

            }
            catch (HttpException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound || ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                ShowNotification("L'authentification a échoué", "#F15B19", "#F15B19", "Echec");
            }
            catch (HttpException ex) when (ex.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                ShowNotification("Le Serveur est Hors Service", "#F15B19", "#F15B19", "Echec");
            }
            catch (Exception ex)
            {
                ShowNotification($"Une Erreur s'est Produite\n{ex.Message}", "#F15B19", "#F15B19", "Echec");
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            if (App.Current.Windows.Count > 0)
            {
                if (App.Current.Windows[0] != null)
                {
                    App.Current.Windows[0].Visibility = Visibility.Visible;
                }
            }
        }

        private void ToogleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.ToogleDrawer.Visibility = Visibility.Hidden;
        }

        private void DrawerLeft_Closed(object sender, RoutedEventArgs e)
        {
            this.ToogleDrawer.Visibility = Visibility.Visible;
        }

        private void ThemeBtn_Checked(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeThemeBaseColor(App.Current, "Dark");
            ThemeManager.Current.ChangeThemeColorScheme(App.Current, "Emerald");
        }

        private void ThemeBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeThemeBaseColor(App.Current, "Light");
            ThemeManager.Current.ChangeThemeColorScheme(App.Current, "Pink");
        }

        private void EnhancedSecurityMode_Click(object sender, RoutedEventArgs e)
        {
            HandyControl.Controls.PopupWindow popup = new HandyControl.Controls.PopupWindow()
            {
                MinWidth = 400,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ShowInTaskbar = false,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None
            };
            //popup.PopupElement
            popup.ShowDialog();

        }

        private void NotificationCenter_Click(object sender, RoutedEventArgs e)
        {
            // Generate a new notification
           
        }

        private void ClearAlertsBtn_Click(object sender, RoutedEventArgs e)
        {
            this.AlertCenterMessageContainer.Items.Clear();
            
        }

        private void AlertsSortingCbbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.AlertsSortingCbbx.SelectedIndex == 0)
            {

                var Xo = new List<string>();
                Xo.Add("Hello56");
                Xo.Add("Hello1");
                Xo.Add("Hel");
                Xo.Add("He");
                IEnumerable<string> Filtered =  Xo.Select(m => m.ToString()).Where(m => m.Length > 3);

                foreach (var item in Filtered)
                {
                    MessageBox.Show(item);
                }
            }
        }

        private void ContentFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            this.LoadingIndicator.Visibility = Visibility.Collapsed;

            if (this.ContentFrame.CanGoBack)
            {
                this.BackNavigation.Visibility = Visibility.Visible;
            }
            else
            {
                this.BackNavigation.Visibility = Visibility.Hidden;
            }
        }

        private void BackNavigation_Click(object sender, RoutedEventArgs e)
        {
            if (this.ContentFrame.CanGoBack)
            {
                this.ContentFrame.GoBack();
            }
        }

        private async void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            //Delete sessionID from cache

            AppLevel.CachingService.appCache.Add<Models.Session>("SessionID", null);

            MessageBox.Show(AppLevel.CachingService.appCache.Get<string>("SessionID"));

            //Call Api logout
            
            try
            {
                 await AppLevel.APIClients.v1Client
                                .DeleteRequest(route: "Auth")
                                .AddQueryParameter("uid", SessionVM.SessionID.ToString())
                                .ExecuteAsync();
            }
            catch (HttpException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return;
            }
            catch (HttpException ex) when (ex.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                return;
            }
            catch (Exception)
            {
                return;
            }

            //Close window
            this.Close();
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Navigate(Home);
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
        }
    }

}
