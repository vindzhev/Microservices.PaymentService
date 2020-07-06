namespace PaymentService.Application.Common.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using PaymentService.Domain.Entities;

    public interface IPolicyAccountRepository
    {
        void Add(PolicyAccount policyAccount);

        void Update(PolicyAccount policyAccount);

        Task<PolicyAccount> FindByNumber(Guid accountNumber);

        Task<bool> ExistsWithPolicyNumber(Guid policyNumber);

        Task SaveChangesAsync();
    }
}
