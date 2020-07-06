namespace PaymentService.Infrastructure.Persistance.Repositories
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using PaymentService.Domain.Entities;
    using PaymentService.Application.Common.Interfaces;

    public class PolicyAccountRepository : IPolicyAccountRepository
    {
        private readonly PaymentDbContext _context;

        public PolicyAccountRepository(PaymentDbContext context) => this._context = context;

        public void Add(PolicyAccount policyAccount) =>
            this._context.PolicyAccounts.Add(policyAccount);

        public async Task<bool> ExistsWithPolicyNumber(Guid policyNumber) =>
            await this._context.PolicyAccounts.AnyAsync(x => x.PolicyId == policyNumber);

        public async Task<PolicyAccount> FindByNumber(Guid accountNumber) =>
            await this._context.PolicyAccounts.FirstOrDefaultAsync(x => x.Id == accountNumber);

        public void Update(PolicyAccount policyAccount) =>
            this._context.PolicyAccounts.Update(policyAccount);

        public async Task SaveChangesAsync() =>
            await this._context.SaveChangesAsync();
    }
}
