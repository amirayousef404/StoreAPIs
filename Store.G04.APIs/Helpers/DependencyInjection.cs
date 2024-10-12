using Microsoft.AspNetCore.Mvc;
using Store.G04.Core.Mapping.Products;
using Store.G04.Core.Services.Contract;
using Store.G04.Core;
using Store.G04.Repository.Data.Contexts;
using Store.G04.Repository;
using Store.G04.Service.Services.Products;
using Store.G04.APIs.Errors;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Store.G04.Core.Repositories.Contract;
using Store.G04.Repository.Repositories;
using Store.G04.Core.Mapping.Baskets;

namespace Store.G04.APIs.Helpers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddBuiltInServices();
            services.AddUserDefinedServices();
            services.AddSwaggerServices();
            services.AddAutoMapperServices(configuration);
            services.AddDbContextServices(configuration);
            services.ConfigureServices();
            services.AddRedisServices(configuration);

            return services;
        }

        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {

            services.AddControllers();
            

            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            return services;
        }

        private static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            
            return services;
        }

        private static IServiceCollection AddUserDefinedServices(this IServiceCollection services)
        {

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();

            

            return services;
        }

        private static IServiceCollection AddAutoMapperServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));


            return services;
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {

            
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(x => x.Value.Errors.Count() > 0)
                                            .SelectMany(x => x.Value.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToArray();

                    var resopnse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(resopnse);
                };
            });

            return services;
        }

        private static IServiceCollection AddRedisServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(connection);
            });

            return services;
            
        }

    }
}
