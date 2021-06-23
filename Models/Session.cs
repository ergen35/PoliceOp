using System;
using System.ComponentModel.DataAnnotations;

namespace PoliceOp.Models
{
    public class Session
    {
        [Key]
        public Guid SessionID { get; set; }

        public override string ToString()
        {
            return SessionID.ToString();
        }
    }
}
