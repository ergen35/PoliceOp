using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PoliceOp.OpCenter.Pages
{
    /// <summary>
    /// Interaction logic for NoticesListPage.xaml
    /// </summary>
    public partial class NoticesListPage : Page
    {
        public List<Models.AvisRecherche> ListWanted { get; set; }

        public NoticesListPage()
        {
            InitializeComponent();

            this.ListWanted = new List<Models.AvisRecherche>() {
                new Models.AvisRecherche()
                {
                     PersonneRecherchee = new Models.Personne(){ Nom = "Eric", Prenom = "Jira", NPI = "641814828"},
                      DateEmission = DateTime.Now,
                       Informations = "kleznzelianezaenf",
                        StatutRecherche = "Actif",
                         UID = Guid.NewGuid()
                }
            };

            this.DataContext = this;
        }
    }
}
