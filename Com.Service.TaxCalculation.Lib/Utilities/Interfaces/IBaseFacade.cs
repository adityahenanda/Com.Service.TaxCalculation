using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Service.TaxCalculation.Lib.Utilities.Interfaces
{
    public interface IBaseFacade<TModel>
    {
        ReadResponse<TModel> Read(int page, int size, string order, List<string> select, string keyword, string filter);
        Task<int> CreateAsync(TModel model);
        Task<TModel> ReadByIdAsync(int id);
    }
}
