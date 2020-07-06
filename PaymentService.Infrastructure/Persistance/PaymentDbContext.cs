namespace PaymentService.Infrastructure.Persistance
{
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using PaymentService.Domain.Entities;

    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions options) : base(options) { }

        public DbSet<PolicyAccount> PolicyAccounts { get; set; }

        public Task<int> SaveChanges(CancellationToken cancellationToken = new CancellationToken()) => this.SaveChangesAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ExpectedPayment>();
            builder.Entity<InPayment>();
            builder.Entity<OutPayment>();
            builder.Entity<Owner>();

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
