namespace PaymentService.Infrastructure.Messaging.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    
    using MicroservicesPOC.Shared.Messaging.Events;
    
    using PaymentService.Domain.Entities;
    using PaymentService.Application.Common.Interfaces;
    

    public class PolicyCreatedHandler : INotificationHandler<PolicyCreatedEvent>
    {
        private readonly IPolicyAccountRepository _policyAccountRepository;

        public PolicyCreatedHandler(IPolicyAccountRepository policyAccountRepository) =>
            this._policyAccountRepository = policyAccountRepository;

        public async Task Handle(PolicyCreatedEvent notification, CancellationToken cancellationToken)
        {
            var policy = new PolicyAccount(notification.PolicyNumber, notification.PolicyHolder.FirstName, notification.PolicyHolder.LastName);

            if (await this._policyAccountRepository.ExistsWithPolicyNumber(notification.PolicyNumber))
                return;

            this._policyAccountRepository.Add(policy);
            await this._policyAccountRepository.SaveChangesAsync();
        }
    }
}
