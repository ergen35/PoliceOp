using System;
using System.ComponentModel.DataAnnotations;

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

        public virtual Personne PersonneRecherchee { get; set; }
    }
}
