using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace PoliceOp.Models
{
    public class Personne
    {

        [Key]
        public int PersonneId { get; set; }

        [Required]
        public string UID { get; set; } 
        
        [Required]
        public string NPI { get; set; }

        [Required]
        public string IFU { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string Sexe { get; set; }

        [Required]
        public string LieuNaissance { get; set; }

        [Required]
        public string Nationalite { get; set; }

        [Required]
        public string Profession { get; set; }

        [Required]
        public string SituationMatrimoniale { get; set; }

        [Required]
        public string SignesParticuliers { get; set; }

        [Required]
        public string CouleurYeux { get; set; }

        [Required]
        public string CouleurCheveux { get; set; }

        [Required]
        public string Teint { get; set; }

        [Required]
        public double Taille { get; set; }

        [Required]
        public byte[] Photographie { get; set; }

        public virtual Biometrie Biometrie { get; set; }

        [Required]
        public virtual Residence Residence { get; set; }

        public virtual Personne Pere { get; set; }

        public virtual Personne Mere { get; set; }

    }
}
