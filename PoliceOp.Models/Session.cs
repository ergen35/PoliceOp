using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoliceOp.Models
{
    public class Session
    {
        public int ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SessionID { get; set; } = new Guid();

        public override string ToString()
        {
            return this.SessionID.ToString();
        }
    }
}
