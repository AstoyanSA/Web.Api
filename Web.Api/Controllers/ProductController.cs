using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Api.Data;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly WebApiDbContext context;

        public ProductController(WebApiDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return await context.Products.ToListAsync();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Product>> GetProduct(int Id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == Id);

            if (product == null)
                return BadRequest($"No such product with id {Id}");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<List<Product>>> AddProduct(Product product)
        {
            this.context.Products.Add(product);
            await this.context.SaveChangesAsync();

            return Ok(await context.Products.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Product>>> UpdateProduct(Product productToEdit)
        {
            var product = await context.Products.FirstOrDefaultAsync(productToEdit.Id);
            if (product == null)
                return BadRequest($"No such product with id {productToEdit.Id}");

            productToEdit.Id = product.Id;
            productToEdit.Name = product.Name;
            productToEdit.Qty = product.Qty;
            productToEdit.Price = product.Price;

            await context.SaveChangesAsync();
            return Ok(await context.Products.ToListAsync());
        }

        [HttpDelete("id")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int Id)
        {
            var product = await context.Products.FirstOrDefaultAsync(Id);
            if (product == null)
                return BadRequest($"No such product with id {Id}");

            context.Products.Remove(product);

            await context.SaveChangesAsync();

            return Ok(await context.Products.ToListAsync());
        }
    }
}
