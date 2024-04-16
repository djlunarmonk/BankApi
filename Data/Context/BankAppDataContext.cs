using BankApi.Domain.Entities;
using BankApi.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Data.Context;

public partial class BankAppDataContext : IdentityDbContext<AppUser>
{
    public BankAppDataContext()
    {
    }

    public BankAppDataContext(DbContextOptions<BankAppDataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Disposition> Dispositions { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }


}
