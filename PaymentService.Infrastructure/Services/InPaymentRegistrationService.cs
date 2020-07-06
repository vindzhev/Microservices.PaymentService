namespace PaymentService.Infrastructure.Services
{
    using System;
    using System.Threading.Tasks;

    using PaymentService.Application.Common.Models;
    using PaymentService.Application.Common.Interfaces;

    public class InPaymentRegistrationService : IInPaymentRegistration
    {
        private readonly IPolicyAccountRepository _policyAccountRepository;

        public InPaymentRegistrationService(IPolicyAccountRepository policyAccountRepository) => this._policyAccountRepository = policyAccountRepository;

        public async Task RegisterInPayments(string directory, DateTimeOffset date)
        {
            var fileToImport = new BankStatementFile(directory, date);

            if (!fileToImport.Exists())
                return;

            foreach (var txLine in fileToImport.Read())
            {
                var account = await this._policyAccountRepository.FindByNumber(Guid.Parse(txLine.AccountNumber));
                account?.InPayment(txLine.Amount, txLine.AccountingDate);

                this._policyAccountRepository.Update(account);
            }

            fileToImport.MarkProcessed();

            await this._policyAccountRepository.SaveChangesAsync();
        }
    }
}
