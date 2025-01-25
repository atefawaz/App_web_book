using App_web_book.Data;
using App_web_book.Data.VSDC;
using Microsoft.EntityFrameworkCore;
using App_web_book.Entities.VSDCEntities;

namespace App_web_book
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add both DbContexts
            builder.Services.AddDbContext<BookdbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BooksConnection")));
            
            builder.Services.AddDbContext<AtefContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("UsersConnection")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}