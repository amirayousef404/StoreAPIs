using Store.G04.APIs.Middlewares;
using Store.G04.Repository.Data.Contexts;
using Store.G04.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Store.G04.APIs.Helpers
{
    public static class ConfigureMiddleware
    {
        public async static Task<WebApplication> ConfigureMiddlewareAsync(this WebApplication app)
        {
            // when I deal with any unmange resource use using

            using var scope = app.Services.CreateScope();


            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<StoreDbContext>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            app.UseMiddleware<ExceptionMiddleware>();

            try
            {

                await context.Database.MigrateAsync(); // update-database
                await StoreDbContextSeed.SeedAsync(context);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex, "there are problems during apply migrations!");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }
    }
}
