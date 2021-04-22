using Parvaryk.Contracts.Models.Dto;

namespace Parvaryk.Contracts.Models.Request
{
    public class CreateOrderingRequest
    {
        public int OwnerUserId { get; set; }

        public int VehicleId { get; set; }

        public DirectionDto StartDirection { get; set; }

        public DirectionDto EndDirection { get; set; }
    }
}
