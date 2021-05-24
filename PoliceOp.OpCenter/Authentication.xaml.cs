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
                    appCache.Add<string>("SessionID",  SessionID , new TimeSpan(0,3,0));


                    //MessageBox.Show(appCache.Get<string>("SessionID"));

                    //Close this window and try to call The OpCenter
                    this.Visibility = Visibility.Collapsed;

                    // When OpCenter Opens, check Hash in the cache
                    MainWindow MW = new MainWindow();
                    MW.Show();

                    // When OpCenter Closes, Delete hash from cache
                }
                else
                    MessageBox.Show("Not Okay");
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
