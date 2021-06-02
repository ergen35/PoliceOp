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

namespace PoliceOp.OpCenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MahApps.Metro.Controls.MetroWindow
    {
        public string SessionID { get; set; }
        public Pages.HomePage Home { get; set; }
        public Models.Agent Agent { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;


            try
            {
                SessionID = AppLevel.CachingService.appCache.Get<string>("SessionID");
               
                if (SessionID == null)
                {
                    this.Close();
                }

            }
            catch (Exception)
            {
                this.Close();
            }


            Agent = new Models.Agent() { Matricule = "4555555881", Nom = "Mad", Prenom = "Crocodile" };

            this.Home = new Pages.HomePage();

            this.ContentFrame.Navigate(Home, null);

            this.AlertCenterMessageContainer.Manager = AppLevel.NotificationManagers.AlertCenterManager;
            this.InAppMessageContainer.Manager = AppLevel.NotificationManagers.InAppNotificationsManager;

            this.Closed += MainWindow_Closed;

            //Load Agent Info once window is loaded
            this.Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
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

        private void NotifBtn_Click(object sender, RoutedEventArgs e)
        {
            AppLevel.NotificationManagers.InAppNotificationsManager.CreateMessage()
                                                           .Accent("#1751C3")
                                                           .Background("#333")
                                                           .HasBadge("Info")
                                                           .HasMessage("Update will be installed on next application restart.")
                                                           .Dismiss().WithButton("Update now", button => { })
                                                           .Dismiss().WithButton("Release notes", button => { })
                                                           .Dismiss().WithButton("Later", button => { })
                                                           .Queue();

            AppLevel.NotificationManagers.AlertCenterManager.CreateMessage()
                                                            .Accent("#1751C3")
                                                            .Background("#333")
                                                            .HasBadge("Info")
                                                            .HasMessage("Update will be installed on next application restart.")
                                                            .Dismiss().WithButton("Update now", button => { })
                                                            .Dismiss().WithButton("Release notes", button => { })
                                                            .Dismiss().WithButton("Later", button => { })
                                                            .Queue();
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

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            //Delete sessionID from cache

            AppLevel.CachingService.appCache.Add<string>("SessionID", string.Empty);
            MessageBox.Show(AppLevel.CachingService.appCache.Get<string>("SessionID"));
            //Call Api logout

            //Close window
            this.Close();
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Navigate(Home);
        }
    }
}
