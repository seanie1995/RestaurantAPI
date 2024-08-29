
using Lab1.Data;
using Lab1.Data.Repos;
using Lab1.Data.Repos.IRepos;
using Lab1.Services;
using Lab1.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Lab1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<RestaurantContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<Data.Repos.IRepos.ICustomerRepo, CustomerRepo>();
            builder.Services.AddScoped<Services.IServices.ICustomerServices, CustomerServices>();
            builder.Services.AddScoped<ITableRepo, TableRepo>();
            builder.Services.AddScoped<ITableServices, TableServices>();
            builder.Services.AddScoped<IBookingServices, BookingServices>();
            builder.Services.AddScoped<IBookingRepo, BookingRepo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
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
