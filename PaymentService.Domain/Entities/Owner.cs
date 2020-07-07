namespace PaymentService.Domain.Entities
{
    using System;
    
    using MicroservicesPOC.Shared.Domain;

    public class Owner : Entity<Guid>
    {
        public Owner() { }

        public Owner(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; protected set; }

        public string LastName { get; protected set; }
    }
}
