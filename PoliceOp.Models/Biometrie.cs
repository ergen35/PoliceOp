using System;
using System.Collections.Generic;



namespace PoliceOp.Models
{
    public class Biometrie
    {
        public int ID { get; set; }
        public Guid UID { get; set; }
        public object DonneesFaciales { get; set; }
        public Dictionary<string, object> DonneesDigitale { get; set; }

        public void Comparer ()
        {
            
        }                
    }
}
