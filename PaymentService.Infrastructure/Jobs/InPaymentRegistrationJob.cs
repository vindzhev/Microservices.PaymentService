namespace PaymentService.Infrastructure.Jobs
{
    using System;
    using System.Threading.Tasks;
    
    using PaymentService.Application.Common.Models;
    using PaymentService.Application.Common.Interfaces;

    public class InPaymentRegistrationJob
    {
        private readonly BackgroundJobsConfig _jobConfig;
        private readonly IInPaymentRegistration _inPaymentRegistration;

        public InPaymentRegistrationJob(IInPaymentRegistration inPaymentRegistration, BackgroundJobsConfig jobConfig)
        {
            this._jobConfig = jobConfig;
            this._inPaymentRegistration = inPaymentRegistration;
        }

        public async Task Run()
        {
            //ToDO: better logging
            Console.WriteLine($"InPayment import started. Looking for file in {this._jobConfig.InPaymentFileFolder}");

            await this._inPaymentRegistration.RegisterInPayments(this._jobConfig.InPaymentFileFolder, DateTimeOffset.Now);

            Console.WriteLine("InPayment import finished.");
        }
    }
}
