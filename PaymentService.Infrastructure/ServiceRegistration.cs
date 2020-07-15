namespace PaymentService.Infrastructure
{
    using MediatR;

    using Hangfire;
    using Hangfire.PostgreSql;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using MicroservicesPOC.Shared.Common;
    using MicroservicesPOC.Shared.Messaging;
    using MicroservicesPOC.Shared.Messaging.Events;

    using PaymentService.Application.Common.Models;
    using PaymentService.Application.Common.Interfaces;
    
    using PaymentService.Infrastructure.Jobs;
    
    using PaymentService.Infrastructure.Persistance;
    using PaymentService.Infrastructure.Persistance.Repositories;

    using PaymentService.Infrastructure.Messaging.Handlers;

    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQConfigurations>(configuration.GetSection("RabbitMQ"));
            services.Configure<BackgroundJobsConfig>(configuration.GetSection("BackgroundJobs"));
            
            services
                .AddDbContext<PaymentDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("PaymentServiceConnection"),
                    x => x.MigrationsAssembly(typeof(PaymentDbContext).Assembly.FullName)))
                .AddScoped<IPolicyAccountRepository, PolicyAccountRepository>()
                .AddTransient<INotificationHandler<PolicyCreatedEvent>, PolicyCreatedHandler>()
                .AddTransient<INotificationHandler<PolicyTerminatedEvent>, PolicyTerminatedHandler>();

            services.AddHostedService<RabbitMqListenerWorker<PolicyCreatedEvent>>();
            services.AddHostedService<RabbitMqListenerWorker<PolicyTerminatedEvent>>();

            services.AddHangfire(config => 
                config.UsePostgreSqlStorage(configuration.GetSection("BackgroundJobs").Get<BackgroundJobsConfig>().HangfireConnectionStringName));

            services.AddConventionalServices(typeof(ServiceRegistration).Assembly);

            return services;
        }

        public static void UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseHangfireServer();
            app.UseHangfireDashboard();

            //Execute every minute
            RecurringJob.AddOrUpdate<InPaymentRegistrationJob>(x => x.Run(), "0 * * 1 * *");
        }

        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<PaymentDbContext>();

            context.Database.Migrate();
        }
    }
}
