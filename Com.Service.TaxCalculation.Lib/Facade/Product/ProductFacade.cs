using Com.Service.TaxCalculation.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Service.TaxCalculation.Lib.Facade.Product
{
    public class ProductFacade : InterfaceProductFacade
    {
        private readonly DbContext dbContext;
        private readonly DbSet<ProductModel> dbSet;

        public ProductFacade(DbContext dbContext, DbSet<ProductModel> dbSet)
        {
            this.dbContext = dbContext;
            this.dbSet = dbSet;
            //dbSet = dbContext.Set<ProductModel>();
        }

        public async Task<int> CreateAsync(ProductModel model)
        {
            EntityExtension.FlagForCreate(model, "admin");
            dbSet.Add(model);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<ProductModel> ReadByIdAsync(int id)
        {
            return await dbSet.FirstOrDefaultAsync(d => d.Id.Equals(id));
        }
    }
}
