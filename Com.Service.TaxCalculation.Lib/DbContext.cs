using Com.Moonlay.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Com.Service.TaxCalculation.Lib
{
    public class DbContext : StandardDbContext
    {
        public DbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
