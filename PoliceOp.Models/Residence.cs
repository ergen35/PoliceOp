using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;


namespace PoliceOp.Models
{
    [ComplexType]
    public class Residence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ResidenceId { get; set; }

        [Required]
        public string Type { get; set; }

        public string Rue { get; set; }

        public string NumeroParcelle { get; set; }

        public string NumeroChambre { get; set; }

        public virtual string CoordonneesGeo { get; set; }

        public string Description { get; set; }

        [ForeignKey("PersonneId")]
        public virtual Personne Proprietaire { get; set; }

        [NotMapped]
        public string AdresseComplete {
            get { return $"{NumeroParcelle} Rue {Rue}, C{NumeroChambre}"; } 
        }

    }
}
