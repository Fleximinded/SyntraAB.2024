
using Microsoft.EntityFrameworkCore;
using Syntra.Frituurtje.API.Services;
using Syntra.Frituurtje.Database.Context;
using Syntra.Frituurtje.Database.Defines;
using Syntra.Frituurtje.Database.Repository;

namespace Syntra.Frituurtje.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<FrituurtjeContext>(o => o.UseSqlServer(connectionStr));

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderManagerService, OrderManagerService>();



            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
