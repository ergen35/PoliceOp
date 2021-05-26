using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoliceOp.Models
{
    public class Requete
    {
        [Key]
        public Guid UID { get; set; }

        [Required]
        public string TermeRequete { get; set; }

        public DateTime DateRequete { get; set; } = DateTime.Now;

    }
}
