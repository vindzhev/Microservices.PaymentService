namespace PaymentService.Application.Common.Models
{
    public class BackgroundJobsConfig
    {
        public string InPaymentFileFolder { get; set; }

        public string HangfireConnectionStringName { get; set; }
    }
}
