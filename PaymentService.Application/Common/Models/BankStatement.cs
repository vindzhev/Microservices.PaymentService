namespace PaymentService.Application.Common.Models
{
    using System;

    public class BankStatement
    {
        public BankStatement(string accountNumber, string amountAsString, string accountingDateAsIsoDateString)
        {
            this.AccountNumber = accountNumber;
            this.Amount = decimal.Parse(amountAsString);
            this.AccountingDate = DateTimeOffset.Parse(accountingDateAsIsoDateString);
        }

        public string AccountNumber { get; private set; }

        public decimal Amount { get; private set; }

        public DateTimeOffset AccountingDate { get; private set; }
    }
}
