using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoliceOp.Models
{
    public class PieceJointe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PieceJointeId { get; set; }

        [Required]
        public string NomFichier { get; set; }

        [Required]
        public string ExtensionFichier { get; set; }

        [Required]
        public byte[] Fichier { get; set; }
    }
}
