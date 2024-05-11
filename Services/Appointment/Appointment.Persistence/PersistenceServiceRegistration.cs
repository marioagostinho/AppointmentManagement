using Appointment.Domain.Repositories;
using Appointment.Persistence.DatabaseContext;
using Appointment.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppointmentDatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AppointmentConnectionString"));
            });

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            return services;
        }
    }
}
