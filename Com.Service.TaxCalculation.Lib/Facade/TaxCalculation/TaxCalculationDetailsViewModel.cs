using Com.Service.TaxCalculation.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Facade.TaxCalculation
{
    public class TaxCalculationDetailsViewModel : BaseViewModel, IValidatableObject
    {
        public string Name { get; set; }
        public int TaxCode { get; set; }
        public string Type { get; set; }
        public double? Amount { get; set; }
        public double? TaxAmount { get; set; }
        public double? Total { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
