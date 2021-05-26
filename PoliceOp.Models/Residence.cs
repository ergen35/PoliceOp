using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PoliceOp.Models
{
    public class Residence
    {
        public int ID { get; set; }   
        
        [Required]
        public string Type { get; set; }

    }
}
