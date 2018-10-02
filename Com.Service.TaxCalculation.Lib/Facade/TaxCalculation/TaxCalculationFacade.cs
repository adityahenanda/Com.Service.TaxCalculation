using Com.Service.TaxCalculation.Lib.Facade.Product;
using Com.Service.TaxCalculation.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Service.TaxCalculation.Lib.Facade.TaxCalculation
{
    public class TaxCalculationFacade : ITaxCalculationFacade
    {
        private readonly DbContext DbContext;
        private readonly DbSet<TaxCalculationModel> DbSet;

        public TaxCalculationFacade(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TaxCalculationModel>();
        }

        public async Task<int> CreateAsync(TaxCalculationModel model)
        {
            model.TotalAmount = 0;
            model.TotalTaxAmount = 0;
            model.GrandTotal = 0;

            foreach (TaxCalculationDetailsModel item in model.Details)
            {
                item.Type = TaxCalculationExtension.generateType(item.TaxCode);
                item.TaxAmount = TaxCalculationExtension.generateTaxAmount(item.TaxCode, item);
                item.Total = item.Amount + item.TaxAmount;

                model.TotalAmount += item.Amount;
                model.TotalTaxAmount += item.TaxAmount;
                model.GrandTotal += item.Total;

                EntityExtension.FlagForCreate(item, "admin");
            }

            EntityExtension.FlagForCreate(model, "admin");
            DbSet.Add(model);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<TaxCalculationModel> Read(int page, int size, string order,List<string> select, string keyword, string filter)
        {
            IQueryable<TaxCalculationModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                ""
            };
            query = QueryHelper<TaxCalculationModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<TaxCalculationModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Id","TotalAmount","TotalTaxAmount","GrandTotal","Details","CreatedUtc"
                };

            query = query
                    .Select(field => new TaxCalculationModel
                    {
                        Id = field.Id,
                        TotalAmount = field.TotalAmount,
                        TotalTaxAmount = field.TotalTaxAmount,
                        GrandTotal=field.GrandTotal,
                        CreatedUtc=field.CreatedUtc,
                        Details = new List<TaxCalculationDetailsModel>(field.Details.Select(i => new TaxCalculationDetailsModel()
                        {
                            Id = i.Id,
                            Name = i.Name,
                            TaxAmount = i.TaxAmount,
                            TaxCode = i.TaxCode,
                            Amount = i.Amount,
                            Type = i.Type,
                            Total = i.Total,
                            TaxCalculationId = i.TaxCalculationId,
                        }))
                    });
            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<TaxCalculationModel>.Order(query, orderDictionary);

            Pageable<TaxCalculationModel> pageable = new Pageable<TaxCalculationModel>(query, page - 1, size);
            List<TaxCalculationModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<TaxCalculationModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<TaxCalculationModel> ReadByIdAsync(int id)
        {
            return await DbSet.Include(res => res.Details).FirstOrDefaultAsync(d => d.Id.Equals(id));
        }
    }
}
