using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PoliceOp.Models
{
    public class Agent : Personne
    {

        /// <summary>
        /// Propriétés relatives à l'agent
        /// </summary>

        [Required]
        public string Matricule { get; set; }

        [Required]
        public string Grade { get; set; }

        [Required]
        public string Corps { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public virtual ICollection<Diffusion> ListeDiffusions { get; set; }
    }
}
