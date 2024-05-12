using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Team.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // MediatR
            services.AddMediatR(config => {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            // Message Broker
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ct, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                });
            });

            return services;
        }
    }
}
