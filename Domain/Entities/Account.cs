﻿namespace BankApi.Domain.Entities;

public partial class Account
{
    public int AccountId { get; set; }

    public string Frequency { get; set; } = null!;

    public DateOnly Created { get; set; }

    public decimal Balance { get; set; }

    public int? AccountTypesId { get; set; }

    public virtual AccountType? AccountTypes { get; set; }

    public virtual ICollection<Disposition>? Dispositions { get; set; } = new List<Disposition>();

    public virtual ICollection<Loan>? Loans { get; set; } = new List<Loan>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
