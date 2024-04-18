namespace BankApi.Domain.DTO
{
    public class CustomerNewDTO
    {
        public string Gender { get; set; } = "female";
        public string Givenname { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Streetaddress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Zipcode { get; set; } = null!;
        public string CountryCode { get; set; } = "SE";
        public string Country { get; set; } = "Sverige";
        public string Emailaddress { get; set; } = null!;

        public string Password { get; set; } = null;
    }
}
