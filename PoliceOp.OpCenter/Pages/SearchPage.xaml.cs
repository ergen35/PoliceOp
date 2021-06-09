using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                new Models.Personne(){Nom = "Azon", Prenom = "Tonangon", NPI = "656248489", UID = Guid.NewGuid().ToString(), Nationalite = "/Resources/images/Femme2.png"  },
                new Models.Personne(){Nom = "Vignon", Prenom = "René Mahougnon", NPI = "656248489", UID = Guid.NewGuid().ToString(), Nationalite = "/Resources/images/Homme1.png" },
                new Models.Personne(){Nom = "Ataki", Prenom = "Vladimir", NPI = "656248489", UID = Guid.NewGuid().ToString(), Nationalite = "/Resources/images/Homme2.png" },
                new Models.Personne(){Nom = "Bignon", Prenom = "Arlette", NPI = "656248489", UID = Guid.NewGuid().ToString(), Nationalite = "/Resources/images/Femme1.png" },
                new Models.Personne(){Nom = "Bigon", Prenom = "Mylo", NPI = "656248489", UID = Guid.NewGuid().ToString(),   Nationalite = "/Resources/images/Homme3.png" },
                new Models.Personne(){Nom = "Hossou", Prenom = "Paul", NPI = "656248489", UID = Guid.NewGuid().ToString(), Nationalite = "/Resources/images/Homme1.png" },
            };

            this.PersonnesListView.ItemsSource = PList;
        }
    }
}
