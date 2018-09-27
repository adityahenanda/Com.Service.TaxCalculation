using Com.Service.TaxCalculation.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Facade.ProductFacade
{
    public class ProductModel :BaseModel
    {
        [MaxLength(255)]
        public string Name { get; set; }
        public int TaxCode { get; set; }
        [MaxLength(25)]
        public string Type { get; set; }

    }
}
