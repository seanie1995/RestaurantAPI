using Lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Data
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) 
        { 
        }       
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<Dish> Dish { get; set; }
        public DbSet<Booking> Booking {  get; set; }
        public DbSet<Admin> Admin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dish>().HasData
                (
                    new Dish { Id = 1, Name = "Cottage Cheese Pierogi", Availability = true, Price = 89 },
                    new Dish { Id = 2, Name = "Russian Pierogi", Availability = true, Price = 89 },
                    new Dish { Id = 3, Name = "Schabowy Pork Cutlet", Availability = true, Price = 119 },
                    new Dish { Id = 4, Name = "Sour Rye Soup", Availability = true, Price = 99 },
                    new Dish { Id = 5, Name = "Bigos", Availability = true, Price = 99 }
                );

            modelBuilder.Entity<Table>().HasData
                (
                    new Table { Id = 1, Capacity = 4 },
                    new Table { Id = 2, Capacity = 4 },
                    new Table { Id = 3, Capacity = 2 },
                    new Table { Id = 4, Capacity = 6 },
                    new Table { Id = 5, Capacity = 8 }
                );
          
        }
    }
}
