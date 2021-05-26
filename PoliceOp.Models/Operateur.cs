using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoliceOp.Models
{
    public class Operateur : Agent
    {
        [Required]
        public string Service { get; set; }

    }
}
