namespace PaymentService.Application.Common.Exceptions
{
    using System;
    
    using MicroservicesPOC.Shared.Common.Exceptions;

    public class BankStatementsFileNotFound : BussinesException
    {
        public BankStatementsFileNotFound(Exception ex) :
            base("Bank statements file not found.", ex) { }
    }
}
