using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PoliceOp.Models
{
    public class Biometrie
    {
        [Key]
        public Guid UID { get; set; }

        public virtual byte[] DonneesFaciales { get; set; }

        public virtual byte[] DonneesDigitales { get; set; }

        public bool Comparer()
        {
            return true;
        }
              
    }
}
