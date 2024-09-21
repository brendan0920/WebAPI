using bmdbWebAPIEF.Models;
using Microsoft.EntityFrameworkCore;

namespace bmdbWebAPIEF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<bmdbContext>
                (
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("bmdbConnectionString"))
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
