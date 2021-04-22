using System.Collections;
using System.Collections.Generic;

namespace Parvaryk.Domain.Entities
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        public int OwnerUserId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string Number { get; set; }

        public string Country { get; set; }

        public virtual User OwnerUser { get; set; }

        public virtual IList<Ordering> Orderings { get; set; }
    }
}
