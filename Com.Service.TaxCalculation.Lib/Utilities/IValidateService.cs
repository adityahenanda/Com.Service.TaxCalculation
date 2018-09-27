using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Utilities
{
    public interface IValidateService
    {
        void Validate(dynamic viewModel);
    }
}
