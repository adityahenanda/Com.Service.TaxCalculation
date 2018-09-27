using Com.Service.TaxCalculation.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Service.TaxCalculation.Lib.Facade.Product
{
    public class ProductFacade : IProductFacade
    {
        private readonly DbContext DbContext;
        private readonly DbSet<ProductModel> DbSet;

        public ProductFacade(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = this.DbContext.Set<ProductModel>();
        }

        public async Task<int> CreateAsync(ProductModel model)
        {
            EntityExtension.FlagForCreate(model, "admin");
            DbSet.Add(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<ProductModel> ReadByIdAsync(int id)
        {
            return await DbSet.FirstOrDefaultAsync(d => d.Id.Equals(id));
        }
    }
}
