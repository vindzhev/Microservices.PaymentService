namespace PaymentService.Domain.Entities
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using MicroservicesPOC.Shared.Extensions;
    using MicroservicesPOC.Shared.Common.Models;
    using MicroservicesPOC.Shared.Common.Entities;

    public class PolicyAccount : Entity<Guid>
    {
        public PolicyAccount() { }

        public PolicyAccount(Guid policyNumber, string ownerFirstName, string ownerLastLastName)
        {
            this.PolicyId = policyNumber;
            this.Owner = new Owner(ownerFirstName, ownerLastLastName);
            this.Status = PolicyAccountStatus.Active;
            this.Entries = new List<AccountingEntry>();
        }

        public Guid PolicyId { get; set; }

        public Owner Owner { get; set; }

        public PolicyAccountStatus Status { get; set; }

        public bool IsActive => Status == PolicyAccountStatus.Active;

        public ICollection<AccountingEntry> Entries { get; set; } = new List<AccountingEntry>();

        public void ExpectedPayment(decimal amount, DateTimeOffset dueDate) => this.Entries.Add(new ExpectedPayment(this, DateTimeOffset.Now, dueDate, amount));

        public void InPayment(decimal amount, DateTimeOffset incomeDate) => this.Entries.Add(new InPayment(this, DateTimeOffset.Now, incomeDate, amount));

        public void OutPayment(decimal amount, DateTimeOffset paymentReleaseDate) => this.Entries.Add(new OutPayment(this, DateTimeOffset.Now, paymentReleaseDate, amount));

        public decimal BalanceAt(DateTimeOffset effectiveDate)
        {
            ICollection<AccountingEntry> effectiveEntries = Entries
                .Where(x => x.IsEffectiveOn(effectiveDate))
                .OrderBy(x => x.CreationDate)
                .ToList();

            decimal balance = 0M;
            effectiveEntries.ForEach(x => balance = x.Apply(balance));

            return balance;
        }

        public void Close(DateTime closingDate, decimal? amountToReturn)
        {
            if (!this.IsActive)
                return;

            if (amountToReturn.HasValue)
                this.OutPayment(amountToReturn.Value, closingDate);

            this.Status = PolicyAccountStatus.Terminated;
        }
    }
}
