namespace Parvaryk.Contracts.Models.Request
{
    public class HandleTransportationRequestRequest
    {
        public int TransportationRequestId { get; set; }

        public bool Approved { get; set; }
    }
}
