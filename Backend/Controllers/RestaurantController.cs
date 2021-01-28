using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Backend.Models;
using System.Linq;
using System;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {
        public RestaurantContext Context { get; set; }
        public RestaurantController(RestaurantContext context)
        {
            Context = context;
        }

        [Route("GetRestaurants")]
        [HttpGet]
        public async Task<List<Restaurant>> GetRestaurants() => await Context.Restaurants.Include(p => p.Products).ToListAsync();

        [Route("AddRestaurant")]
        [HttpPost]
        public async Task AddRestaurant([FromBody] Restaurant restaurant)
        {
            Context.Restaurants.Add(restaurant);
            await Context.SaveChangesAsync();
        }

        [Route("UpdateRestaurant")]
        [HttpPut]
        public async Task UpdateRestaurant([FromBody] Restaurant restaurant)
        {
            Context.Update<Restaurant>(restaurant);
            await Context.SaveChangesAsync();
        }

        [Route("DeleteRestaurant/{id}")]
        [HttpDelete]
        public async Task DeleteRestaurant([FromBody] int id)
        {
            Context.Remove(Context.Restaurants.FindAsync(id));
            await Context.SaveChangesAsync();
        }

        [Route("GetProducts/{id}")]
        [HttpGet]
        public async Task<List<Product>> GetProducts(int id) => await Context.Products.ToListAsync();

        [Route("AddProduct/{id}")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(int id, [FromBody] Product product)
        {
            if (String.IsNullOrEmpty(product.Name) || String.IsNullOrEmpty(product.Description) || product.Price <= 0)
            {
                return BadRequest( new { message = "All fields must be filled!" } );
            }

            var restaurant = await Context.Restaurants.FindAsync(id);

            product.restaurant = restaurant;

            Context.Products.Add(product);
            await Context.SaveChangesAsync();
        
            return Ok(product.ID);
        }

        [Route("UpdateProduct")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            if (String.IsNullOrEmpty(product.Name) || String.IsNullOrEmpty(product.Description) || product.Price <= 0)
            {
                return BadRequest( new { message = "All fields must be filled!" } );
            }

            var p = await Context.Products.FindAsync(product.ID);

            p.Name = product.Name;
            p.Description = product.Description;
            p.Price = product.Price;
            
            await Context.SaveChangesAsync();
            
            return Ok();
        }

        [Route("DeleteProduct/{id}")]
        [HttpDelete]
        public async Task DeleteProduct(int id)
        {
            Context.CartItems.Include(p => p.product).Where(x => x.product.ID == id).ToList().ForEach(x => Context.CartItems.Remove(x));

            Context.Products.Remove(await Context.Products.FindAsync(id));
            
            await Context.SaveChangesAsync();
        }

        [Route("GetCartItems")]
        [HttpGet]
        public async Task<List<CartItem>> GetCartItems() => await Context.CartItems.Include(x => x.product).ToListAsync();

        [Route("AddCartItem/{id}")]
        [HttpPost]
        public async Task<int> AddCartItem(int id)
        {
            var item = await Context.CartItems.Include(x => x.product).FirstOrDefaultAsync(x => x.product.ID == id);

            if (item == null)
            {
                var newItem = await Context.Products.FindAsync(id);
                Context.CartItems.Add(item = new CartItem { product = newItem, Quantity = 1 });
            }
            else item.Quantity = Math.Clamp(++item.Quantity, 0, 10);

            await Context.SaveChangesAsync();

            return item.Quantity;
        }

    
        [Route("DeleteCartItem/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var item = await Context.CartItems.Include(x => x.product).FirstOrDefaultAsync(x => x.product.ID == id);

            if (item != null)
            {
                if (item.Quantity < 2)
                {
                    Context.CartItems.Remove(item);
                } else item.Quantity = Math.Clamp(--item.Quantity, 0, 10);
                
                await Context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return BadRequest(new { message = "Cart item does not exist." });
            }
        }

        [Route("DeleteAllCartItem")]
        [HttpDelete]
        public async Task DeleteAllCartItem()
        {
            await Context.CartItems.ForEachAsync(x => Context.CartItems.Remove(x));
            await Context.SaveChangesAsync();
        }
    }
}
