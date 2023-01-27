using gRPCService.Entity;
using Microsoft.EntityFrameworkCore;

namespace gRPCService.Data
{
    public class ProductDbContext :DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
        public DbSet<Product> Product { get; set; }
    }
}
