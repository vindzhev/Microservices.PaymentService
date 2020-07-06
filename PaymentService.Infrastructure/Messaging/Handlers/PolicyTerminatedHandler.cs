namespace PaymentService.Infrastructure.Messaging.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    
    using MicroservicesPOC.Shared.Messaging.Events;
    
    using PaymentService.Application.Common.Interfaces;
    

    public class PolicyTerminatedHandler : INotificationHandler<PolicyTerminatedEvent>
    {
        private readonly IPolicyAccountRepository _policyAccountRepository;

        public PolicyTerminatedHandler(IPolicyAccountRepository policyAccountRepository) =>
            this._policyAccountRepository = policyAccountRepository;

        public async Task Handle(PolicyTerminatedEvent notification, CancellationToken cancellationToken)
        {
            var policy = await this._policyAccountRepository.FindByNumber(notification.PolicyNumber);

            if (policy == null) //TODO: proper business exception + message
                throw new ArgumentNullException();

            policy.Close(notification.PolicyTo, notification.AmountToReturn);

            this._policyAccountRepository.Update(policy);

            await this._policyAccountRepository.SaveChangesAsync();
        }
    }
}
