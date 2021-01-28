using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public RestaurantContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}