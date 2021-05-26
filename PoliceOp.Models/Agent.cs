using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DataType(DataType.Password)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string PasswordHash { get; set; }
    }
}
