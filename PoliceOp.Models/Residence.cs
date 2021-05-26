using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PoliceOp.Models
{
    [ComplexType]
    public class Residence
    {        
        [Required]
        public string Type { get; set; }
        public string Description { get; set; }
        public string Rue { get; set; }
        public string NumeroParcelle { get; set; }
        public string NumeroChambre { get; set; }
        public GeoCoordinate CoordonneesGeo { get; set; }

    }
}
