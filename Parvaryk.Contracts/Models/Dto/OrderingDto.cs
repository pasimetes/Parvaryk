using System;

namespace Parvaryk.Contracts.Models.Dto
{
    public class OrderingDto
    {
        public int OrderingId { get; set; }

        public int OwnerUserId { get; set; }

        public int VehicleId { get; set; }

        public int OrderingStatusId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? StatusUpdatedDate { get; set; }

        public int StartDirectionId { get; set; }

        public DirectionDto StartDirection { get; set; }
        
        public int EndDirectionId { get; set; }

        public DirectionDto EndDirection { get; set; }
    }
}
