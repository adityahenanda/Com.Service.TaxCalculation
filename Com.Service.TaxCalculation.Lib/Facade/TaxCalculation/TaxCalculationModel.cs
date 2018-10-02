using Com.Service.TaxCalculation.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Facade.TaxCalculation
{
    public class TaxCalculationModel : BaseModel
    {
        public double? TotalAmount { get; set; }
        public double? TotalTaxAmount { get; set; }
        public double? GrandTotal { get; set; }
        public ICollection<TaxCalculationDetailsModel> Details { get; set; }
    }
}
