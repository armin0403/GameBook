using GameBook.Infrastructure;
using GameBook.Infrastructure.UnitOfWork;
using GameBook.Services.Services;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
         
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMapper, Mapper>();
            services.AddScoped<IPaginationService, PaginationService>();
            services.AddScoped<IGameService, GameService>();

            services.AddDbContext<GameBookDbContext>((sp, opts) =>
            {
                opts.UseNpgsql(sp.GetRequiredService<IConfiguration>().GetConnectionString("GameBookConnection"));
            });

            return services;
        }
    }
}
