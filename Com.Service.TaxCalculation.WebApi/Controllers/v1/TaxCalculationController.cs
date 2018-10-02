using AutoMapper;
using Com.Service.TaxCalculation.Lib.Facade.TaxCalculation;
using Com.Service.TaxCalculation.Lib.Utilities;
using Com.Service.TaxCalculation.WebApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Service.TaxCalculation.WebApi.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/tax-calculation")]
    public class TaxCalculationController : BaseController<TaxCalculationModel, TaxCalculationViewModel, ITaxCalculationFacade>
    {
        public TaxCalculationController(IValidateService validateService, ITaxCalculationFacade facade, IMapper mapper) : base(validateService, facade, mapper, "1.0.0")
        {
        }
    }
}
