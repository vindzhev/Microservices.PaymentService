namespace PaymentService.Application.Common.Exceptions
{
    using System;

    using MicroservicesPOC.Shared.Common.Exceptions;

    public class BankStatementsFileReadingError : BussinesException
    {
        public BankStatementsFileReadingError(Exception ex) : 
            base($"Policy Account not found. BankStatementsFileReadingError", ex) { }
    }
}
