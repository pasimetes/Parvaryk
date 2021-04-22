namespace Parvaryk.Domain.Entities
{
    public class UserContactInformation
    {
        public int UserContactInformationId { get; set; }

        public int UserId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string HomeAddress { get; set; }

        public string HomeZipCode { get; set; }

        public string WorkAddress { get; set; }

        public string WorkZipCode { get; set; }

        public string Country { get; set; }

        public virtual User User { get; set; }
    }
}
