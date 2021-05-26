using System;

namespace PoliceOp.Models
{
    public class AvisRecherche
    {
        public int ID { get; set; }
        public Guid UID { get; set; }
        public DateTime DateEmission { get; set; }
        public string StatutRecherche { get; set; }
    }
}
