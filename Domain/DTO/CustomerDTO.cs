namespace BankApi.Domain.DTO
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string Givenname { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Streetaddress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Zipcode { get; set; } = null!;
        public string CountryCode { get; set; } = null!;

        public string Emailaddress { get; set; } = null!;

        public override string ToString()
        {
            return $"{CustomerId}, {Givenname} {Surname}, {Streetaddress}, {Zipcode} {City} {CountryCode}";
        }
    }
}
