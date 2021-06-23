using System.ComponentModel.DataAnnotations;

namespace PoliceOp.Models
{
    public class Operateur : Agent
    {
        [Required]
        public string Service { get; set; }

    }
}
