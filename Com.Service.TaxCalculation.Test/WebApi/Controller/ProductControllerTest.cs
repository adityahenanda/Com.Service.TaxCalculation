using Com.Service.TaxCalculation.Lib.Facade.Product;
using Com.Service.TaxCalculation.Test.WebApi.Utils;
using Com.Service.TaxCalculation.WebApi.Controllers.v1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Service.TaxCalculation.Test.WebApi.Controller
{
    public class ProductControllerTest : BaseControllerTest<ProductController, ProductModel, ProductViewModel, IProductFacade>
    {
    }
}
