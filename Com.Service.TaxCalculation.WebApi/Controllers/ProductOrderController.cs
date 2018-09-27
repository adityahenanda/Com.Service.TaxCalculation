using AutoMapper;
using Com.Service.TaxCalculation.Lib.Facade.Product;
using Com.Service.TaxCalculation.Lib.Utilities;
using Com.Service.TaxCalculation.WebApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Service.TaxCalculation.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sales/production-orders")]
    public class ProductOrderController : BaseController<ProductModel, ProductViewModel, InterfaceProductFacade>
    {
        public ProductOrderController(IValidateService validateService, InterfaceProductFacade interfaceProductFacade, IMapper mapper) : base( validateService, interfaceProductFacade, mapper, "1.0")
        {
        }

    }
}
