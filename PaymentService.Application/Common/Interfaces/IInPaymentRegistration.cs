namespace PaymentService.Application.Common.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using MicroservicesPOC.Shared.Common.Services;

    public interface IInPaymentRegistration : IService
    {
        Task RegisterInPayments(string directory, DateTimeOffset date);
    }
}
