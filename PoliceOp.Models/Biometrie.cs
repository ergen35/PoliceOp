using System;
using System.Collections.Generic;



namespace PoliceOp.Models
{
    internal class Biometrie
    {
        public Guid ID { get; set; }
        public object DonneesFaciales { get; set; }
        public Dictionary<string, object> DonneesDigitale { get; set; }

        public void Comparer ()
        {
            
        }                
    }
}
