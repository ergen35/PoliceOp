using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoliceOp.Models
{
    public class Requete
    {
        public int ID { get; set; }

        public Guid UID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string TermeRequete { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateRequete { get; set; } = DateTime.Now;

    }
}
