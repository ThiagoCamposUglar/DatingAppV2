using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(options =>
            {
                 options.UseSqlite(config.GetConnectionString("DefaultConnection"));//Pega do appsettings.json
            });

            services.AddCors();//adicionando cors
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}