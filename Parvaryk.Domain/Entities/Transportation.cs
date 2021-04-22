using System;

namespace Parvaryk.Domain.Entities
{
    public class Transportation
    {
        public int TransportationId { get; set; }

        public int OrderingId { get; set; }

        public int CarrierUserId { get; set; }

        public int TransportationStatusId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? StatusUpdatedDate { get; set; }

        public virtual Ordering Ordering { get; set; }

        public virtual User CarrierUser { get; set; }
    }
}
