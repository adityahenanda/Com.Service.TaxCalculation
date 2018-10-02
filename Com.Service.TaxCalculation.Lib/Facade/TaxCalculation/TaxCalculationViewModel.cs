using Com.Service.TaxCalculation.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Facade.TaxCalculation
{
    public class TaxCalculationViewModel : BaseViewModel, IValidatableObject
    {
        public double? TotalAmount { get; set; }
        public double? TotalTaxAmount { get; set; }
        public double? GrandTotal { get; set; }
        public ICollection<TaxCalculationDetailsViewModel> Details { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Details.Count.Equals(0))
            {
                yield return new ValidationResult("is required", new List<string> { "Details" });
            }
            else
            {

            }
        }
    }
}
