using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Service.TaxCalculation.Lib.Utilities.Interfaces
{
    public interface IBaseFacade<TModel>
    {
        Task<int> CreateAsync(TModel model);
        Task<TModel> ReadByIdAsync(int id);
    }
}
