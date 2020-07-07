namespace PaymentService.Domain.Entities
{
    using System;

    using MicroservicesPOC.Shared.Domain;

    public abstract class AccountingEntry : Entity<Guid>
    {
        public AccountingEntry() { }

        public AccountingEntry(PolicyAccount policyAccount, DateTimeOffset creationDate, DateTimeOffset effectiveDate, decimal amount)
        {
            this.PolicyAccount = policyAccount;
            this.CreationDate = creationDate;
            this.EffectiveDate = effectiveDate;
            this.Amount = amount;
        }

        public PolicyAccount PolicyAccount { get; protected set; }

        public DateTimeOffset CreationDate { get; protected set; }

        public DateTimeOffset EffectiveDate { get; protected set; }

        public decimal Amount { get; protected set; }

        public abstract decimal Apply(decimal state);

        public bool IsEffectiveOn(DateTimeOffset theDate) => this.EffectiveDate <= theDate;
    }
}
