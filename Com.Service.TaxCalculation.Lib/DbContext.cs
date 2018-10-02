using Com.Moonlay.Data.EntityFrameworkCore;
using Com.Service.TaxCalculation.Lib.Facade.Product;
using Com.Service.TaxCalculation.Lib.Facade.TaxCalculation;
using Microsoft.EntityFrameworkCore;

namespace Com.Service.TaxCalculation.Lib
{
    public class DbContext : StandardDbContext
    {
        public DbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<TaxCalculationDetailsModel> TaxCalculationDetails { get; set; }
        public DbSet<TaxCalculationModel> TaxCalculation { get; set; }
    }
}
