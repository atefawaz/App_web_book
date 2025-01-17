using Microsoft.EntityFrameworkCore; // For DbContext
using App_web_book.library; // For ApplicationDbContext

namespace App_web_book
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Use WebApplication.CreateBuilder instead of App_web_book.CreateBuilder
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Add DbContext with the connection string from appsettings.json
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add controllers and other required services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Enable Swagger for API documentation in Development
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Uncomment this in production for HTTPS redirection
            // app.UseHttpsRedirection();

            app.UseAuthorization();

            // Map controllers
            app.MapControllers();

            // Start the application
            app.Run();
        }
    }
}