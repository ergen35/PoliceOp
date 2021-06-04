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
                new Models.Personne(){Nom = "Hello", Prenom = "Hello", NPI = "656248489", UID = Guid.NewGuid().ToString() },
                new Models.Personne(){Nom = "Halo", Prenom = "Malo", NPI = "656248489", UID = Guid.NewGuid().ToString() },
                new Models.Personne(){Nom = "Hilo", Prenom = "Milo", NPI = "656248489", UID = Guid.NewGuid().ToString() },
                new Models.Personne(){Nom = "Hulo", Prenom = "Mulo", NPI = "656248489", UID = Guid.NewGuid().ToString() },
                new Models.Personne(){Nom = "Hylo", Prenom = "Mylo", NPI = "656248489", UID = Guid.NewGuid().ToString() },
                new Models.Personne(){Nom = "Mulo", Prenom = "Poulos", NPI = "656248489", UID = Guid.NewGuid().ToString() },
            };

            this.PersonnesListView.ItemsSource = PList;
        }
    }
}
