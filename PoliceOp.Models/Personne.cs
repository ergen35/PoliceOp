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
        public string Photographie { get; set; }

        [ForeignKey("Biometrie")]
        [Column(Order = 1)]
        public int BiometrieID { get; set; }

        [ForeignKey("Residence")]
        [Column(Order = 2)]
        public int ResidenceId { get; set; }

        [ForeignKey("PersonneId")]
        [Column(Order = 3)]
        public int PereId { get; set; }

        [ForeignKey("PersonneId")]
        [Column(Order = 4)]
        public int MereId { get; set; }

    }
}
