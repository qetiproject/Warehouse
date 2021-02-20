using ApplicationShared.Interfaces;
using ApplicationShared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.ApplicationService.Mappers;
using Warehouse.CoreServices.Interfaces;
using Warehouse.CoreServices.Repositories;
using Warehouse.DatabaseEntity.DB;

namespace DatingApp.Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration Configuration
        )
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
            });

            services.AddAutoMapper(typeof(ObjectsMapper).Assembly);
            //CleanArchitecture.Application
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();

            //CleanArchitecture.Domain.Interfaces | CleanArchitecture.Infra.Data.Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
