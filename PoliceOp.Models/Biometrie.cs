using System;
using System.ComponentModel.DataAnnotations;


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
