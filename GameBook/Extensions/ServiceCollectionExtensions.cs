using FluentValidation;
using GameBook.Helpers.DropdownHelper;
using GameBook.Helpers.ToastHelper;
using GameBook.Infrastructure;
using GameBook.Infrastructure.UnitOfWork;
using GameBook.Services.Services;
using GameBook.Validator.User;
using GameBook.Web.ViewModels;
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
            services.AddScoped<IMapper, MapsterMapper.Mapper>();
            services.AddScoped<IPaginationService, PaginationService>();
            services.AddScoped<IDropdownService, DropdownService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IToastService, ToastService>();

            services.AddScoped<IValidator<RegisterViewModel>, RegisterViewModelValidator>();
            services.AddScoped<IValidator<LogInViewModel>, LoginViewModelValidator>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGameService, GameService>();

            services.AddDbContext<GameBookDbContext>((sp, opts) =>
            {
                opts.UseNpgsql(sp.GetRequiredService<IConfiguration>().GetConnectionString("GameBookConnection"));
            });

            return services;
        }
    }
}
