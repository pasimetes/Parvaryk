namespace Parvaryk.Contracts.Models.Request
{
    public class SignUpRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string HomeAdress { get; set; }

        public string HomeZipCode { get; set; }

        public string WorkAddress { get; set; }

        public string WorkZipCode { get; set; }

        public string Country { get; set; }
    }
}
