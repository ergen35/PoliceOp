using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoliceOp.Models
{
    public class Session
    {
        [Key]
        public Guid SessionID { get; set; }

        public override string ToString()
        {
            return this.SessionID.ToString();
        }
    }
}
