using System;
using System.Collections.Generic;

namespace Parvaryk.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public byte[] Password { get; set; }

        public byte[] Salt { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual UserContactInformation UserContactInformation { get; set; }

        public virtual IList<Vehicle> Vehicles { get; set; }

        public virtual IList<Ordering> Orderings { get; set; }

        public virtual IList<Transportation> Transportations { get; set; }
    }
}
