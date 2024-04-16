using BankApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BankApi.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        public virtual int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }


    }
}
