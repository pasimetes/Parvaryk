using System;

namespace Parvaryk.Contracts.Models.Dto
{
    public class TransporationRequestDto
    {
        public int TransportationRequestId { get; set; }

        public int OrderingId { get; set; }

        public int SenderUserId { get; set; }

        public int TransportationRequestStatusId { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime? ResponseDate { get; set; }
    }
}
