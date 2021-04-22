namespace Parvaryk.Contracts.Models.Request
{
    public class CreateVehicleRequest
    {
        public int OwnerUserId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string Number { get; set; }

        public string Country { get; set; }
    }
}
