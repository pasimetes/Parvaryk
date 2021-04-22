using System;
using System.Collections.Generic;

namespace Parvaryk.Domain.Entities
{
    public class Ordering
    {
        public int OrderingId { get; set; }

        public int OwnerUserId { get; set; }

        public int VehicleId { get; set; }

        public int OrderingStatusId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? StatusUpdatedDate { get; set; }

        public int StartDirectionId { get; set; }

        public int EndDirectionId { get; set; }

        public virtual User OwnerUser { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual Direction StartDirection { get; set; }

        public virtual Direction EndDirection { get; set; }

        public virtual ICollection<Transportation> Transportations { get; set; }

        public virtual ICollection<TransportationRequest> TransportationRequests { get; set; }
    }
}
