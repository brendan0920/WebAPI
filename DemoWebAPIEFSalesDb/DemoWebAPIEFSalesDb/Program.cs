using DemoWebAPIEFSalesDb.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPIEFSalesDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<SalesDBContext>
                (
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("SalesDBConnectionString"))
                );
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
