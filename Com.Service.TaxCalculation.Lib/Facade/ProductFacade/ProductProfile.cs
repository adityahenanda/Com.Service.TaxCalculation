using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Service.TaxCalculation.Lib.Facade.ProductFacade
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductModel, ProductViewModel>().ReverseMap();
        }
    }
}
