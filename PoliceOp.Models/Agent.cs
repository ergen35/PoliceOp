using System.ComponentModel.DataAnnotations;

namespace PoliceOp.Models
{
    public class Agent : Personne
    {
        [Required]
        public string Matricule { get; set; }    
        
        [Required]
        public string Grade { get; set; }

        [Required]
        public string Corps { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
