namespace PaymentService.Application.Account.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    
    using PaymentService.Domain.Entities;
    using PaymentService.Application.Common.Interfaces;

    public class GetAccountBalanceQuery : IRequest<GetAccountBalanceQueryResult>
    {
        public GetAccountBalanceQuery(Guid policyNumber) => this.PolicyNumber = policyNumber;

        public Guid PolicyNumber { get; set; }

        public class GetAccountBalanceQueryHandler : IRequestHandler<GetAccountBalanceQuery, GetAccountBalanceQueryResult>
        {
            private readonly IPolicyAccountRepository _policyAccountRepository;

            public GetAccountBalanceQueryHandler(IPolicyAccountRepository policyAccountRepository) => this._policyAccountRepository = policyAccountRepository;

            public async Task<GetAccountBalanceQueryResult> Handle(GetAccountBalanceQuery request, CancellationToken cancellationToken)
            {
                PolicyAccount policyAccount = await this._policyAccountRepository.FindByNumber(request.PolicyNumber);

                //TODO: better exceptions
                if (policyAccount == null)
                    throw new NullReferenceException(nameof(policyAccount));

                //TODO: Move mapping to Automapper
                return new GetAccountBalanceQueryResult()
                {
                    Balance = new PolicyAccountBalanceDto()
                    {
                        PolicyAccountNumber = policyAccount.Id,
                        PolicyNumber = policyAccount.PolicyId,
                        Balance = policyAccount.BalanceAt(DateTimeOffset.Now)
                    }
                };
            }
        }
    }

    public class GetAccountBalanceQueryResult
    {
        public PolicyAccountBalanceDto Balance { get; set; }
    }

    public class PolicyAccountBalanceDto
    {
        public Guid PolicyNumber { get; set; }

        public Guid PolicyAccountNumber { get; set; }

        public decimal Balance { get; set; }
    }
}
