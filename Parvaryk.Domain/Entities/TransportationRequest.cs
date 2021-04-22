using System;

namespace Parvaryk.Domain.Entities
{
    public class TransportationRequest
    {
        public int TransportationRequestId { get; set; }

        public int OrderingId { get; set; }

        public int SenderUserId { get; set; }

        public int TransportationRequestStatusId { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime? ResponseDate { get; set; }

        public virtual Ordering Ordering { get; set; }

        public virtual User SenderUser { get; set; }
    }
}
