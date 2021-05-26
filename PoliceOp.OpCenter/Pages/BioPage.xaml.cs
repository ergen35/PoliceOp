using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace PoliceOp.OpCenter.Pages
{
    /// <summary>
    /// Interaction logic for BioPage.xaml
    /// </summary>
    public partial class BioPage : Page
    {
        public Models.Personne Personne { get; set; }
        public BioPage()
        {
            InitializeComponent();

            this.DataContext = this;

            Personne = new Models.Personne()
            {
                Nom = "Eric",
                Prenom = "Avery",
                DateNaissance = new DateTime(1998, 05, 18),
                PersonneId = 5,
                IFU = "898888884658548",
                Mere = new Models.Personne() { Nom = "Bark", Prenom = "Joyce" },
                CouleurCheveux = "Bisque",
                CouleurYeux = "PaleGreen",
                NPI = "0000400848080",
                Pere = new Models.Personne() { Nom = "Eric", Prenom = "Thanos" },
                LieuNaissance = "Transylvania",
                Nationalite = "Béninoise",
                Profession = "Ing. SIL",
                Sexe = Models.Sexe.M,
                SignesParticuliers = "Trop Beau.. wow",
                Taille = 1.98,
                SituationMatrimoniale = "Célibataire endurci",
                Teint = "Noir",
                Telephone = "90210790",
                UID = Guid.NewGuid()
            };

        }

        private void BtnPere_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnMere_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FaceDataBtn_Click(object sender, RoutedEventArgs e)
        {
            this.FaceDataExp.IsExpanded = !this.FaceDataExp.IsExpanded;
        }

        private void FingerPrintBtn_Click(object sender, RoutedEventArgs e)
        {
            this.FingerDataExp.IsExpanded = !this.FingerDataExp.IsExpanded;
        }
    }
}
