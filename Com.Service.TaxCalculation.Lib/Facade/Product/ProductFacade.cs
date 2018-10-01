using Com.Service.TaxCalculation.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ReadResponse<ProductModel> Read(int page, int size, List<string> select, string keyword, string filter)
        {
            IQueryable<ProductModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Name"
            };
            query = QueryHelper<ProductModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<ProductModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Id","Name","TaxCode","Type"
                };

            query = query
                    .Select(field => new ProductModel
                    {
                        Id = field.Id,
                        Name = field.Name,
                        TaxCode = field.TaxCode,
                        Type = field.Type,

                    });


            Pageable<ProductModel> pageable = new Pageable<ProductModel>(query, page - 1, size);
            List<ProductModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<ProductModel>(data, totalData, selectedFields);
        }

        public async Task<ProductModel> ReadByIdAsync(int id)
        {
            return await DbSet.FirstOrDefaultAsync(d => d.Id.Equals(id));
        }
    }
}
