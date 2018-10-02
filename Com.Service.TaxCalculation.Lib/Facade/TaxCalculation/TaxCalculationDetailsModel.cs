using Com.Service.TaxCalculation.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Facade.TaxCalculation
{
    public class TaxCalculationDetailsModel : BaseModel
    {
        public string Name { get; set; }
        public int TaxCode { get; set; }
        public string Type { get; set; }
        public double? Amount { get; set; }
        public double? TaxAmount { get; set; }
        public double? Total { get; set; }
        public int TaxCalculationId { get; set; }
        public virtual TaxCalculationModel TaxCalculation { get; set; }
    }
}
