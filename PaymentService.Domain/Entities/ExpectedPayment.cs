namespace PaymentService.Domain.Entities
{
    using System;

    public class ExpectedPayment : AccountingEntry
    {
        public ExpectedPayment() { }

        public ExpectedPayment(PolicyAccount policyAccount, DateTimeOffset creationDate, DateTimeOffset effectiveDate, decimal amount) :
            base(policyAccount, creationDate, effectiveDate, amount) { }

        public override decimal Apply(decimal state) => state - this.Amount;
    }
}
