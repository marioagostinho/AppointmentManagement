using Appointment.Application.EventBusConsumers;
using EventBus.Messages.Common;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Appointment.Application
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
                config.AddConsumer<AppointmentConsumer>();
                config.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                    cfg.ReceiveEndpoint(EventBusConstants.TimeSlotQueue, c =>
                    {
                        c.ConfigureConsumer<AppointmentConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
