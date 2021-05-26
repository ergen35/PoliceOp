using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PoliceOp.Models
{
    public class Coordonnees
    {
        public int ID { get; set; }
        public string Rue { get; set; }
        public string NumeroParcelle { get; set; }
        public string NumeroChambre { get; set; }
        public string CoordonneesGeo { get; set; }

    }
}
