using Microsoft.EntityFrameworkCore;
using PersistencePostgres;
using PersistencePostgres.Repositories;

namespace WebApp.ServicesCollectionsExt
{
    public static class DataBaseServiceExt
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services, ConfigurationManager configuration) 
        {
            services.AddDbContext<LearningDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(LearningDbContext)));
            });
            services.AddScoped<CoursesRepository>();

            return services;
        }
    }
}
