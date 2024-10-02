
using Lab1.Data;
using Lab1.Data.Repos;
using Lab1.Data.Repos.IRepos;
using Lab1.Services;
using Lab1.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lab1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            DotNetEnv.Env.Load();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,                
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,         
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

                };
            });

            // Add services to the container.
            builder.Services.AddDbContext<RestaurantContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("LocalReact", policy =>
                {
                    policy.WithOrigins("http://localhost:5173/")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
                
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<Data.Repos.IRepos.ICustomerRepo, CustomerRepo>();
            builder.Services.AddScoped<Services.IServices.ICustomerServices, CustomerServices>();
            builder.Services.AddScoped<ITableRepo, TableRepo>();
            builder.Services.AddScoped<ITableServices, TableServices>();
            builder.Services.AddScoped<IBookingServices, BookingServices>();
            builder.Services.AddScoped<IBookingRepo, BookingRepo>();
			builder.Services.AddScoped<IDishServices, DishServices>();
			builder.Services.AddScoped<IDishRepo, DishRepo>();
            builder.Services.AddScoped<IAdminRepo, AdminRepo>();
            builder.Services.AddScoped<IAdminServices, AdminServices>();

			var app = builder.Build();
            app.UseAuthentication();
            app.UseAuthorization(); 

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
