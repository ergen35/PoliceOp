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
    /// Interaction logic for AgentsManagementPage.xaml
    /// </summary>
    public partial class AgentsManagementPage : Page
    {
        public AgentsManagementPage()
        {
            InitializeComponent();

            List<Models.Agent> PList = new List<Models.Agent>()
            {
                new Models.Agent(){Nom = "Hello", Prenom = "Hello", NPI = "656248489", UID = Guid.NewGuid().ToString(), Matricule = "5612178787887" },
                new Models.Agent(){Nom = "Halo", Prenom = "Malo", NPI = "656248489", UID = Guid.NewGuid().ToString(), Matricule = "5682826887" },
                new Models.Agent(){Nom = "Hilo", Prenom = "Milo", NPI = "656248489", UID = Guid.NewGuid().ToString(), Matricule = "5619820887" },
                new Models.Agent(){Nom = "Hulo", Prenom = "Mulo", NPI = "656248489", UID = Guid.NewGuid().ToString(), Matricule = "80490487" },
                new Models.Agent(){Nom = "Hylo", Prenom = "Mylo", NPI = "656248489", UID = Guid.NewGuid().ToString(), Matricule = "94807887" },
                new Models.Agent(){Nom = "Mulo", Prenom = "Poulos", NPI = "656248489", UID = Guid.NewGuid().ToString(), Matricule = "309087887" },
            };

            this.PersonnesListView.ItemsSource = PList;
        }
    }
}
