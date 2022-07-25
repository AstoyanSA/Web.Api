using Microsoft.EntityFrameworkCore;

namespace Web.Api.Data
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
