using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoliceOp.Models
{
    public class AvisRecherche
    {
        [Key]
        public Guid UID { get; set; }

        [Required]
        public DateTime DateEmission { get; set; }

        [Required]
        public string StatutRecherche { get; set; }

        public string Informations { get; set; }

        [Required]
        [ForeignKey("PersonneId")]
        public int PersonneRechercheeId { get; set; }

        [NotMapped]
        public Personne PersonneRecherchee { get; set; }

    }
}
