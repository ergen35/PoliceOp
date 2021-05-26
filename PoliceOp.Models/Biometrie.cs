using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;



namespace PoliceOp.Models
{
    public class Biometrie
    {
        public int ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UID { get; set; }
        public string DonneesFaciales { get; set; }
        public string DonneesDigitales { get; set; }

        public void Comparer ()
        {
            
        }                
    }
}
