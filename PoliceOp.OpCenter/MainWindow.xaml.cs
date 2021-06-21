using ControlzEx.Theming;
using Enterwell.Clients.Wpf.Notifications;
using LazyCache;
using PoliceOp.OpCenter.Services;
using RestSharp;
using System;
using System.Windows;
using System.Windows.Controls;

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

            DataContext = this;


            try
            {
                SessionVM = AppLevel.CachingService.appCache.Get<Models.SessionVM>("SessionVM");

                if (SessionVM.SessionID == (new Models.Session()).SessionID.ToString())
                {
                    Close();
                }

            }
            catch (Exception)
            {
                Close();
            }

            jWTServices = new JWTServices();


            Home = new Pages.HomePage();

            ContentFrame.Navigate(Home, null);

            AlertCenterMessageContainer.Manager = AppLevel.NotificationManagers.AlertCenterManager;
            InAppMessageContainer.Manager = AppLevel.NotificationManagers.InAppNotificationsManager;

            Closed += MainWindow_Closed;

            Agent = new Models.Agent();

            //Load Agent Info once window is loaded
            Loaded += MainWindow_Loaded;

        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            AppLevel.APIClients.AppRestClient2.Authenticator = new RestSharp.Authenticators.JwtAuthenticator(
                        jWTServices.TokenizeSessionID(SessionVM.SessionID, "get-Agent"));

            var req = new RestRequest($"Agents/{SessionVM.AgentID}", RestSharp.DataFormat.Json);

            var response = await AppLevel.APIClients.AppRestClient2.ExecuteGetAsync<Models.Agent>(request: req);

            if (response.IsSuccessful)
            {
                Agent = response.Data;
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ShowNotification("Impossible d'obtenir les informations demandées", "#F15B19", "#F15B19", "Erreur");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ShowNotification("Requête non Authorisée", "#333", "#E0A030", "Avertissement");
                }
                else
                {
                    ShowNotification("Une Erreur est survenue", "#333", "#E0A030", "Erreur");
                }
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
            ToogleDrawer.Visibility = Visibility.Hidden;
        }

        private void DrawerLeft_Closed(object sender, RoutedEventArgs e)
        {
            ToogleDrawer.Visibility = Visibility.Visible;
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
            AlertCenterMessageContainer.Items.Clear();
        }
        private void AlertsSortingCbbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ContentFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            LoadingIndicator.Visibility = Visibility.Collapsed;

            if (ContentFrame.CanGoBack)
            {
                BackNavigation.Visibility = Visibility.Visible;
            }
            else
            {
                BackNavigation.Visibility = Visibility.Hidden;
            }
        }

        private void BackNavigation_Click(object sender, RoutedEventArgs e)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            //Delete sessionID from cache

            AppLevel.CachingService.appCache.Add<Models.Session>("SessionID", null);

            //Call Api logout

            //try
            //{
            //    //await AppLevel.APIClients.v1Client
            //    //               .DeleteRequest(route: "Auth")
            //    //               .AddQueryParameter("uid", SessionVM.SessionID.ToString())
            //    //               .ExecuteAsync();
            //}
            //catch
            //{

            //}

            //Close window
            Close();
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(Home);
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

        private void WantedNoticesSM_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void SideMenu_ItemSelected(object sender, RoutedEventArgs e)
        {
            if (((sender as HandyControl.Controls.SideMenuItem).Header as string).ToLower().Contains("wanted"))
            {
                ContentFrame.Navigate(new Pages.NoticesListPage());
            }

            if (((sender as HandyControl.Controls.SideMenuItem).Header as string).ToLower().Contains("search"))
            {
                DrawerLeft.IsOpen = false;
                ContentFrame.Navigate(new Pages.SearchPage());
            }

            if (((sender as HandyControl.Controls.SideMenuItem).Header as string).ToLower().Contains("work"))
            {
                //this.ContentFrame.Navigate(new Pages.NoticesListPage());
            }

            if (((sender as HandyControl.Controls.SideMenuItem).Header as string).ToLower().Contains("agents"))
            {
                ContentFrame.Navigate(new Pages.AgentsManagementPage());
            }
            if (((sender as HandyControl.Controls.SideMenuItem).Header as string).ToLower().Contains("diffusions"))
            {
                ContentFrame.Navigate(new Pages.DiffusionsList());
            }
            if (((sender as HandyControl.Controls.SideMenuItem).Header as string).ToLower().Contains("admin"))
            {
                ContentFrame.Navigate(new Pages.AdministrationPage());
            }
            if (((sender as HandyControl.Controls.SideMenuItem).Header as string).ToLower().Contains("center"))
            {
                NotificationCenter_Click(this, null);
            }


        }
    }

}
