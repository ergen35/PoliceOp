using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace PoliceOp.Models
{
    public class Personne
    {
        public int ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UID { get; set; } 
        
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

        public ICollection<string> Telephone { get; set; }
        
        public Personne Pere { get; set; }

        public Personne Mere { get; set; }

        [Required]
        [EnumDataType(typeof(Sexe))]
        public Sexe Sexe { get; set; }

        [Required]
        public string LieuNaissance { get; set; }

        [Required]
        public string Nationalité { get; set; }

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
        public Byte[] Photographie { get; set; }

    }
}
