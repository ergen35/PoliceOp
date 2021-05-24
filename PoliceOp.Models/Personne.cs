using System;
using System.Collections.Generic;


namespace PoliceOp.Models
{
    public class Personne
    {
        public int ID { get; set; }
        public Guid UID { get; set; }              
        public string NPI { get; set; }
        public string IFU { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public ICollection<string> Telephone { get; set; }
        public Personne Pere { get; set; }
        public Personne Mere { get; set; }
        public string Sexe { get; set; }
        public string LieuNaissance { get; set; }
        public string Nationalité { get; set; }
        public string Profession { get; set; }
        public string SituationMatrimoniale { get; set; }
        public string SignesParticuliers { get; set; }
        public string CouleurYeux { get; set; }
        public string CouleurCheveux { get; set; }
        public string Teint { get; set; }
        public double Taille { get; set; }
        public Byte[] Photographie { get; set; }

    }
}
