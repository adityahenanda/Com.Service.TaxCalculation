using Com.Service.TaxCalculation.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Facade.Product
{
    public class ProductViewModel : BaseViewModel, IValidatableObject
    {
        public string Name { get; set; }
        public int TaxCode { get; set; }
        public string Type { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (string.IsNullOrEmpty(this.Name))
                yield return new ValidationResult("is required", new List<string> { "Name" });

            if (this.TaxCode.Equals(0))
                yield return new ValidationResult("is required", new List<string> { "TaxCode" });

        }
    }
}
