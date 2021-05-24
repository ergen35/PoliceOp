using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;
using Microsoft.Extensions.Configuration;
using LazyCache;
using Effortless.Net.Encryption;
using System.Windows.Threading;
using Enterwell.Clients.Wpf.Notifications;




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


            //Initialize Image
            //BitmapImage image = new BitmapImage();
            //image.BeginInit();
            //image.UriSource = new Uri("/Resources/BadgeAnimation.gif");
            //image.EndInit();
            //ImageBehavior.SetAnimatedSource(this.BackGif, image);

            this.AuthMessages.Manager = AppLevel.NotificationManagers.AuthNotificationsManager;

            this.KeyDown += Authentication_KeyDown;
        }

        private void Authentication_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Enter || e.Key == Key.Space)
            {
                MessageBox.Show("KeyEvent fired");
            }
        }

        private async void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.LoginTxtb.Text != "" && this.PwdTxtb.Password != "")
            {
                if (this.LoginTxtb.VerifyData())
                {
                    //Send Request to API

                    //Return Result

                    // Get Key from AppConfig
                    string key = System.Configuration.ConfigurationManager.AppSettings.Get("HashKey");
                    //Generate Hash

                    string SessionID = await Task.Run<string>(() => { return Guid.NewGuid().ToString(); });

                    //Caching
                    IAppCache appCache = new CachingService();
                    appCache.Add<string>("SessionID", SessionID, new TimeSpan(0, 3, 0));


                    //MessageBox.Show(appCache.Get<string>("SessionID"));

                    //Close this window and try to call The OpCenter
                    this.Visibility = Visibility.Collapsed;
                    //Clear Password
                    this.PwdTxtb.Clear();

                    // When OpCenter Opens, check Hash in the cache
                    MainWindow MW = new MainWindow();
                    MW.Show();

                    // When OpCenter Closes, Delete hash from cache
                }
                else
                    MessageBox.Show("Not Okay");

            }

            ShowNotification("Connection Lost.");

        }

        private void FingerPrintIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            this.FingerPrintIcon.Effect = new HandyControl.Media.Effects.BrightnessEffect();
            
        }

        private void FingerPrintIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            this.FingerPrintIcon.Effect = new System.Windows.Media.Effects.BlurEffect();
        }

        private void ShowNotification(string Message)
        {
            AppLevel.NotificationManagers.AuthNotificationsManager.CreateMessage()
                                        .Accent("#1751C3")
                                        .Animates(true)
                                        .AnimationInDuration(0.75)
                                        .AnimationOutDuration(2)
                                        .Background("#333")
                                        .HasBadge("Info")
                                        .HasMessage("Update will be installed on next application restart. This message will be dismissed after 5 seconds.")
                                        .Dismiss().WithButton("Update now", button => { })
                                        .Dismiss().WithButton("Release notes", button => { })
                                        .Dismiss().WithDelay(TimeSpan.FromSeconds(5))
                                        .Queue();
        }
    }
}
