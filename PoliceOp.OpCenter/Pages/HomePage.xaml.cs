using System;
using System.Windows;
using System.Windows.Controls;


namespace PoliceOp.OpCenter.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();


        }

        private async void TileNews_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window item in App.Current.Windows)
            {
                if (item.Title.ToLower() == "opcenter")
                {
                    (item as MainWindow).LoadingIndicator.Visibility = Visibility.Visible;

                    await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 5));

                    (item as MainWindow).ContentFrame.Navigate(new NoticesListPage());
                    break;
                }
            }
        }

        private async void TileSearch_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window item in App.Current.Windows)
            {
                if (item.Title.ToLower() == "opcenter")
                {
                    (item as MainWindow).LoadingIndicator.Visibility = Visibility.Visible;

                    await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 9));

                    (item as MainWindow).ContentFrame.Navigate(new SearchPage());
                    break;
                }
            }
        }

        private async void TileWorkspace_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window item in App.Current.Windows)
            {
                if (item.Title.ToLower() == "opcenter")
                {
                    (item as MainWindow).LoadingIndicator.Visibility = Visibility.Visible;

                    await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 5));

                    (item as MainWindow).ContentFrame.Navigate(new SearchPage());
                    break;
                }
            }
        }

        private async void TileAgentsMgmt_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window item in App.Current.Windows)
            {
                if (item.Title.ToLower() == "opcenter")
                {
                    (item as MainWindow).LoadingIndicator.Visibility = Visibility.Visible;

                    await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 12));

                    (item as MainWindow).ContentFrame.Navigate(new AgentsManagementPage());
                    break;
                }
            }
        }

        private async void TileDiffusion_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window item in App.Current.Windows)
            {
                if (item.Title.ToLower() == "opcenter")
                {
                    (item as MainWindow).LoadingIndicator.Visibility = Visibility.Visible;

                    await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 7));

                    (item as MainWindow).ContentFrame.Navigate(new DiffusionsList());
                    break;
                }
            }
        }

        private async void TileAdministration_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window item in App.Current.Windows)
            {
                if (item.Title.ToLower() == "opcenter")
                {
                    (item as MainWindow).LoadingIndicator.Visibility = Visibility.Visible;

                    await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 8));

                    (item as MainWindow).ContentFrame.Navigate(new AdministrationPage());
                    break;
                }
            }
        }
    }
}
