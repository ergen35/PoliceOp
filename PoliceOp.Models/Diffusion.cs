using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoliceOp.Models
{
    public class Diffusion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiffusionId { get; set; }

        [ForeignKey("PersonneId")]
        public int AuthorId { get; set; }

        public string Sujet { get; set; }

        [Required]
        public string Details { get; set; }

        public string Cible { get; set; } = "Agents";

        public DateTime DateDiffusion { get; set; }

        public virtual ICollection<PieceJointe> PiecesJointes { get; set; }
    }
}
