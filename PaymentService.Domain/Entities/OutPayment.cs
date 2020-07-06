namespace PaymentService.Domain.Entities
{
    using System;

    public class OutPayment : AccountingEntry
    {
        public OutPayment() { }

        public OutPayment(PolicyAccount policyAccount, DateTimeOffset creationDate, DateTimeOffset effectiveDate, decimal amount) :
            base(policyAccount, creationDate, effectiveDate, amount) { }

        public override decimal Apply(decimal state) => state - this.Amount;
    }
}
