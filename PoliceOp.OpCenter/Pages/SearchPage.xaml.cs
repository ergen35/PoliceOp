using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PoliceOp.OpCenter.Pages
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();

            List<Models.Personne> PList = new List<Models.Personne>()
            {
                new Models.Personne(){PersonneId = 160,Nom = "Azon", Prenom = "Tonangon", NPI = "656248489", UID = Guid.NewGuid().ToString(), Nationalite = "/Resources/images/Femme2.png"  },
                new Models.Personne(){PersonneId = 26, Nom = "Vignon", Prenom = "René Mahougnon", NPI = "656248489", UID = Guid.NewGuid().ToString(), Nationalite = "/Resources/images/Homme1.png" },
                new Models.Personne(){PersonneId = 13, Nom = "Ataki", Prenom = "Vladimir", NPI = "656248489", UID = Guid.NewGuid().ToString(), Nationalite = "/Resources/images/Homme2.png" },
                new Models.Personne(){PersonneId = 90,Nom = "Bignon", Prenom = "Arlette", NPI = "656248489", UID = Guid.NewGuid().ToString(), Nationalite = "/Resources/images/Femme1.png" },
                new Models.Personne(){PersonneId = 37, Nom = "Bigon", Prenom = "Mylo", NPI = "656248489", UID = Guid.NewGuid().ToString(),   Nationalite = "/Resources/images/Homme3.png" },
                new Models.Personne(){PersonneId = 89, Nom = "Hossou", Prenom = "Paul", NPI = "656248489", UID = Guid.NewGuid().ToString(), Nationalite = "/Resources/images/Homme1.png" },
            };

            this.PersonnesListView.ItemsSource = PList;
        }

        private async void DetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            int PersonneId = Convert.ToInt32((sender as Button).Tag);

            foreach (Window item in App.Current.Windows)
            {
                if (item.Title.ToLower() == "opcenter")
                {
                    (item as MainWindow).LoadingIndicator.Visibility = Visibility.Visible;

                    await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 9));

                    (item as MainWindow).ContentFrame.Navigate(new BioPage(PersonneId));
                    break;
                }
            }

        }
    }
}
