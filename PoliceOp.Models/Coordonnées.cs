
namespace PoliceOp.Models
{
    public class Coordonnées
    {
        public int ID { get; set; }
        public string Rue { get; set; }
        public string NuméroParcelle { get; set; }
        public string NuméroChambre { get; set; }
        public object CoordonnéesGéo { get; set; }

    }
}
