using Com.Moonlay.Data.EntityFrameworkCore;
using Com.Service.TaxCalculation.Lib.Facade.Product;
using Microsoft.EntityFrameworkCore;

namespace Com.Service.TaxCalculation.Lib
{
    public class DbContext : StandardDbContext
    {
        public DbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ProductModel> Product { get; set; }
    }
}
