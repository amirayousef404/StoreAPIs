
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.G04.APIs.Errors;
using Store.G04.APIs.Helpers;
using Store.G04.APIs.Middlewares;
using Store.G04.Core;
using Store.G04.Core.Mapping.Products;
using Store.G04.Core.Services.Contract;
using Store.G04.Repository;
using Store.G04.Repository.Data;
using Store.G04.Repository.Data.Contexts;
using Store.G04.Service.Services.Products;

namespace Store.G04.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDependencies(builder.Configuration);

            var app = builder.Build();

            await app.ConfigureMiddlewareAsync();

            app.Run();
        }
    }
}
