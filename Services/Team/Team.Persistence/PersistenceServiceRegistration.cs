using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Team.Domain.Repositories;
using Team.Persistence.DatabaseContext;
using Team.Persistence.Repositories;

namespace Team.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TeamDatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TeamConnectionString"));
            });

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IOpeningTimeSlotRepository, OpeningTimeSlotRepository>();
            services.AddScoped<IOpeningHoursRepository, OpeningHoursRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();

            return services;
        }
    }
}
